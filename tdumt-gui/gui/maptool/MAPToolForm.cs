using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DjeFramework1.Common.GUI.Dialogs;
using DjeFramework1.Common.GUI.Traces;
using DjeFramework1.Common.Types;
using DjeFramework1.Common.Types.Forms;
using TDUModdingLibrary.fileformats;
using TDUModdingLibrary.fileformats.banks;
using TDUModdingLibrary.support.constants;
using TDUModdingTools.common;

namespace TDUModdingTools.gui.maptool
{
    public partial class MAPToolForm : Form
    {
        #region Constants
        /// <summary>
        /// Libellé d'information lorsque la recherche n'a pas été initialisée
        /// </summary>
        private const string _ENTRY_COUNT_START_TEXT = "Please select a MAP file.";

        /// <summary>
        /// Libellé d'information lorsque le fichier MAP a été chargé
        /// </summary>
        private const string _ENTRY_COUNT_TEXT = "File loaded with {0} entries.";

        /// <summary>
        /// Libellé d'information pour la boîte de recherche
        /// </summary>
        private const string _LABEL_SEARCH = "Find a MAP entry...";

        /// <summary>
        /// Message d'info lorsque l'on souhaite rechercher une clé mais qu'aucun MAP n'est chargé
        /// </summary>
        private const string _INFO_MAP_NOT_LOADED = "Please load a MAP file first.";

        /// <summary>
        /// Message apparaissant dans la barre d'état lorsque l'opération "Fix All" a réussi
        /// </summary>
        private const string _STATUS_FIX_ALL_SUCCESS = "{0} entry(ies) successfully fixed. MAP file saved.";

        /// <summary>
        /// Message apparaissant dans la barre d'état lorsque l'opération "Fix All" a réussi
        /// </summary>
        private const string _STATUS_CHANGE_SUCCESS = "Entry successfully updated. MAP file saved.";

        /// <summary>
        /// Message apparaissant dans la barre d'état lorsque l'opération "Fix" a réussi
        /// </summary>
        private const string _STATUS_FIX_SUCCESS = "Entry successfully fixed. MAP file saved.";

        /// <summary>
        /// Message apparaissant dans la barre d'état lorsque l'opération "Fix" a échoué car aucun fichier correspondant
        /// </summary>
        private const string _STATUS_FIX_FAILED = "Unknown file for selected entry. Unable to fix it.";
        #endregion

        #region Attributs
        /// <summary>
        /// Le fichier MAP rattaché
        /// </summary>
        private MAP leMAP;
        #endregion

        /// <summary>
        /// Constructeur paramétré. Le fichier spécifié est immédiatement chargé
        /// </summary>
        /// <param name="fileToEdit">Nom du fichier MAP à ouvrir</param>
        public MAPToolForm(string fileToEdit)
        {
            InitializeComponent();

            entryCountLabel.Text = _ENTRY_COUNT_START_TEXT;

            // EVO 32 : support StatusLog
            StatusBarLogManager.AddNewLog(this, toolStripStatusLabel);

            // EVO 18 : clé par défaut
            inputKEYFilePath.Text = AppConstants.FOLDER_XML + AppConstants.FILE_MAP_KEY;

            if (fileToEdit != null)
                // Fichier MAP paramétré
                inputFilePath.Text = fileToEdit;
            else
                // Fichier MAP par défaut...
                inputFilePath.Text = Program.ApplicationSettings.TduMainFolder + FileConstants.FOLDER_BNK + MAP.FILE_MAP;

            // Ouverture
            refreshButton_Click(this, new EventArgs());
        }

        #region Méthodes privées
        /// <summary>
        /// Met à jour la liste des entrées
        /// </summary>
        /// <param name="keepSelection">true pour replacer le sélecteur à l'emplacement d'origine, false sinon</param>
        private void _UpdateEntryList(bool keepSelection)
        {
            // Sauvegarde de l'indice sélectionné
            if (keepSelection)
            {
                ListView2.StoreSelectedIndex(entryList);
            }

            // Vidage de liste
            entryList.Items.Clear();
            entryCountLabel.Text = _ENTRY_COUNT_START_TEXT;

            // Utilisation de la clé XML
            if (!string.IsNullOrEmpty(inputKEYFilePath.Text))
            {
                leMAP.SetFileNamesFromKey(inputKEYFilePath.Text);
            }

            // Lecture de la structure
            foreach (UInt32 anotherKey in leMAP.EntryList.Keys)
            {
                MAP.Entry anotherEntry = leMAP.EntryList[anotherKey];
                ListViewItem anotherItem = new ListViewItem(string.Format("{0}", anotherEntry.entryNumber + 1));

                // EVO 13 : recherche de la taille du fichier sur disque
                FileInfo fi = null;
                try
                {
                    if (anotherEntry.fileName != null)
                    {
                        string actualFileName = Program.ApplicationSettings.TduMainFolder + @"\" + anotherEntry.fileName;
                        fi = new FileInfo(actualFileName);
                    }
                }
                catch (Exception ex)
                {
                    // Erreur silencieuse ici
                    Exception2.PrintStackTrace(ex);
                }

                anotherItem.SubItems.Add(string.Format("{0}", anotherEntry.fileId));
                anotherItem.SubItems.Add(string.Format("{0}", anotherEntry.firstSize));
                anotherItem.SubItems.Add(string.Format("{0}", anotherEntry.secondSize));
                anotherItem.SubItems.Add(string.Format("{0}", anotherEntry.address));
                anotherItem.SubItems.Add(string.Format("{0}", anotherEntry.fileName));
                if (fi != null && fi.Exists)
                {
                    // EVO 13 : un fichier dont la taille ne correspond pas est affiché en rouge
                    if (fi.Length != anotherEntry.firstSize || fi.Length != anotherEntry.secondSize)
                    {
                        anotherItem.ForeColor = GuiConstants.COLOR_INVALID_ITEM;
                    }
                    anotherItem.SubItems.Add(string.Format("{0}", fi.Length));
                }
                else
                {
                    anotherItem.SubItems.Add("");
                }
                entryList.Items.Add(anotherItem);
            }

            // Nombre d'entrées
            entryCountLabel.Text = string.Format(_ENTRY_COUNT_TEXT, leMAP.EntryList.Keys.Count);

            // Restauration de la sélection
            if (keepSelection)
            {
                ListView2.RestoreSelectedIndex(entryList);
            }
        }

        /// <summary>
        /// Met à jour une seule entrée dans la liste
        /// </summary>
        /// <param name="lvItem">élément de la liste</param>
        /// <param name="entryId">identifiant de l'entrée mise à jour</param>
        private void _UpdateSingleEntry(ListViewItem lvItem, uint entryId)
        {
            if (lvItem == null)
            {
                return;
            }

            MAP.Entry entry = leMAP.EntryList[entryId];

            lvItem.ForeColor = SystemColors.WindowText;
            lvItem.SubItems[1].Text = string.Format("{0}",entry.fileId);
            lvItem.SubItems[2].Text = string.Format("{0}",entry.firstSize);
            lvItem.SubItems[3].Text = string.Format("{0}",entry.secondSize);
            lvItem.SubItems[4].Text = string.Format("{0}",entry.address);
            lvItem.SubItems[5].Text = string.Format("{0}",entry.fileName);

            // EVO 13 : un fichier dont la taille ne correspond pas est affiché en rouge
            try
            {
                uint actualSize = uint.Parse(lvItem.SubItems[6].Text);
                if (actualSize != entry.firstSize || actualSize != entry.secondSize)
                {
                    lvItem.ForeColor = GuiConstants.COLOR_INVALID_ITEM;
                }
            }
            catch (Exception ex)
            {
                // Silent exception
                Exception2.PrintStackTrace(ex);
            }
        }

        /// <summary>
        /// Répare l'entrée spécifiée avec la taille spécifiée
        /// </summary>
        /// <param name="entryId">L'identifiant de l'entrée</param>
        /// <param name="actualFileSize">La taille à appliquer</param>
        private void _FixEntry(uint entryId, uint actualFileSize)
        {
            if (entryId == 0 || actualFileSize == 0)
            {
                return;
            }

            leMAP.UpdateEntrySizes(entryId, actualFileSize, actualFileSize);
        }

        /// <summary>
        /// Répare toutes les entrées invalides
        /// </summary>
        private void _FixInvalidEntries(IEnumerable<ListViewItem> invalidItems)
        {
            Collection<uint> idList = new Collection<uint>();
            Collection<uint> sizeList = new Collection<uint>();

            // Parcours des entrées invalides
            foreach (ListViewItem anotherItem in invalidItems)
            {
                uint fileId = uint.Parse(anotherItem.SubItems[1].Text);
                uint size = uint.Parse(anotherItem.SubItems[6].Text);

                idList.Add(fileId);
                sizeList.Add(size);
            }

            // Mise à jour
            leMAP.UpdateEntrySizes(idList, sizeList, sizeList);
        }

        /// <summary>
        /// Retourne la liste d'items invalides dans le tableau
        /// </summary>
        /// <returns></returns>
        private Collection<ListViewItem> _GetInvalidEntries()
        {
            Collection<ListViewItem> listeRetour = new Collection<ListViewItem>();

            // Parcours du tableau
            foreach (ListViewItem anotherItem in entryList.Items)
            {
                try
                {
                    uint size1 = uint.Parse(anotherItem.SubItems[2].Text);
                    uint size2 = uint.Parse(anotherItem.SubItems[3].Text);

                    if (!string.IsNullOrEmpty(anotherItem.SubItems[6].Text))
                    {
                        uint actualSize = uint.Parse(anotherItem.SubItems[6].Text);

                        if (size1 != actualSize || size2 != actualSize)
                        {
                            listeRetour.Add(anotherItem);
                        }
                    }
                }
                catch (FormatException fex)
                {
                    // on passe à l'entrée suivante
                    Exception2.PrintStackTrace(fex);
                }
            }

            return listeRetour;
        }
        #endregion

        #region Evénements
        private void browseMAPButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = GuiConstants.FILTER_MAP_ALL_FILES;

            DialogResult dr = openFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                inputFilePath.Text = openFileDialog1.FileName;
            }
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            // Contrôle de la saisie ...
            if (string.IsNullOrEmpty(inputFilePath.Text))
            {
                return;
            }

            // Récupération des infos

            try
            {
                Cursor = Cursors.WaitCursor;

                leMAP = TduFile.GetFile(inputFilePath.Text) as MAP;

                if (leMAP != null)
                {
                    // Mise à jour des entrées
                    _UpdateEntryList(true);
                }

                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void modifyButton_Click(object sender, EventArgs e)
        {
            if (entryList.SelectedItems.Count == 0)
            {
                return;
            }

            // On récupère l'entrée sélectionnée
            try
            {
                UInt32 id = UInt32.Parse(entryList.SelectedItems[0].SubItems[1].Text);
                MAP.Entry entryToModify = leMAP.EntryList[id];

                // Affichage de la fenêtre de modification
                DialogResult dr = new SizeModifierForm(entryToModify, leMAP).ShowDialog(this);

                // Des modifications ?
                if (dr != DialogResult.Cancel)
                {
                    // EVO 30 : mise à jour de la ligne uniquement
                    _UpdateSingleEntry(entryList.SelectedItems[0], id);

                    Cursor = Cursors.Default;

                    // EVO 32
                    StatusBarLogManager.ShowEvent(this, _STATUS_CHANGE_SUCCESS);
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }

        }

        private void entryList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            modifyButton_Click(sender, new EventArgs());
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (leMAP == null || leMAP.EntryList == null || leMAP.EntryCount == 0)
                return;

            // On utilise la boite de dialogue de recherche
            StringCollection columnNames = new StringCollection();
            columnNames.Add("File Id");
            columnNames.Add("Size 1 (bytes)");
            columnNames.Add("Size 2 (bytes)");
            columnNames.Add("File name (from KEY file)");
            columnNames.Add("Actual size (bytes)");

            ListViewSearchBox searchBox = ListViewSearchBox.GetInstance(entryList, _LABEL_SEARCH, columnNames);

            searchBox.Show(this);
        }

        private void fileNamesButton_Click(object sender, EventArgs e)
        {
            if (leMAP == null)
            {
                MessageBoxes.ShowInfo(this, _INFO_MAP_NOT_LOADED);
                return;
            }

            // Affiche la fenêtre de l'outil
            new FileListingForm(leMAP, inputKEYFilePath.Text).Show(this);
        }

        private void browseKEYButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Multiselect = false;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = GuiConstants.FILTER_XML_ALL_FILES;

            DialogResult dr = openFileDialog1.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                inputKEYFilePath.Text = openFileDialog1.FileName;
            }
        }

        private void fixButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton "Fix entry"
            if (entryList.SelectedItems.Count != 1)
            {
                return;
            }

            try
            {
                // Récupération des infos
                ListViewItem selectedItem = entryList.SelectedItems[0];
                uint entryId = uint.Parse(selectedItem.SubItems[1].Text);

                if (! string.IsNullOrEmpty(selectedItem.SubItems[6].Text))
                {
                    uint actualFileSize = uint.Parse(selectedItem.SubItems[6].Text);

                    _FixEntry(entryId, actualFileSize);

                    // EVO 30 : Refresh
                    _UpdateSingleEntry(selectedItem, entryId);

                    // EVO 32
                    StatusBarLogManager.ShowEvent(this, _STATUS_FIX_SUCCESS);
                }
                else
                {
                    // EVO 32
                    StatusBarLogManager.ShowEvent(this, _STATUS_FIX_FAILED);
                }
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void fixAllButton_Click(object sender, EventArgs e)
        {
            // Clic sur le bouton "Fix all entries"

            // Récupération des entrées invalides
            Collection<ListViewItem> invalidEntries = _GetInvalidEntries();
            if (invalidEntries.Count == 0)
            {
                return;
            }

            Cursor = Cursors.WaitCursor;

            try
            {
                _FixInvalidEntries(invalidEntries);

                // EVO 30 : Refresh
                foreach (ListViewItem anotherLvi in invalidEntries)
                {
                    uint entryId = uint.Parse(anotherLvi.SubItems[1].Text);

                    _UpdateSingleEntry(anotherLvi, entryId);
                }

                Cursor = Cursors.Default;

                // EVO 32
                string message = string.Format(_STATUS_FIX_ALL_SUCCESS, invalidEntries.Count);

                StatusBarLogManager.ShowEvent(this,message);
            }
            catch (Exception ex)
            {
                MessageBoxes.ShowError(this, ex);
            }
        }

        private void MAPToolForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Nettoyage log
            StatusBarLogManager.RemoveLog(this);
        }
        #endregion
    }
}