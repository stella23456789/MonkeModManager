namespace MonkeModManager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.buttonFolderBrowser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonInstall = new System.Windows.Forms.Button();
            this.labelStatus = new System.Windows.Forms.Label();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.Plugins = new System.Windows.Forms.TabPage();
            this.listViewMods = new System.Windows.Forms.ListView();
            this.columnHeaderName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderAuthor = new System.Windows.Forms.ColumnHeader();
            this.contextMenuStripMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Utilities = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonOpenWiki = new System.Windows.Forms.Button();
            this.buttonDiscordLink = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonBepInEx = new System.Windows.Forms.Button();
            this.buttonOpenConfig = new System.Windows.Forms.Button();
            this.buttonOpenGameFolder = new System.Windows.Forms.Button();
            this.labelOpen = new System.Windows.Forms.Label();
            this.buttonUninstallAll = new System.Windows.Forms.Button();
            this.Instances = new System.Windows.Forms.TabPage();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.RefrshIns = new System.Windows.Forms.Button();
            this.instanceButton = new System.Windows.Forms.Button();
            this.Settins = new System.Windows.Forms.TabPage();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button3 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.Experimental = new System.Windows.Forms.TabPage();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.BananaClicker = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonModInfo = new System.Windows.Forms.Button();
            this.buttonToggleMods = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.jToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlMain.SuspendLayout();
            this.Plugins.SuspendLayout();
            this.contextMenuStripMain.SuspendLayout();
            this.Utilities.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.Instances.SuspendLayout();
            this.Settins.SuspendLayout();
            this.Experimental.SuspendLayout();
            this.BananaClicker.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDirectory.Enabled = false;
            this.textBoxDirectory.Location = new System.Drawing.Point(10, 25);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(508, 22);
            this.textBoxDirectory.TabIndex = 0;
            // 
            // buttonFolderBrowser
            // 
            this.buttonFolderBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFolderBrowser.Location = new System.Drawing.Point(524, 25);
            this.buttonFolderBrowser.Name = "buttonFolderBrowser";
            this.buttonFolderBrowser.Size = new System.Drawing.Size(26, 23);
            this.buttonFolderBrowser.TabIndex = 1;
            this.buttonFolderBrowser.Text = "..";
            this.buttonFolderBrowser.UseVisualStyleBackColor = true;
            this.buttonFolderBrowser.Click += new System.EventHandler(this.buttonFolderBrowser_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Gorilla Tag Folder Path:";
            // 
            // buttonInstall
            // 
            this.buttonInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInstall.Enabled = false;
            this.buttonInstall.Location = new System.Drawing.Point(440, 341);
            this.buttonInstall.Name = "buttonInstall";
            this.buttonInstall.Size = new System.Drawing.Size(112, 23);
            this.buttonInstall.TabIndex = 4;
            this.buttonInstall.Text = "Install / Update";
            this.buttonInstall.UseVisualStyleBackColor = true;
            this.buttonInstall.Click += new System.EventHandler(this.buttonInstall_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(7, 346);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(66, 13);
            this.labelStatus.TabIndex = 5;
            this.labelStatus.Text = "Status: Null";
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.Plugins);
            this.tabControlMain.Controls.Add(this.Utilities);
            this.tabControlMain.Controls.Add(this.Instances);
            this.tabControlMain.Controls.Add(this.Settins);
            this.tabControlMain.Controls.Add(this.Experimental);
            this.tabControlMain.Controls.Add(this.BananaClicker);
            this.tabControlMain.Location = new System.Drawing.Point(10, 53);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(544, 282);
            this.tabControlMain.TabIndex = 8;
            // 
            // Plugins
            // 
            this.Plugins.Controls.Add(this.listViewMods);
            this.Plugins.Location = new System.Drawing.Point(4, 22);
            this.Plugins.Name = "Plugins";
            this.Plugins.Padding = new System.Windows.Forms.Padding(3);
            this.Plugins.Size = new System.Drawing.Size(536, 256);
            this.Plugins.TabIndex = 0;
            this.Plugins.Text = "Plugins";
            this.Plugins.UseVisualStyleBackColor = true;
            // 
            // listViewMods
            // 
            this.listViewMods.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewMods.CheckBoxes = true;
            this.listViewMods.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
            {
                this.columnHeaderName, this.columnHeaderAuthor
            });
            this.listViewMods.ContextMenuStrip = this.contextMenuStripMain;
            this.listViewMods.FullRowSelect = true;
            this.listViewMods.HideSelection = false;
            this.listViewMods.Location = new System.Drawing.Point(6, 6);
            this.listViewMods.Name = "listViewMods";
            this.listViewMods.Size = new System.Drawing.Size(524, 244);
            this.listViewMods.TabIndex = 0;
            this.listViewMods.UseCompatibleStateImageBehavior = false;
            this.listViewMods.View = System.Windows.Forms.View.Details;
            this.listViewMods.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewMods_ItemChecked);
            this.listViewMods.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listViewMods_ItemSelectionChanged);
            this.listViewMods.DoubleClick += new System.EventHandler(this.listViewMods_DoubleClick);
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 321;
            // 
            // columnHeaderAuthor
            // 
            this.columnHeaderAuthor.Text = "Author";
            this.columnHeaderAuthor.Width = 162;
            // 
            // contextMenuStripMain
            // 
            this.contextMenuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.viewInfoToolStripMenuItem
            });
            this.contextMenuStripMain.Name = "contextMenuStripMain";
            this.contextMenuStripMain.Size = new System.Drawing.Size(124, 26);
            // 
            // viewInfoToolStripMenuItem
            // 
            this.viewInfoToolStripMenuItem.Name = "viewInfoToolStripMenuItem";
            this.viewInfoToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.viewInfoToolStripMenuItem.Text = "View Info";
            this.viewInfoToolStripMenuItem.Click += new System.EventHandler(this.viewInfoToolStripMenuItem_Click);
            // 
            // Utilities
            // 
            this.Utilities.Controls.Add(this.button1);
            this.Utilities.Controls.Add(this.labelVersion);
            this.Utilities.Controls.Add(this.pictureBox1);
            this.Utilities.Controls.Add(this.buttonOpenWiki);
            this.Utilities.Controls.Add(this.buttonDiscordLink);
            this.Utilities.Controls.Add(this.groupBox1);
            this.Utilities.Controls.Add(this.buttonUninstallAll);
            this.Utilities.Location = new System.Drawing.Point(4, 22);
            this.Utilities.Name = "Utilities";
            this.Utilities.Size = new System.Drawing.Size(536, 256);
            this.Utilities.TabIndex = 1;
            this.Utilities.Text = "Utilities";
            this.Utilities.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Config Editor\r\n";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(188, 209);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(119, 13);
            this.labelVersion.TabIndex = 11;
            this.labelVersion.Text = "Monke Mod Manager";
            this.labelVersion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.labelVersion.UseMnemonic = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(170, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(186, 163);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // buttonOpenWiki
            // 
            this.buttonOpenWiki.Location = new System.Drawing.Point(379, 183);
            this.buttonOpenWiki.Name = "buttonOpenWiki";
            this.buttonOpenWiki.Size = new System.Drawing.Size(134, 23);
            this.buttonOpenWiki.TabIndex = 9;
            this.buttonOpenWiki.Text = "Check out the guides!";
            this.buttonOpenWiki.UseVisualStyleBackColor = true;
            this.buttonOpenWiki.Click += new System.EventHandler(this.buttonOpenWiki_Click);
            // 
            // buttonDiscordLink
            // 
            this.buttonDiscordLink.Location = new System.Drawing.Point(379, 153);
            this.buttonDiscordLink.Name = "buttonDiscordLink";
            this.buttonDiscordLink.Size = new System.Drawing.Size(134, 23);
            this.buttonDiscordLink.TabIndex = 8;
            this.buttonDiscordLink.Text = "Join the Discord!";
            this.buttonDiscordLink.UseVisualStyleBackColor = true;
            this.buttonDiscordLink.Click += new System.EventHandler(this.buttonDiscordLink_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonBepInEx);
            this.groupBox1.Controls.Add(this.buttonOpenConfig);
            this.groupBox1.Controls.Add(this.buttonOpenGameFolder);
            this.groupBox1.Controls.Add(this.labelOpen);
            this.groupBox1.Location = new System.Drawing.Point(373, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(146, 130);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // buttonBepInEx
            // 
            this.buttonBepInEx.Location = new System.Drawing.Point(6, 96);
            this.buttonBepInEx.Name = "buttonBepInEx";
            this.buttonBepInEx.Size = new System.Drawing.Size(134, 23);
            this.buttonBepInEx.TabIndex = 5;
            this.buttonBepInEx.Text = "Mods/Plugins Folder";
            this.buttonBepInEx.UseVisualStyleBackColor = true;
            this.buttonBepInEx.Click += new System.EventHandler(this.buttonOpenBepInExFolder_Click);
            // 
            // buttonOpenConfig
            // 
            this.buttonOpenConfig.Location = new System.Drawing.Point(6, 67);
            this.buttonOpenConfig.Name = "buttonOpenConfig";
            this.buttonOpenConfig.Size = new System.Drawing.Size(134, 23);
            this.buttonOpenConfig.TabIndex = 5;
            this.buttonOpenConfig.Text = "Config Folder";
            this.buttonOpenConfig.UseVisualStyleBackColor = true;
            this.buttonOpenConfig.Click += new System.EventHandler(this.buttonOpenConfigFolder_Click);
            // 
            // buttonOpenGameFolder
            // 
            this.buttonOpenGameFolder.Location = new System.Drawing.Point(6, 38);
            this.buttonOpenGameFolder.Name = "buttonOpenGameFolder";
            this.buttonOpenGameFolder.Size = new System.Drawing.Size(134, 23);
            this.buttonOpenGameFolder.TabIndex = 5;
            this.buttonOpenGameFolder.Text = "Game Folder";
            this.buttonOpenGameFolder.UseVisualStyleBackColor = true;
            this.buttonOpenGameFolder.Click += new System.EventHandler(this.buttonOpenGameFolder_Click);
            // 
            // labelOpen
            // 
            this.labelOpen.AutoSize = true;
            this.labelOpen.Location = new System.Drawing.Point(23, 15);
            this.labelOpen.Name = "labelOpen";
            this.labelOpen.Size = new System.Drawing.Size(99, 13);
            this.labelOpen.TabIndex = 6;
            this.labelOpen.Text = "Important Folders";
            // 
            // buttonUninstallAll
            // 
            this.buttonUninstallAll.Location = new System.Drawing.Point(14, 43);
            this.buttonUninstallAll.Name = "buttonUninstallAll";
            this.buttonUninstallAll.Size = new System.Drawing.Size(132, 23);
            this.buttonUninstallAll.TabIndex = 0;
            this.buttonUninstallAll.Text = "Uninstall All Mods";
            this.buttonUninstallAll.UseVisualStyleBackColor = true;
            this.buttonUninstallAll.Click += new System.EventHandler(this.buttonUninstallAll_Click);
            // 
            // Instances
            // 
            this.Instances.Controls.Add(this.checkedListBox1);
            this.Instances.Controls.Add(this.button2);
            this.Instances.Controls.Add(this.RefrshIns);
            this.Instances.Controls.Add(this.instanceButton);
            this.Instances.Location = new System.Drawing.Point(4, 22);
            this.Instances.Name = "Instances";
            this.Instances.Padding = new System.Windows.Forms.Padding(3);
            this.Instances.Size = new System.Drawing.Size(536, 256);
            this.Instances.TabIndex = 2;
            this.Instances.Text = "Instances";
            this.Instances.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(3, 4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(527, 225);
            this.checkedListBox1.TabIndex = 4;
            this.checkedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(155, 232);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(226, 21);
            this.button2.TabIndex = 3;
            this.button2.Text = "Remove Selected Instance";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // RefrshIns
            // 
            this.RefrshIns.Location = new System.Drawing.Point(387, 232);
            this.RefrshIns.Name = "RefrshIns";
            this.RefrshIns.Size = new System.Drawing.Size(143, 21);
            this.RefrshIns.TabIndex = 2;
            this.RefrshIns.Text = "Refresh Instances";
            this.RefrshIns.UseVisualStyleBackColor = true;
            this.RefrshIns.Click += new System.EventHandler(this.RefrshIns_Click);
            // 
            // instanceButton
            // 
            this.instanceButton.Location = new System.Drawing.Point(6, 232);
            this.instanceButton.Name = "instanceButton";
            this.instanceButton.Size = new System.Drawing.Size(143, 21);
            this.instanceButton.TabIndex = 0;
            this.instanceButton.Text = "Create New Instance";
            this.instanceButton.UseVisualStyleBackColor = true;
            this.instanceButton.Click += new System.EventHandler(this.Instance_Click);
            // 
            // Settins
            // 
            this.Settins.Controls.Add(this.checkBox3);
            this.Settins.Controls.Add(this.button3);
            this.Settins.Controls.Add(this.checkBox1);
            this.Settins.Location = new System.Drawing.Point(4, 22);
            this.Settins.Name = "Settins";
            this.Settins.Padding = new System.Windows.Forms.Padding(3);
            this.Settins.Size = new System.Drawing.Size(536, 256);
            this.Settins.TabIndex = 3;
            this.Settins.Text = "Settings";
            this.Settins.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.ForeColor = System.Drawing.Color.Black;
            this.checkBox3.Location = new System.Drawing.Point(6, 31);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(160, 19);
            this.checkBox3.TabIndex = 3;
            this.checkBox3.Text = "Auto-update";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 227);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(115, 23);
            this.button3.TabIndex = 1;
            this.button3.Text = "Check for updates";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.ForeColor = System.Drawing.Color.Black;
            this.checkBox1.Location = new System.Drawing.Point(6, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(160, 19);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Use Intances";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Experimental
            // 
            this.Experimental.Controls.Add(this.checkBox4);
            this.Experimental.Location = new System.Drawing.Point(4, 22);
            this.Experimental.Name = "Experimental";
            this.Experimental.Padding = new System.Windows.Forms.Padding(3);
            this.Experimental.Size = new System.Drawing.Size(536, 256);
            this.Experimental.TabIndex = 4;
            this.Experimental.Text = "Experimental";
            this.Experimental.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.Location = new System.Drawing.Point(6, 6);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(115, 22);
            this.checkBox4.TabIndex = 0;
            this.checkBox4.Text = "Dark Mode";
            this.checkBox4.UseVisualStyleBackColor = true;
            this.checkBox4.CheckedChanged += new System.EventHandler(this.checkBox4_CheckedChanged);
            // 
            // BananaClicker
            // 
            this.BananaClicker.Controls.Add(this.label3);
            this.BananaClicker.Controls.Add(this.label2);
            this.BananaClicker.Controls.Add(this.button4);
            this.BananaClicker.Controls.Add(this.panel1);
            this.BananaClicker.Location = new System.Drawing.Point(4, 22);
            this.BananaClicker.Name = "BananaClicker";
            this.BananaClicker.Padding = new System.Windows.Forms.Padding(3);
            this.BananaClicker.Size = new System.Drawing.Size(536, 256);
            this.BananaClicker.TabIndex = 5;
            this.BananaClicker.Text = "BananaClicker";
            this.BananaClicker.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(524, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "Click Power: 1";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(524, 27);
            this.label2.TabIndex = 2;
            this.label2.Text = "Money: $0";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(6, 227);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(218, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Buy Monke (+0.1 Auto-clicks) - $5";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(315, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 100);
            this.panel1.TabIndex = 0;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // buttonModInfo
            // 
            this.buttonModInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonModInfo.Enabled = false;
            this.buttonModInfo.Location = new System.Drawing.Point(322, 341);
            this.buttonModInfo.Name = "buttonModInfo";
            this.buttonModInfo.Size = new System.Drawing.Size(112, 23);
            this.buttonModInfo.TabIndex = 9;
            this.buttonModInfo.Text = "View Mod Info";
            this.buttonModInfo.UseVisualStyleBackColor = true;
            this.buttonModInfo.Click += new System.EventHandler(this.buttonModInfo_Click);
            // 
            // buttonToggleMods
            // 
            this.buttonToggleMods.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonToggleMods.Enabled = false;
            this.buttonToggleMods.Location = new System.Drawing.Point(204, 341);
            this.buttonToggleMods.Name = "buttonToggleMods";
            this.buttonToggleMods.Size = new System.Drawing.Size(112, 23);
            this.buttonToggleMods.TabIndex = 10;
            this.buttonToggleMods.Text = "Disable Mods";
            this.buttonToggleMods.UseVisualStyleBackColor = true;
            this.buttonToggleMods.Click += new System.EventHandler(this.buttonToggleMods_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
            {
                this.jToolStripMenuItem
            });
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(140, 26);
            // 
            // jToolStripMenuItem
            // 
            this.jToolStripMenuItem.Name = "jToolStripMenuItem";
            this.jToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.jToolStripMenuItem.Text = "List Instance";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 376);
            this.Controls.Add(this.buttonToggleMods);
            this.Controls.Add(this.buttonModInfo);
            this.Controls.Add(this.tabControlMain);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.buttonInstall);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonFolderBrowser);
            this.Controls.Add(this.textBoxDirectory);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Monke Mod Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControlMain.ResumeLayout(false);
            this.Plugins.ResumeLayout(false);
            this.contextMenuStripMain.ResumeLayout(false);
            this.Utilities.ResumeLayout(false);
            this.Utilities.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Instances.ResumeLayout(false);
            this.Settins.ResumeLayout(false);
            this.Experimental.ResumeLayout(false);
            this.BananaClicker.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage BananaClicker;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TabPage Experimental;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem jToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button RefrshIns;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage Instances;
        private System.Windows.Forms.TabPage Settins;
        private System.Windows.Forms.Button instanceButton;
        private System.Windows.Forms.Button button1;

        #endregion

        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.Button buttonFolderBrowser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonInstall;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage Plugins;
        private System.Windows.Forms.ListView listViewMods;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderAuthor;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMain;
        private System.Windows.Forms.ToolStripMenuItem viewInfoToolStripMenuItem;
        private System.Windows.Forms.Button buttonModInfo;
        private System.Windows.Forms.TabPage Utilities;
        private System.Windows.Forms.Button buttonUninstallAll;
        private System.Windows.Forms.Label labelOpen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonBepInEx;
        private System.Windows.Forms.Button buttonOpenConfig;
        private System.Windows.Forms.Button buttonOpenGameFolder;
        private System.Windows.Forms.Button buttonOpenWiki;
        private System.Windows.Forms.Button buttonDiscordLink;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button buttonToggleMods;
    }
}
