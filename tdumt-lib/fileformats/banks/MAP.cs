using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Xml;
using DjeFramework1.Common.Calculations;
using DjeFramework1.Common.Support.Meta;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.support;
using TDUModdingLibrary.support.constants;

namespace TDUModdingLibrary.fileformats.banks
{
    /// <summary>
    /// Représente le format MAP.
    /// </summary>
    public class MAP : TduFile
    {
        #region Constantes
        /// <summary>
        /// Pattern for file name (to redefine)
        /// </summary>
        public new const string FILENAME_PATTERN = @"Bnk1\.map$";

        /// <summary>
        /// Nom du fichier gérant la MAP
        /// </summary>
        public const string FILE_MAP = @"\Bnk1.map";

        /// <summary>
        /// Size of 1.45's Bnk1.map
        /// </summary>
        public const long SIZE_1_45_MAP = 165701;

        /// <summary>
        /// Size of 1.66's (no pack) Bnk1.map
        /// </summary>
        public const long SIZE_1_66_MAP = 165845;

        /// <summary>
        /// Size of 1.66's (with pack) Bnk1.map
        /// </summary>
        public const long SIZE_1_66_MEGAPACK_MAP = 169613;

        /// <summary>
        /// Taille du tag, en octets
        /// </summary>
        private const uint _TAG_LENGTH = 4;

        /// <summary>
        /// Libellé du message d'erreur lorsqu'aucune entrée n'a été trouvée
        /// </summary>
        private const string _ERROR_ENTRY_NOT_FOUND = "Entry not found for '{0}'";
        
        /// <summary>
        /// Balise XML 'identifiant' pour la clé
        /// </summary>
        private const string _KEY_TAG_ID = "mapId";

        /// <summary>
        /// Balise XML 'nom de fichier' pour la clé
        /// </summary>
        private const string _KEY_TAG_FILENAME = "fileName";

        
        #endregion

        #region Structures
        /// <summary>
        /// Représente un entrée dans le fichier MAP
        /// </summary>
        public struct Entry
        {
            public UInt32 entryNumber;
            public UInt32 address;
            public UInt32 fileId;
            public UInt32 firstSize;
            public UInt32 secondSize;
            public String fileName;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Liste d'entrées dans le fichier MAP
        /// </summary>
        public Dictionary<UInt32, Entry> EntryList
        {
            get { return entryList; }
        }
        private readonly Dictionary<UInt32, Entry> entryList;

        /// <summary>
        /// Nombre d'entrées dans le fichier MAP
        /// </summary>
        public int EntryCount
        {
            get
            {
                if (entryList == null)
                    return -1;
                
                return entryList.Count;
            }
        }

        /// <summary>
        /// Tag du fichier (X premiers caractères)
        /// </summary>
        public string Tag
        {
            get { return _Tag; }
        }
        private string _Tag;

        /// <summary>
        /// Séquence permettant de délimiter la fin d'une entrée
        /// </summary>
        public string FinishMarker
        {
            get { return finishMarker; }
        }
        private string finishMarker;

        /// <summary>
        /// True when file is a magic map, else if it's not.
        /// </summary>
        public bool IsMagicMap
        {
            get { return _IsMagicMap; }
        }
        private bool _IsMagicMap;
        #endregion

        #region Attributs
        #endregion

        /// <summary>
        /// Constructeur.
        /// </summary>
        internal MAP(string mapFileName)
        {
            // Mise à jour des attributs
            _FileName = mapFileName;
            entryList = new Dictionary<UInt32, Entry>();

            // Lecture
            _ReadData();
        }

        internal MAP(){}

        #region TDUFile implementation
        /// <summary>
        /// Initialise les infos avec le contenu du fichier
        /// </summary>
        protected override sealed void _ReadData()
        {
            FileInfo fileInfo = new FileInfo(_FileName);
            BinaryReader reader = null;

            try
            {
                reader = new BinaryReader(new FileStream(_FileName, FileMode.Open, FileAccess.Read));
                _FileSize = (uint)fileInfo.Length;

                reader.BaseStream.Seek(0x0L, SeekOrigin.Begin);

                // TAG
                _Tag = new string(reader.ReadChars((int)_TAG_LENGTH));

                // Lecture des entrées  (attention, tout est en Big Endian ici !)
                uint count = 0;
                bool finished = false;
                Entry newEntry;

                _IsMagicMap = true;

                while (!finished)
                {
                    newEntry = new Entry {entryNumber = count};

                    // 1 octet inutilisé
                    reader.BaseStream.Seek(1, SeekOrigin.Current);

                    // Adresse
                    newEntry.address = (uint)reader.BaseStream.Position;

                    // Id du fichier : 4 octets 
                    newEntry.fileId = BinaryTools.ToBigEndian(reader.ReadUInt32());

                    // Taille 1
                    newEntry.firstSize = BinaryTools.ToBigEndian(reader.ReadUInt32());

                    // Non utilisé : 4 octets
                    reader.BaseStream.Seek(4, SeekOrigin.Current);

                    // Taille 2
                    newEntry.secondSize = BinaryTools.ToBigEndian(reader.ReadUInt32());

                    // Non utilisé : 5 octets
                    reader.BaseStream.Seek(5, SeekOrigin.Current);

                    // Marqueur de fin d'entrée
                    if (finishMarker == null)
                        finishMarker = Encoding.ASCII.GetString(reader.ReadBytes(2));
                    else
                        reader.BaseStream.Seek(2, SeekOrigin.Current);

                    // Ajout de l'entrée à la collection
                    entryList.Add(newEntry.fileId, newEntry);

                    // Is it a magic map ?
                    if ((newEntry.firstSize != 0 || newEntry.secondSize != 0) && _IsMagicMap)
                        _IsMagicMap = false;

                    if (reader.BaseStream.Position >= _FileSize)
                        finished = true;

                    count++;
                }
            }
            catch (EndOfStreamException)
            {
                // Fin du fichier atteinte, on ne fait rien
            }
            finally
            {
                if (reader != null)
                {
                    reader.BaseStream.Seek(0, SeekOrigin.Begin);
                    reader.Close();
                }
            }

            // EVO_65: Properties
            Property.ComputeValueDelegate entryCountDelegate = () => EntryCount.ToString();
            Property.ComputeValueDelegate magicDelegate = () => IsMagicMap ? "Yes" : "No";

            Properties.Add(new Property("Entry count", "Mapping", entryCountDelegate));
            Properties.Add(new Property("Magic Map?", "Mapping", magicDelegate));
        }

        /// <summary>
        /// Not implemented - do not use
        /// </summary>
        public override void Save()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Met à jour les tailles de l'entrée spécifiée.
        /// Ecrit directement dans le fichier
        /// </summary>
        /// <param name="fileId">identifiant interne du fichier</param>
        /// <param name="size1">taille 1 à appliquer</param>
        /// <param name="size2">taille 2 à appliquer</param>
        public void UpdateEntrySizes(uint fileId, uint size1, uint size2)
        {
            Collection<uint> idList = new Collection<uint> { fileId };
            Collection<uint> size1List = new Collection<uint> { size1 };
            Collection<uint> size2List = new Collection<uint> { size2 };

            UpdateEntrySizes(idList, size1List, size2List);
        }

        /// <summary>
        /// Met à jour les tailles des entrées spécifiées.
        /// Ecrit directement dans le fichier
        /// </summary>
        /// <param name="idList">liste d'identifiants internes de fichiers</param>
        /// <param name="size1List">liste des tailles 1 à appliquer</param>
        /// <param name="size2List">liste des tailles 2 à appliquer</param>
        public void UpdateEntrySizes(Collection<uint> idList, Collection<uint> size1List, Collection<uint> size2List)
        {
            if (idList == null || size1List == null || size2List == null)
                return;

            // Parcours des entrées
            for (int i = 0 ; i < idList.Count ; i++)
            {
                uint fileId = idList[i];
                uint size1 = size1List[i];
                uint size2 = size2List[i];

                // Récupération de l'entrée
                Entry e = EntryList[fileId];

                // Mise à jour de la structure
                e.firstSize = size1;
                e.secondSize = size2;

                // Enregistrement dans le fichier
                BinaryWriter mapWriter = null;
                try
                {
                    // ANO_22 : on enlève l'attribut 'lecture seule' sur le fichier
                    File2.RemoveAttribute(FileName, FileAttributes.ReadOnly);

                    mapWriter = new BinaryWriter(new FileStream(FileName, FileMode.Open));
                    mapWriter.BaseStream.Seek(e.address, SeekOrigin.Begin);

                    // Taille 1
                    mapWriter.BaseStream.Seek(0x4, SeekOrigin.Current);
                    mapWriter.Write(BinaryTools.ToLittleEndian(size1));

                    // Taille 2
                    mapWriter.BaseStream.Seek(0x4, SeekOrigin.Current);
                    mapWriter.Write(BinaryTools.ToLittleEndian(size2));

                    // Mise à jour de la liste d'entrées
                    EntryList.Remove(fileId);
                    EntryList.Add(fileId, e);
                }
                finally
                {
                    if (mapWriter != null)
                        mapWriter.Close();
                }
            }
        }

        /// <summary>
        /// Renvoie la liste des fichiers dans le dossier BNK de TDU ainsi que dans les sous-dossiers
        /// </summary>
        /// <param name="tduRootFolder"></param>
        /// <returns></returns>
        public static Dictionary<string, long> ReportTDUFiles(string tduRootFolder)
        {
            // Initialisation
            Dictionary<string, long> fileList = new Dictionary<string, long>();

            // Dossier de démarrage
            if (!tduRootFolder.EndsWith(@"\"))
                tduRootFolder += @"\";

            string bnkFolder = (tduRootFolder + LibraryConstants.FOLDER_BNK);

            // Parcours du système de fichiers
            _UpdateFileList(fileList, bnkFolder);

            return fileList;
        }

        /// <summary>
        /// Tente d'associer chaque entrée dans le fichier MAP à un fichier du jeu
        /// </summary>
        /// <param name="tduFiles">Liste de fichiers</param>
        /// <param name="keyContents">Contenu de la clé</param>
        /// <param name="failedFiles">Liste de fichiers pour lesquels l'identification a échoué</param>
        /// <returns>Les noms de fichiers ainsi que leurs ids possibles</returns>
        public Dictionary<string, ArrayList> LinkEntriesToFiles(Dictionary<string, long> tduFiles, Dictionary<uint, string> keyContents, StringCollection failedFiles)
        {
            Dictionary<string, ArrayList> fileIdentifiers = new Dictionary<string, ArrayList>();

            // Pour chaque fichier, on va regarder à quel id(s) correspond sa taille
            foreach (string name in tduFiles.Keys)
            {
                bool entryFound = false;
                long size = tduFiles[name];

                // EVO : si le fichier est trouvé dans la clé, on ajoute seulement l'identifiant qui correspond
                string fileNameValue = _GetRelativePathFromAbsolute(name);

                if (keyContents.ContainsValue(fileNameValue))
                {
                    foreach (uint anotherId in keyContents.Keys)
                    {
                        string anotherFileName = keyContents[anotherId];

                        if (anotherFileName.Equals(fileNameValue))
                        {
                            ArrayList idList = new ArrayList {anotherId};

                            fileIdentifiers.Add(name, idList);
                            entryFound = true;
                            break;
                        }
                    }
                } else {
                    // Parcours des entrées du fichier MAP
                    foreach (uint anotherId in entryList.Keys)
                    {
                        Entry theEntry = entryList[anotherId];

                        // EVO : si l'identifiant est présent dans la clé
                        if (!keyContents.ContainsKey(anotherId) && theEntry.firstSize == size)
                        {
                            // On vérifie la taille 1 pour le moment
                            entryFound = true;
                            
                            // On ajoute l'identifiant à la liste
                            ArrayList idList;
                            
                            if (fileIdentifiers.ContainsKey(name))
                                idList = fileIdentifiers[name];
                            else
                                idList = new ArrayList();
                            fileIdentifiers.Add(name, idList);

                            idList.Add(anotherId);
                        }

                    }
                }

                //Gestion de la liste d'échecs
                if (!entryFound)
                    failedFiles.Add(name);
            }

            return fileIdentifiers;
        }

        /// <summary>
        /// Utilise le fichier clé spécifié pour mettre à jour les noms de fichiers
        /// </summary>
        /// <param name="keyFilePath">Chemin vers le fichier clé</param>
        public void SetFileNamesFromKey(string keyFilePath)
        {
            // Récupération du contenu de la clé
            Dictionary<uint, string> keyContents = GetKeyContents(keyFilePath);

            // Parcours de la clé
            foreach (uint anotherId in keyContents.Keys)
            {
                // Mise à jour de la structure
                try
                {
                    Entry currentEntry = entryList[anotherId];

                    if (currentEntry.entryNumber > 0)
                    {
                        currentEntry.fileName = keyContents[anotherId];
                        entryList[currentEntry.fileId] = currentEntry;
                    }
                }
                catch (Exception ex)
                {
                    // Exception silencieuse ici, mais tracée
                    Exception2.PrintStackTrace(ex);
                }
            }
        }

        /// <summary>
        /// Renvoie l'entrée du MAP correspondant au nom de fichier spécifié
        /// </summary>
        /// <param name="pFileName">Fichier recherché</param>
        /// <returns>L'entrée correspondante si elle est dispo, sinon une exception est retournée</returns>
        public Entry GetEntryFromFileName(string pFileName)
        {
            if (pFileName == null)
                throw new Exception(string.Format(_ERROR_ENTRY_NOT_FOUND,pFileName));

            // On ne récupère que la partie du chemin commençant à 'Euro'
            int euroPos = pFileName.LastIndexOf(@"\Euro");
            pFileName = pFileName.Substring(euroPos);

            // Parcours des entrées
            foreach (Entry anotherEntry in entryList.Values)
            {
                string anotherFileName = anotherEntry.fileName;

                if (anotherFileName != null && anotherFileName.Equals(pFileName, StringComparison.CurrentCultureIgnoreCase))
                    return anotherEntry;
            }

            // Aucune entrée trouvée
            throw new Exception(string.Format(_ERROR_ENTRY_NOT_FOUND,pFileName));
        }

        /// <summary>
        /// Renvoie le contenu de la clé dans une table indexée par l'identifiant MAP
        /// </summary>
        /// <param name="pKeyFileName">Nom du fichier clé</param>
        /// <returns>Une collection de couples (id,nom de fichier)</returns>
        public static Dictionary<uint, string> GetKeyContents(string pKeyFileName)
        {
            Dictionary<uint,string> res = new Dictionary<uint,string>();

            if (string.IsNullOrEmpty(pKeyFileName))
                return res;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
            
                xmlDoc.Load(pKeyFileName);

                XmlElement rootElement = xmlDoc.DocumentElement;

                if (rootElement != null)
                {
                    // On parcourt les noeuds enfants
                    foreach (XmlElement noeudEnfant in rootElement.ChildNodes)
                    {
                        // Lecture des infos...
                        String mapId = noeudEnfant.GetAttribute(_KEY_TAG_ID);
                        String fileName = noeudEnfant.GetAttribute(_KEY_TAG_FILENAME);

                        res.Add(uint.Parse(mapId), fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                // Exception silencieuse ici, mais tracée
                Exception2.PrintStackTrace(ex);
            }

            return res;
        }

        /// <summary>
        /// Patche le MAP pour désactiver la protection (yes !!!)
        /// Une copie de sauvegarde du MAP peut être créée
        /// </summary>
        /// <param name="makeBackup">true to make a backup</param>
        public void PatchIt(bool makeBackup)
        {
            // If it's already a magic map, no need to patch it...
            if (!_IsMagicMap)
            {
                string backupFileName = null;

                try
                {
                    // Copie de sauvegarde
                    // ANO_27 : gestion des sauvegardes existantes
                    if (makeBackup)
                    {
                        backupFileName = FileName + "." + LibraryConstants.EXTENSION_BACKUP;

                        while (File.Exists(backupFileName))
                            backupFileName += "." + LibraryConstants.EXTENSION_BACKUP;

                        Tools.BackupFile(FileName, backupFileName);
                    }

                    // Collecte des ids
                    ArrayList ids = new ArrayList();

                    foreach (uint entry in EntryList.Keys)
                        ids.Add(entry);

                    // Fix
                    foreach (uint entry in ids)
                        UpdateEntrySizes(entry, 0, 0);
                }
                catch (Exception ex)
                {
                    // Si erreur, on restaure la sauvegarde si elle a été créée
                    Exception2.PrintStackTrace(ex);

                    if (makeBackup && File.Exists(backupFileName))
                        Tools.RestoreFile(backupFileName, _FileName);

                    throw;
                }
            }
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Méthode utilitaire : met à jour la liste de fichiers selon le dossier spécifié
        /// </summary>
        /// <param name="fileList">La liste de fichiers à mettre à jour</param>
        /// <param name="folderToBrowse">Dossier à parcourir</param>
        private static void _UpdateFileList(Dictionary<string, long> fileList, string folderToBrowse)
        {
            DirectoryInfo di = new DirectoryInfo(folderToBrowse);

            foreach (FileInfo anotherFileInfo in di.GetFiles(/*"*.bnk"*/))
            {
                // Ajout de clé
                if (!fileList.ContainsKey(anotherFileInfo.FullName))
                    fileList.Add(anotherFileInfo.FullName, anotherFileInfo.Length);
            }

            // Traitement récursif pour les sous-dossiers
            foreach (DirectoryInfo anotherDirectoryInfo in di.GetDirectories())
                _UpdateFileList(fileList, anotherDirectoryInfo.FullName);
        }

        /// <summary>
        /// Renvoie le chemin relatif d'un fichier depuis son chemin absolu. Ex : D:\Jeux\TDU\Euro\Bnk\toto.bnk >>> \Euro\Bnk\toto.bnk
        /// </summary>
        /// <param name="absoluteFileName">Chemin absolu du fichier</param>
        /// <returns>Le chemin relatif</returns>
        private static string _GetRelativePathFromAbsolute(string absoluteFileName)
        {
            string res = null;

            if (string.IsNullOrEmpty(absoluteFileName))
                return res;

            int startPos = absoluteFileName.IndexOf(@"\Euro\Bnk");

            if (startPos != -1)
                res = absoluteFileName.Substring(startPos);

            return res;
        }
        #endregion
    }
}