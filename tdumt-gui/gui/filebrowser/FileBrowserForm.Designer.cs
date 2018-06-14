namespace TDUModdingTools.gui.filebrowser
{
    partial class FileBrowserForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Avatar");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("CHARACTERS");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("\\", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileBrowserForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.vSplitContainer = new System.Windows.Forms.SplitContainer();
            this.folderTreeView = new System.Windows.Forms.TreeView();
            this.smallImageList = new System.Windows.Forms.ImageList(this.components);
            this.hSplitContainer = new System.Windows.Forms.SplitContainer();
            this.bnkListView = new System.Windows.Forms.ListView();
            this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.sizeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.typeColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.largeImageList = new System.Windows.Forms.ImageList(this.components);
            this.bnkStatusStrip = new System.Windows.Forms.StatusStrip();
            this.bnkStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bnkStatusLabelCountSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.contentTreeView = new System.Windows.Forms.TreeView();
            this.contentListView = new System.Windows.Forms.ListView();
            this.contentsStatusStrip = new System.Windows.Forms.StatusStrip();
            this.contentsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bnkToolStrip = new System.Windows.Forms.ToolStrip();
            this.titleToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.loadBnkToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.fileOpenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fileEditToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fileExtractToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fileRenameToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fileDeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.fileReplaceParentToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.keepNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.fileViewToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.fileViewFlatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileViewHierarchicalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSearchToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.fileSearchToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.mainToolStrip = new System.Windows.Forms.ToolStrip();
            this.refreshTreeToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bnkOpenToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.bnkBackupToolsStripButton = new System.Windows.Forms.ToolStripButton();
            this.bnkRestoreToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.bnkDeleteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolsToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.disableFSCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripDropDownButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.bnkManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editTasksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.largeIconsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.detailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileWatcher = new System.IO.FileSystemWatcher();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.vSplitContainer.Panel1.SuspendLayout();
            this.vSplitContainer.Panel2.SuspendLayout();
            this.vSplitContainer.SuspendLayout();
            this.hSplitContainer.Panel1.SuspendLayout();
            this.hSplitContainer.Panel2.SuspendLayout();
            this.hSplitContainer.SuspendLayout();
            this.bnkStatusStrip.SuspendLayout();
            this.contentsStatusStrip.SuspendLayout();
            this.bnkToolStrip.SuspendLayout();
            this.mainToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1008, 662);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.vSplitContainer);
            this.panel1.Controls.Add(this.mainToolStrip);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 654);
            this.panel1.TabIndex = 0;
            // 
            // vSplitContainer
            // 
            this.vSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.vSplitContainer.Name = "vSplitContainer";
            // 
            // vSplitContainer.Panel1
            // 
            this.vSplitContainer.Panel1.Controls.Add(this.folderTreeView);
            // 
            // vSplitContainer.Panel2
            // 
            this.vSplitContainer.Panel2.Controls.Add(this.hSplitContainer);
            this.vSplitContainer.Size = new System.Drawing.Size(1000, 629);
            this.vSplitContainer.SplitterDistance = 200;
            this.vSplitContainer.SplitterWidth = 2;
            this.vSplitContainer.TabIndex = 1;
            // 
            // folderTreeView
            // 
            this.folderTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.folderTreeView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderTreeView.HideSelection = false;
            this.folderTreeView.ImageIndex = 1;
            this.folderTreeView.ImageList = this.smallImageList;
            this.folderTreeView.Location = new System.Drawing.Point(0, 0);
            this.folderTreeView.Name = "folderTreeView";
            treeNode1.Name = "Noeud1";
            treeNode1.Text = "Avatar";
            treeNode2.Name = "Noeud2";
            treeNode2.Text = "CHARACTERS";
            treeNode3.Name = "rootNode";
            treeNode3.Text = "\\";
            this.folderTreeView.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.folderTreeView.SelectedImageIndex = 0;
            this.folderTreeView.Size = new System.Drawing.Size(200, 629);
            this.folderTreeView.TabIndex = 0;
            this.folderTreeView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.folderTreeView_MouseClick);
            this.folderTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.folderTreeView_AfterSelect);
            this.folderTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.folderTreeView_KeyDown);
            // 
            // smallImageList
            // 
            this.smallImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("smallImageList.ImageStream")));
            this.smallImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.smallImageList.Images.SetKeyName(0, "folder-open_16.png");
            this.smallImageList.Images.SetKeyName(1, "folder-closed_16.png");
            this.smallImageList.Images.SetKeyName(2, "applications_16.png");
            this.smallImageList.Images.SetKeyName(3, "disc-media_16.png");
            this.smallImageList.Images.SetKeyName(4, "documents_16.png");
            this.smallImageList.Images.SetKeyName(5, "bnk-closed_16.png");
            this.smallImageList.Images.SetKeyName(6, "backup_16.png");
            this.smallImageList.Images.SetKeyName(7, "2db_16.png");
            this.smallImageList.Images.SetKeyName(8, "2dm_16.png");
            this.smallImageList.Images.SetKeyName(9, "3d_16.png");
            this.smallImageList.Images.SetKeyName(10, "wav_16.png");
            this.smallImageList.Images.SetKeyName(11, "database_16.png");
            this.smallImageList.Images.SetKeyName(12, "folder-packed-open_16.png");
            this.smallImageList.Images.SetKeyName(13, "folder-packed-closed_16.png");
            this.smallImageList.Images.SetKeyName(14, "folder-ext-open_16.png");
            this.smallImageList.Images.SetKeyName(15, "folder-ext-closed_16.png");
            this.smallImageList.Images.SetKeyName(16, "cam_16.png");
            // 
            // hSplitContainer
            // 
            this.hSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.hSplitContainer.Name = "hSplitContainer";
            this.hSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // hSplitContainer.Panel1
            // 
            this.hSplitContainer.Panel1.Controls.Add(this.bnkListView);
            this.hSplitContainer.Panel1.Controls.Add(this.bnkStatusStrip);
            // 
            // hSplitContainer.Panel2
            // 
            this.hSplitContainer.Panel2.Controls.Add(this.contentTreeView);
            this.hSplitContainer.Panel2.Controls.Add(this.contentListView);
            this.hSplitContainer.Panel2.Controls.Add(this.contentsStatusStrip);
            this.hSplitContainer.Panel2.Controls.Add(this.bnkToolStrip);
            this.hSplitContainer.Size = new System.Drawing.Size(798, 629);
            this.hSplitContainer.SplitterDistance = 355;
            this.hSplitContainer.SplitterWidth = 2;
            this.hSplitContainer.TabIndex = 0;
            // 
            // bnkListView
            // 
            this.bnkListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader,
            this.sizeColumnHeader,
            this.typeColumnHeader});
            this.bnkListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bnkListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnkListView.HideSelection = false;
            this.bnkListView.LargeImageList = this.largeImageList;
            this.bnkListView.Location = new System.Drawing.Point(0, 22);
            this.bnkListView.Name = "bnkListView";
            this.bnkListView.ShowItemToolTips = true;
            this.bnkListView.Size = new System.Drawing.Size(798, 333);
            this.bnkListView.SmallImageList = this.smallImageList;
            this.bnkListView.TabIndex = 0;
            this.bnkListView.UseCompatibleStateImageBehavior = false;
            this.bnkListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.bnkListView_MouseDoubleClick);
            this.bnkListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.bnkListView_MouseClick);
            this.bnkListView.SelectedIndexChanged += new System.EventHandler(this.bnkListView_SelectedIndexChanged);
            this.bnkListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bnkListView_KeyDown);
            // 
            // nameColumnHeader
            // 
            this.nameColumnHeader.Text = "Name";
            this.nameColumnHeader.Width = 200;
            // 
            // sizeColumnHeader
            // 
            this.sizeColumnHeader.Text = "Size (bytes)";
            this.sizeColumnHeader.Width = 75;
            // 
            // typeColumnHeader
            // 
            this.typeColumnHeader.Text = "Type";
            this.typeColumnHeader.Width = 200;
            // 
            // largeImageList
            // 
            this.largeImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("largeImageList.ImageStream")));
            this.largeImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.largeImageList.Images.SetKeyName(0, "folder-closed_32.png");
            this.largeImageList.Images.SetKeyName(1, "folder-open_32.png");
            this.largeImageList.Images.SetKeyName(2, "applications_32.png");
            this.largeImageList.Images.SetKeyName(3, "disc-media_32.png");
            this.largeImageList.Images.SetKeyName(4, "documents_32.png");
            this.largeImageList.Images.SetKeyName(5, "bnk-closed_32.png");
            this.largeImageList.Images.SetKeyName(6, "backup_32.png");
            this.largeImageList.Images.SetKeyName(7, "2db_32.png");
            this.largeImageList.Images.SetKeyName(8, "2dm_32.png");
            this.largeImageList.Images.SetKeyName(9, "3d_32.png");
            this.largeImageList.Images.SetKeyName(10, "wav_32.png");
            this.largeImageList.Images.SetKeyName(11, "database_32.png");
            this.largeImageList.Images.SetKeyName(12, "blank.png");
            this.largeImageList.Images.SetKeyName(13, "blank.png");
            this.largeImageList.Images.SetKeyName(14, "blank.png");
            this.largeImageList.Images.SetKeyName(15, "blank.png");
            this.largeImageList.Images.SetKeyName(16, "cam_32.png");
            // 
            // bnkStatusStrip
            // 
            this.bnkStatusStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.bnkStatusStrip.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnkStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bnkStatusLabel,
            this.bnkStatusLabelCountSize});
            this.bnkStatusStrip.Location = new System.Drawing.Point(0, 0);
            this.bnkStatusStrip.Name = "bnkStatusStrip";
            this.bnkStatusStrip.Size = new System.Drawing.Size(798, 22);
            this.bnkStatusStrip.SizingGrip = false;
            this.bnkStatusStrip.TabIndex = 1;
            this.bnkStatusStrip.Text = "statusStrip1";
            // 
            // bnkStatusLabel
            // 
            this.bnkStatusLabel.IsLink = true;
            this.bnkStatusLabel.Name = "bnkStatusLabel";
            this.bnkStatusLabel.Size = new System.Drawing.Size(0, 17);
            this.bnkStatusLabel.ToolTipText = "Click to view in explorer (Ctrl+G)";
            this.bnkStatusLabel.Click += new System.EventHandler(this.bnkStatusLabel_Click);
            // 
            // bnkStatusLabelCountSize
            // 
            this.bnkStatusLabelCountSize.Name = "bnkStatusLabelCountSize";
            this.bnkStatusLabelCountSize.Size = new System.Drawing.Size(0, 17);
            // 
            // contentTreeView
            // 
            this.contentTreeView.AllowDrop = true;
            this.contentTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.contentTreeView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contentTreeView.HideSelection = false;
            this.contentTreeView.ImageIndex = 0;
            this.contentTreeView.ImageList = this.smallImageList;
            this.contentTreeView.LabelEdit = true;
            this.contentTreeView.Location = new System.Drawing.Point(0, 25);
            this.contentTreeView.Name = "contentTreeView";
            this.contentTreeView.SelectedImageIndex = 0;
            this.contentTreeView.ShowNodeToolTips = true;
            this.contentTreeView.Size = new System.Drawing.Size(798, 224);
            this.contentTreeView.TabIndex = 1;
            this.contentTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.contentTreeView_NodeMouseDoubleClick);
            this.contentTreeView.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.contentTreeView_AfterLabelEdit);
            this.contentTreeView.DragDrop += new System.Windows.Forms.DragEventHandler(this.contentListView_DragDrop);
            this.contentTreeView.DragEnter += new System.Windows.Forms.DragEventHandler(this.contentListView_DragEnter);
            this.contentTreeView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.contentListView_KeyUp);
            this.contentTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.contentTreeView_NodeMouseClick);
            this.contentTreeView.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.contentTreeView_BeforeLabelEdit);
            this.contentTreeView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.contentListView_KeyDown);
            this.contentTreeView.MouseLeave += new System.EventHandler(this.contentListView_MouseLeave);
            // 
            // contentListView
            // 
            this.contentListView.AllowDrop = true;
            this.contentListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.contentListView.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contentListView.HideSelection = false;
            this.contentListView.LabelEdit = true;
            this.contentListView.Location = new System.Drawing.Point(0, 25);
            this.contentListView.Name = "contentListView";
            this.contentListView.ShowItemToolTips = true;
            this.contentListView.Size = new System.Drawing.Size(798, 224);
            this.contentListView.SmallImageList = this.smallImageList;
            this.contentListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.contentListView.TabIndex = 1;
            this.contentListView.UseCompatibleStateImageBehavior = false;
            this.contentListView.View = System.Windows.Forms.View.List;
            this.contentListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.contentListView_MouseDoubleClick);
            this.contentListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.contentListView_MouseClick);
            this.contentListView.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.contentListView_AfterLabelEdit);
            this.contentListView.DragDrop += new System.Windows.Forms.DragEventHandler(this.contentListView_DragDrop);
            this.contentListView.DragEnter += new System.Windows.Forms.DragEventHandler(this.contentListView_DragEnter);
            this.contentListView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.contentListView_KeyUp);
            this.contentListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.contentListView_KeyDown);
            this.contentListView.MouseLeave += new System.EventHandler(this.contentListView_MouseLeave);
            // 
            // contentsStatusStrip
            // 
            this.contentsStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsStatusLabel});
            this.contentsStatusStrip.Location = new System.Drawing.Point(0, 250);
            this.contentsStatusStrip.Name = "contentsStatusStrip";
            this.contentsStatusStrip.Size = new System.Drawing.Size(798, 22);
            this.contentsStatusStrip.TabIndex = 0;
            this.contentsStatusStrip.Text = "statusStrip1";
            // 
            // contentsStatusLabel
            // 
            this.contentsStatusLabel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contentsStatusLabel.Name = "contentsStatusLabel";
            this.contentsStatusLabel.Size = new System.Drawing.Size(337, 17);
            this.contentsStatusLabel.Text = "No BNK file selected. You may also drag&&drop a file to the list above.";
            // 
            // bnkToolStrip
            // 
            this.bnkToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bnkToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.titleToolStripLabel,
            this.loadBnkToolStripButton,
            this.toolStripSeparator7,
            this.fileOpenToolStripButton,
            this.fileEditToolStripButton,
            this.fileExtractToolStripButton,
            this.fileRenameToolStripButton,
            this.fileDeleteToolStripButton,
            this.toolStripSeparator8,
            this.fileReplaceParentToolStripDropDownButton,
            this.toolStripSeparator3,
            this.fileViewToolStripDropDownButton,
            this.fileSearchToolStripTextBox,
            this.fileSearchToolStripLabel});
            this.bnkToolStrip.Location = new System.Drawing.Point(0, 0);
            this.bnkToolStrip.Name = "bnkToolStrip";
            this.bnkToolStrip.Size = new System.Drawing.Size(798, 25);
            this.bnkToolStrip.TabIndex = 2;
            this.bnkToolStrip.Text = "toolStrip1";
            // 
            // titleToolStripLabel
            // 
            this.titleToolStripLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleToolStripLabel.Name = "titleToolStripLabel";
            this.titleToolStripLabel.Size = new System.Drawing.Size(88, 22);
            this.titleToolStripLabel.Text = "BNK Manager";
            // 
            // loadBnkToolStripButton
            // 
            this.loadBnkToolStripButton.AutoToolTip = false;
            this.loadBnkToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.loadBnkToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadBnkToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("loadBnkToolStripButton.Image")));
            this.loadBnkToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.loadBnkToolStripButton.Name = "loadBnkToolStripButton";
            this.loadBnkToolStripButton.Size = new System.Drawing.Size(75, 22);
            this.loadBnkToolStripButton.Text = "Load BNK...";
            this.loadBnkToolStripButton.ToolTipText = "Loads a BNK file from disk (Ctrl+L)";
            this.loadBnkToolStripButton.Click += new System.EventHandler(this.fileLoadBnkToolStripButton_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // fileOpenToolStripButton
            // 
            this.fileOpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileOpenToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileOpenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("fileOpenToolStripButton.Image")));
            this.fileOpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileOpenToolStripButton.Name = "fileOpenToolStripButton";
            this.fileOpenToolStripButton.Size = new System.Drawing.Size(41, 22);
            this.fileOpenToolStripButton.Text = "Open";
            this.fileOpenToolStripButton.ToolTipText = "Opens selected file for read-only with default program (double click)";
            this.fileOpenToolStripButton.Click += new System.EventHandler(this.fileOpenToolStripButton_Click);
            // 
            // fileEditToolStripButton
            // 
            this.fileEditToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileEditToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileEditToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("fileEditToolStripButton.Image")));
            this.fileEditToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileEditToolStripButton.Name = "fileEditToolStripButton";
            this.fileEditToolStripButton.Size = new System.Drawing.Size(32, 22);
            this.fileEditToolStripButton.Text = "Edit";
            this.fileEditToolStripButton.ToolTipText = "Opens selected file with default editor and creates a new task (Shift+double clic" +
                "k)";
            this.fileEditToolStripButton.Click += new System.EventHandler(this.fileEditToolStripButton_Click);
            // 
            // fileExtractToolStripButton
            // 
            this.fileExtractToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileExtractToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileExtractToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("fileExtractToolStripButton.Image")));
            this.fileExtractToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileExtractToolStripButton.Name = "fileExtractToolStripButton";
            this.fileExtractToolStripButton.Size = new System.Drawing.Size(62, 22);
            this.fileExtractToolStripButton.Text = "Extract...";
            this.fileExtractToolStripButton.ToolTipText = "Extracts selected packed file(s) (Ctrl+E)";
            this.fileExtractToolStripButton.Click += new System.EventHandler(this.fileExtractToolStripButton_Click);
            // 
            // fileRenameToolStripButton
            // 
            this.fileRenameToolStripButton.AutoToolTip = false;
            this.fileRenameToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileRenameToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileRenameToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("fileRenameToolStripButton.Image")));
            this.fileRenameToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileRenameToolStripButton.Name = "fileRenameToolStripButton";
            this.fileRenameToolStripButton.Size = new System.Drawing.Size(55, 22);
            this.fileRenameToolStripButton.Text = "Rename";
            this.fileRenameToolStripButton.ToolTipText = "Changes name of selected packed file (F2)";
            // 
            // fileDeleteToolStripButton
            // 
            this.fileDeleteToolStripButton.AutoToolTip = false;
            this.fileDeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileDeleteToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileDeleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("fileDeleteToolStripButton.Image")));
            this.fileDeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileDeleteToolStripButton.Name = "fileDeleteToolStripButton";
            this.fileDeleteToolStripButton.Size = new System.Drawing.Size(47, 22);
            this.fileDeleteToolStripButton.Text = "Delete";
            this.fileDeleteToolStripButton.ToolTipText = "Removes selected packed file to save space (Del)";
            this.fileDeleteToolStripButton.Visible = false;
            this.fileDeleteToolStripButton.Click += new System.EventHandler(this.fileDeleteToolStripButton_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 25);
            // 
            // fileReplaceParentToolStripDropDownButton
            // 
            this.fileReplaceParentToolStripDropDownButton.AutoToolTip = false;
            this.fileReplaceParentToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileReplaceParentToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.keepNameToolStripMenuItem,
            this.changeNameToolStripMenuItem});
            this.fileReplaceParentToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileReplaceParentToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("fileReplaceParentToolStripDropDownButton.Image")));
            this.fileReplaceParentToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileReplaceParentToolStripDropDownButton.Name = "fileReplaceParentToolStripDropDownButton";
            this.fileReplaceParentToolStripDropDownButton.Size = new System.Drawing.Size(62, 22);
            this.fileReplaceParentToolStripDropDownButton.Text = "Replace";
            this.fileReplaceParentToolStripDropDownButton.ToolTipText = "Replaces selected packed file with a file on disk";
            // 
            // keepNameToolStripMenuItem
            // 
            this.keepNameToolStripMenuItem.Name = "keepNameToolStripMenuItem";
            this.keepNameToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.keepNameToolStripMenuItem.Text = "Keep name...";
            this.keepNameToolStripMenuItem.ToolTipText = "Keeps original file name (Ctrl+R)";
            this.keepNameToolStripMenuItem.Click += new System.EventHandler(this.keepNameToolStripMenuItem_Click);
            // 
            // changeNameToolStripMenuItem
            // 
            this.changeNameToolStripMenuItem.Name = "changeNameToolStripMenuItem";
            this.changeNameToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.changeNameToolStripMenuItem.Text = "Change name...";
            this.changeNameToolStripMenuItem.ToolTipText = "Uses name of replacing file (Ctrl+Shift+R)";
            this.changeNameToolStripMenuItem.Click += new System.EventHandler(this.changeNameToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // fileViewToolStripDropDownButton
            // 
            this.fileViewToolStripDropDownButton.AutoToolTip = false;
            this.fileViewToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.fileViewToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileViewFlatToolStripMenuItem,
            this.fileViewHierarchicalToolStripMenuItem});
            this.fileViewToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileViewToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("fileViewToolStripDropDownButton.Image")));
            this.fileViewToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fileViewToolStripDropDownButton.Name = "fileViewToolStripDropDownButton";
            this.fileViewToolStripDropDownButton.Size = new System.Drawing.Size(47, 22);
            this.fileViewToolStripDropDownButton.Text = "View";
            this.fileViewToolStripDropDownButton.ToolTipText = "Changes view options";
            // 
            // fileViewFlatToolStripMenuItem
            // 
            this.fileViewFlatToolStripMenuItem.Checked = true;
            this.fileViewFlatToolStripMenuItem.CheckOnClick = true;
            this.fileViewFlatToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fileViewFlatToolStripMenuItem.Name = "fileViewFlatToolStripMenuItem";
            this.fileViewFlatToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.fileViewFlatToolStripMenuItem.Text = "Flat";
            this.fileViewFlatToolStripMenuItem.ToolTipText = "Simple layout (recommended)";
            this.fileViewFlatToolStripMenuItem.Click += new System.EventHandler(this.fileViewFlatToolStripMenuItem_Click);
            // 
            // fileViewHierarchicalToolStripMenuItem
            // 
            this.fileViewHierarchicalToolStripMenuItem.CheckOnClick = true;
            this.fileViewHierarchicalToolStripMenuItem.Name = "fileViewHierarchicalToolStripMenuItem";
            this.fileViewHierarchicalToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.fileViewHierarchicalToolStripMenuItem.Text = "Hierarchical";
            this.fileViewHierarchicalToolStripMenuItem.ToolTipText = "Real layout (advanced)";
            this.fileViewHierarchicalToolStripMenuItem.Click += new System.EventHandler(this.fileViewHierarchicalToolStripMenuItem_Click);
            // 
            // fileSearchToolStripTextBox
            // 
            this.fileSearchToolStripTextBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.fileSearchToolStripTextBox.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileSearchToolStripTextBox.Name = "fileSearchToolStripTextBox";
            this.fileSearchToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            this.fileSearchToolStripTextBox.ToolTipText = "Searches for packed file...";
            this.fileSearchToolStripTextBox.TextChanged += new System.EventHandler(this.fileSearchToolStripTextBox_TextChanged);
            // 
            // fileSearchToolStripLabel
            // 
            this.fileSearchToolStripLabel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.fileSearchToolStripLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileSearchToolStripLabel.Name = "fileSearchToolStripLabel";
            this.fileSearchToolStripLabel.Size = new System.Drawing.Size(48, 22);
            this.fileSearchToolStripLabel.Text = "Search:";
            // 
            // mainToolStrip
            // 
            this.mainToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshTreeToolStripButton,
            this.toolStripSeparator1,
            this.bnkOpenToolStripButton,
            this.bnkBackupToolsStripButton,
            this.bnkRestoreToolStripButton,
            this.bnkDeleteToolStripButton,
            this.toolStripSeparator2,
            this.toolsToolStripDropDownButton,
            this.viewToolStripDropDownButton});
            this.mainToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mainToolStrip.Name = "mainToolStrip";
            this.mainToolStrip.Size = new System.Drawing.Size(1000, 25);
            this.mainToolStrip.TabIndex = 0;
            this.mainToolStrip.Text = "toolStrip1";
            // 
            // refreshTreeToolStripButton
            // 
            this.refreshTreeToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.refreshTreeToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshTreeToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshTreeToolStripButton.Image")));
            this.refreshTreeToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.refreshTreeToolStripButton.Name = "refreshTreeToolStripButton";
            this.refreshTreeToolStripButton.Size = new System.Drawing.Size(52, 22);
            this.refreshTreeToolStripButton.Text = "Refresh";
            this.refreshTreeToolStripButton.ToolTipText = "Actualizes files and folders (F5)";
            this.refreshTreeToolStripButton.Click += new System.EventHandler(this.refreshTreeToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bnkOpenToolStripButton
            // 
            this.bnkOpenToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnkOpenToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnkOpenToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("bnkOpenToolStripButton.Image")));
            this.bnkOpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bnkOpenToolStripButton.Name = "bnkOpenToolStripButton";
            this.bnkOpenToolStripButton.Size = new System.Drawing.Size(41, 22);
            this.bnkOpenToolStripButton.Text = "Open";
            this.bnkOpenToolStripButton.ToolTipText = "Opens selected file (double click)";
            this.bnkOpenToolStripButton.Click += new System.EventHandler(this.bnkOpenToolStripButton_Click);
            // 
            // bnkBackupToolsStripButton
            // 
            this.bnkBackupToolsStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnkBackupToolsStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnkBackupToolsStripButton.Image = ((System.Drawing.Image)(resources.GetObject("bnkBackupToolsStripButton.Image")));
            this.bnkBackupToolsStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bnkBackupToolsStripButton.Name = "bnkBackupToolsStripButton";
            this.bnkBackupToolsStripButton.Size = new System.Drawing.Size(50, 22);
            this.bnkBackupToolsStripButton.Text = "Backup";
            this.bnkBackupToolsStripButton.ToolTipText = "Makes a quick backup of selected file(s) (Ctrl+B)";
            this.bnkBackupToolsStripButton.Click += new System.EventHandler(this.bnkBackupToolsStripButton_Click);
            // 
            // bnkRestoreToolStripButton
            // 
            this.bnkRestoreToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnkRestoreToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnkRestoreToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("bnkRestoreToolStripButton.Image")));
            this.bnkRestoreToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bnkRestoreToolStripButton.Name = "bnkRestoreToolStripButton";
            this.bnkRestoreToolStripButton.Size = new System.Drawing.Size(53, 22);
            this.bnkRestoreToolStripButton.Text = "Restore";
            this.bnkRestoreToolStripButton.ToolTipText = "Restores file from selected backup (Ctrl+N)";
            this.bnkRestoreToolStripButton.Click += new System.EventHandler(this.bnkRestoreToolStripButton_Click);
            // 
            // bnkDeleteToolStripButton
            // 
            this.bnkDeleteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.bnkDeleteToolStripButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bnkDeleteToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("bnkDeleteToolStripButton.Image")));
            this.bnkDeleteToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bnkDeleteToolStripButton.Name = "bnkDeleteToolStripButton";
            this.bnkDeleteToolStripButton.Size = new System.Drawing.Size(47, 22);
            this.bnkDeleteToolStripButton.Text = "Delete";
            this.bnkDeleteToolStripButton.ToolTipText = "Erases selected file(s) (Del)";
            this.bnkDeleteToolStripButton.Click += new System.EventHandler(this.bnkDeleteToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolsToolStripDropDownButton
            // 
            this.toolsToolStripDropDownButton.AutoToolTip = false;
            this.toolsToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolsToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.disableFSCToolStripMenuItem,
            this.toolStripSeparator5,
            this.settingsToolStripMenuItem});
            this.toolsToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolsToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("toolsToolStripDropDownButton.Image")));
            this.toolsToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolsToolStripDropDownButton.Name = "toolsToolStripDropDownButton";
            this.toolsToolStripDropDownButton.Size = new System.Drawing.Size(49, 22);
            this.toolsToolStripDropDownButton.Text = "Tools";
            // 
            // disableFSCToolStripMenuItem
            // 
            this.disableFSCToolStripMenuItem.Name = "disableFSCToolStripMenuItem";
            this.disableFSCToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.disableFSCToolStripMenuItem.Text = "Disable File Size Control";
            this.disableFSCToolStripMenuItem.Click += new System.EventHandler(this.disableFSCToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(197, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.settingsToolStripMenuItem.Text = "Settings...";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // viewToolStripDropDownButton
            // 
            this.viewToolStripDropDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.viewToolStripDropDownButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bnkManagerToolStripMenuItem,
            this.editTasksToolStripMenuItem,
            this.toolStripSeparator4,
            this.listToolStripMenuItem,
            this.largeIconsToolStripMenuItem,
            this.detailsToolStripMenuItem});
            this.viewToolStripDropDownButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.viewToolStripDropDownButton.Image = ((System.Drawing.Image)(resources.GetObject("viewToolStripDropDownButton.Image")));
            this.viewToolStripDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewToolStripDropDownButton.Name = "viewToolStripDropDownButton";
            this.viewToolStripDropDownButton.Size = new System.Drawing.Size(47, 22);
            this.viewToolStripDropDownButton.Text = "View";
            this.viewToolStripDropDownButton.ToolTipText = "Changes view options";
            // 
            // bnkManagerToolStripMenuItem
            // 
            this.bnkManagerToolStripMenuItem.Checked = true;
            this.bnkManagerToolStripMenuItem.CheckOnClick = true;
            this.bnkManagerToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bnkManagerToolStripMenuItem.Name = "bnkManagerToolStripMenuItem";
            this.bnkManagerToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad1)));
            this.bnkManagerToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.bnkManagerToolStripMenuItem.Text = "BNK Manager";
            this.bnkManagerToolStripMenuItem.Click += new System.EventHandler(this.bnkManagerToolStripMenuItem_Click);
            // 
            // editTasksToolStripMenuItem
            // 
            this.editTasksToolStripMenuItem.CheckOnClick = true;
            this.editTasksToolStripMenuItem.Name = "editTasksToolStripMenuItem";
            this.editTasksToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad2)));
            this.editTasksToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.editTasksToolStripMenuItem.Text = "Edit Tasks";
            this.editTasksToolStripMenuItem.Click += new System.EventHandler(this.editTasksToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(228, 6);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.listToolStripMenuItem.Text = "Small icons";
            this.listToolStripMenuItem.Click += new System.EventHandler(this.listToolStripMenuItem_Click);
            // 
            // largeIconsToolStripMenuItem
            // 
            this.largeIconsToolStripMenuItem.Name = "largeIconsToolStripMenuItem";
            this.largeIconsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.largeIconsToolStripMenuItem.Text = "Large icons";
            this.largeIconsToolStripMenuItem.Click += new System.EventHandler(this.iconsToolStripMenuItem_Click);
            // 
            // detailsToolStripMenuItem
            // 
            this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
            this.detailsToolStripMenuItem.Size = new System.Drawing.Size(231, 22);
            this.detailsToolStripMenuItem.Text = "Details";
            this.detailsToolStripMenuItem.Click += new System.EventHandler(this.detailsToolStripMenuItem_Click);
            // 
            // fileWatcher
            // 
            this.fileWatcher.EnableRaisingEvents = true;
            this.fileWatcher.SynchronizingObject = this;
            // 
            // openFileDialog
            // 
            this.openFileDialog.AddExtension = false;
            // 
            // FileBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 662);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FileBrowserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TDUMT - File Browser";
            this.SizeChanged += new System.EventHandler(this.FileBrowserForm_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.FileBrowserForm_VisibleChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileBrowserForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.vSplitContainer.Panel1.ResumeLayout(false);
            this.vSplitContainer.Panel2.ResumeLayout(false);
            this.vSplitContainer.ResumeLayout(false);
            this.hSplitContainer.Panel1.ResumeLayout(false);
            this.hSplitContainer.Panel1.PerformLayout();
            this.hSplitContainer.Panel2.ResumeLayout(false);
            this.hSplitContainer.Panel2.PerformLayout();
            this.hSplitContainer.ResumeLayout(false);
            this.bnkStatusStrip.ResumeLayout(false);
            this.bnkStatusStrip.PerformLayout();
            this.contentsStatusStrip.ResumeLayout(false);
            this.contentsStatusStrip.PerformLayout();
            this.bnkToolStrip.ResumeLayout(false);
            this.bnkToolStrip.PerformLayout();
            this.mainToolStrip.ResumeLayout(false);
            this.mainToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileWatcher)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer vSplitContainer;
        private System.Windows.Forms.ToolStrip mainToolStrip;
        private System.Windows.Forms.ToolStripButton refreshTreeToolStripButton;
        private System.Windows.Forms.SplitContainer hSplitContainer;
        private System.Windows.Forms.TreeView folderTreeView;
        private System.Windows.Forms.StatusStrip contentsStatusStrip;
        private System.Windows.Forms.ListView bnkListView;
        private System.Windows.Forms.ListView contentListView;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton bnkOpenToolStripButton;
        private System.Windows.Forms.ToolStripButton bnkBackupToolsStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.StatusStrip bnkStatusStrip;
        private System.IO.FileSystemWatcher fileWatcher;
        private System.Windows.Forms.ToolStripStatusLabel bnkStatusLabel;
        private System.Windows.Forms.ImageList largeImageList;
        private System.Windows.Forms.ToolStripStatusLabel contentsStatusLabel;
        private System.Windows.Forms.ImageList smallImageList;
        private System.Windows.Forms.ToolStripButton bnkDeleteToolStripButton;
        private System.Windows.Forms.ToolStripButton bnkRestoreToolStripButton;
        private System.Windows.Forms.ToolStrip bnkToolStrip;
        private System.Windows.Forms.ToolStripButton fileOpenToolStripButton;
        private System.Windows.Forms.ToolStripButton fileEditToolStripButton;
        private System.Windows.Forms.ToolStripButton fileExtractToolStripButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.ToolStripDropDownButton toolsToolStripDropDownButton;
        private System.Windows.Forms.ToolStripLabel titleToolStripLabel;
        private System.Windows.Forms.ToolStripDropDownButton viewToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem bnkManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editTasksToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem largeIconsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem detailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableFSCToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader nameColumnHeader;
        private System.Windows.Forms.ColumnHeader sizeColumnHeader;
        private System.Windows.Forms.ColumnHeader typeColumnHeader;
        private System.Windows.Forms.ToolStripButton loadBnkToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripButton fileRenameToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton fileViewToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem fileViewFlatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileViewHierarchicalToolStripMenuItem;
        private System.Windows.Forms.TreeView contentTreeView;
        private System.Windows.Forms.ToolStripTextBox fileSearchToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel fileSearchToolStripLabel;
        private System.Windows.Forms.ToolStripStatusLabel bnkStatusLabelCountSize;
        private System.Windows.Forms.ToolStripDropDownButton fileReplaceParentToolStripDropDownButton;
        private System.Windows.Forms.ToolStripMenuItem keepNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton fileDeleteToolStripButton;
    }
}