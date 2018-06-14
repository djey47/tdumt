using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingTools.common;

namespace TDUModdingTools.gui.maptool
{
    public partial class FileListingForm : Form
    {
        #region Constantes
        /// <summary>
        /// Libellé de départ
        /// </summary>
        private const string _LABEL_START_TEXT = "Please launch analysis.";
        
        /// <summary>
        /// Libellé après analyse
        /// </summary>
        private const string _LABEL_COUNT_TEXT = "{0} files - {1} entries - {2} ids found ({3}% coverage).";
        
        /// <summary>
        /// Message de statut apparaissant en fin d'analyse.
        /// </summary>
        private const string _STATUS_END_ANALYSIS = "Analysis ended.";
       
        /// <summary>
        /// Message de statut apparaissant en fin de génération de clé.
        /// </summary>
        private const string _STATUS_SUCCESS_GENERATE = "KEY successfully generated. Enter this file in MAP Tool back then 'Refresh'.";

        /// <summary>
        /// Message de statut apparaissant en cours de génération de clé.
        /// </summary>
        private const string _STATUS_ANALYSING = "Please wait during analysis...";

        /// <summary>
        /// Message de statut apparaissant après copie des id dans le presse-papiers
        /// </summary>
        private const string _STATUS_COPY_DONE = "Selected id(s) copied to clipboard.";

        /// <summary>
        /// Message d'info demandant le lancement de l'analyse en premier
        /// </summary>
        private const string _INFO_ANALYSIS_FIRST = "Please launch analysis first.";
        #endregion

        #region Attributs
        /// <summary>
        /// Le fichier MAP rattaché
        /// </summary>
        private readonly MAP leMAP;

        /// <summary>
        /// La clé existante
        /// </summary>
        private readonly Dictionary<uint, string> laCle;

        /// <summary>
        /// L'association Nom de fichier,liste d'identifiants MAP
        /// </summary>
        private Dictionary<string, ArrayList> idList;
        #endregion
        
        /// <summary>
        /// Constructeur principal
        /// </summary>
        /// <param name="mapFile">Instance du MAP</param>
        /// <param name="keyFile">Chemin et nom du fichier de clé (optionnel)</param>
        public FileListingForm(MAP mapFile, string keyFile)
        {
            InitializeComponent();

            // EVO 32
            StatusBarLogManager.AddNewLog(this, toolStripStatusLabel);

            leMAP = mapFile;
            if (!string.IsNullOrEmpty(keyFile))
            {
                laCle = MAP.GetKeyContents(keyFile);
                keyPath.Text = keyFile;
            }
            fileCountLabel.Text = _LABEL_START_TEXT;
            tduPath.Text = Program.ApplicationSettings.TduMainFolder;
        }

        #region Evenements
        private void analyzeButton_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(tduPath.Text) || leMAP == null) {
                return;
            }

            try {
                Cursor = Cursors.WaitCursor;

                StatusBarLogManager.ShowEvent(this, _STATUS_ANALYSING);

                // Lance l'analyse
                Dictionary<string,long> currentFileList = MAP.ReportTDUFiles(tduPath.Text);

                // Tente l'association
                StringCollection failedFiles = new StringCollection();
                idList = leMAP.LinkEntriesToFiles(currentFileList, laCle, failedFiles);

                // Met à jour la liste de fichiers
                _UpdateFileList();

                Cursor = Cursors.Default;
            } catch (Exception ex) {
                MessageBoxes.ShowError(this,ex);
            } finally {
                // EVO 32
                StatusBarLogManager.ShowEvent(this, _STATUS_END_ANALYSIS);
            }
        }

        private void exportOKResultsButton_Click(object sender, EventArgs e)
        {
            if (fileList.Items.Count == 0)
            {
                MessageBoxes.ShowInfo(this,_INFO_ANALYSIS_FIRST);
                return;
            }

            // Permet de sélectionner un fichier de sortie
            saveFileDialog.FileName = "";
            saveFileDialog.Filter = GuiConstants.FILTER_XML_ALL_FILES;

            DialogResult dr = saveFileDialog.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;

                    _GenerateKEYFile(saveFileDialog.FileName, fileList.Items);

                    Cursor = Cursors.Default;

                    // EVO 32
                    StatusBarLogManager.ShowEvent(this, _STATUS_SUCCESS_GENERATE);
                }
                catch (Exception ex)
                {
                    MessageBoxes.ShowError(this, ex);
                }
            }
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton de recherche
            if (fileList.Items.Count == 0)
                return;

            // On utilise la boite de dialogue de recherche
            StringCollection columnNames = new StringCollection();
            columnNames.Add("#");
            columnNames.Add("Result");
            columnNames.Add("File Name");
            columnNames.Add("MAP Identifier(s)");

            ListViewSearchBox searchBox = ListViewSearchBox.GetInstance(fileList, "", columnNames);

            searchBox.Show(this);
        }

        private void copyButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton 'Copy Ids'
            if (fileList.SelectedItems.Count != 1)
                return;

            try
            {
                string ids = fileList.SelectedItems[0].SubItems[3].Text;

                Clipboard.SetText(ids, TextDataFormat.Text);

                StatusBarLogManager.ShowEvent(this, _STATUS_COPY_DONE);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }
        #endregion

        #region Méthodes privées
        /// <summary>
        /// Met à jour la liste de fichiers
        /// </summary>
        private void _UpdateFileList()
        {
            int foundIds = 0;

            // Vidage de liste
            fileList.Items.Clear();
            fileCountLabel.Text = _LABEL_START_TEXT;

            foreach (string anotherKey in idList.Keys)
            {
                ArrayList list = idList[anotherKey];

                // Construction de la liste d'identifiants
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < list.Count; i++)
                {
                    sb.Append(list[i]);
                    if (i < list.Count - 1)
                    {
                        sb.Append(" - ");
                    }
                }

                // Résultat (OK si 1 seul identifiant trouvé)
                string result = "";

                if (list.Count == 1)
                {
                    result = "OK";
                    foundIds++;
                }

                // Ajout de ligne
                ListViewItem anotherMainItem = new ListViewItem(string.Format("{0}", fileList.Items.Count + 1));
                anotherMainItem.SubItems.Add(string.Format("{0}", result));
                anotherMainItem.SubItems.Add(string.Format("{0}", anotherKey.Substring(tduPath.Text.Length)));
                anotherMainItem.SubItems.Add(string.Format("{0}", sb));
                fileList.Items.Add(anotherMainItem);
            }

            // Stats
            double coverage = Math.Round( (double) foundIds / leMAP.EntryCount * 100, 2);

            // Mise à jour du nombre d'entrées
            fileCountLabel.Text = string.Format(_LABEL_COUNT_TEXT, idList.Count, leMAP.EntryCount, foundIds, coverage);
        }

        /// <summary>
        /// Génère le fichier de clés correspondant à la liste obtenue
        /// </summary>
        /// <param name="fileName">File to generate</param>
        /// <param name="items">Item list to generate keys from</param>
        private static void _GenerateKEYFile(string fileName, ListView.ListViewItemCollection items)
        {
            // Création du fichier
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(Xml2.XML_1_0_HEADER + "<tduMapKey></tduMapKey>");

            // Elément racine
            XmlElement noeudRacine = doc.DocumentElement;

            // Parcours de la collection d'items pour ajouter les noeuds enfants
            foreach (ListViewItem lvi in items)
            {
                // On n'ajoute que les items marquées "OK"
                string result = lvi.SubItems[1].Text;

                if (result.Equals("OK"))
                {
                    // Nouvel élément
                    XmlNode unElement = doc.CreateElement("entry");

                    // Attribut :  identifiant
                    string identifier = lvi.SubItems[3].Text;
                    XmlAttribute attributId = doc.CreateAttribute("mapId");
                    attributId.Value = identifier;
                    unElement.Attributes.Append(attributId);

                    // Attribut :  nom du fichier
                    string file = lvi.SubItems[2].Text;
                    XmlAttribute attributNomFichier = doc.CreateAttribute("fileName");
                    attributNomFichier.Value = file;
                    unElement.Attributes.Append(attributNomFichier);

                    // On rattache cet élément à la racine
                    noeudRacine.AppendChild(unElement);
                }
            }

            // Enregistrement du fichier XML
            doc.Save(fileName);
        }
        #endregion
    }
}