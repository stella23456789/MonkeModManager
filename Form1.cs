using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using MonkeModManager.Internals;
using MonkeModManager.Internals.SimpleJSON;
using Microsoft.VisualBasic;

namespace MonkeModManager
{
    public partial class Form1 : Form
    {
        
        private string DefaultOculusInstallDirectory = @"C:\Program Files\Oculus\Software\Software\another-axiom-gorilla-tag";
        private string DefaultSteamInstallDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag";
        public static string InstallDirectory = @"";
        Dictionary<string, int> groups = new Dictionary<string, int>();
        private List<ReleaseInfo> releases;
        private bool modsDisabled;
        private string configFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MMM.config.conf");
        private string DefaultDoorstopPath = @"target_assembly=BepInEx\core\BepInEx.Preloader.dll";
        private int CurrentVersion = 3; // actual version is 2.0.1 aka a hotfix
        public bool InstanceEnabled;
        public bool ExperimentalEnabled;
        public bool AutoUpdateEnabled;
        
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonFolderBrowser_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.FileName = "Gorilla Tag.exe";
                fileDialog.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
                fileDialog.FilterIndex = 1;
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = fileDialog.FileName;
                    if (Path.GetFileName(path).Equals("Gorilla Tag.exe"))
                    {
                        InstallDirectory = Path.GetDirectoryName(path);
                        textBoxDirectory.Text = InstallDirectory;
                        EditmmmConfig(InstallDirectory);
                    }
                    else
                    {
                        MessageBox.Show("That's not Gorilla Tag.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void buttonInstall_Click(object sender, EventArgs e)
        {
            new Thread(Install).Start();
        }

        private void Install()
        {
            if (InstanceEnabled)
            {
                UpdateStatus("Changing Unity Doorstop config");
                DoorstopInstanceChange(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{checkedListBox1.CheckedItems[0]}\BepInEx\core\BepInEx.Preloader.dll"));
                ChangeInstallButtonState(false);
                UpdateStatus("Starting install sequence...");
                foreach (ReleaseInfo release in releases)
                {
                    if (release.Install)
                    {
                        UpdateStatus(string.Format("Downloading...{0}", release.Name));
                        byte[] file = DownloadFile(release.Link);
                        UpdateStatus(string.Format("Installing...{0}", release.Name));
                        string fileName = Path.GetFileName(release.Link);
                        if (Path.GetExtension(fileName).Equals(".dll"))
                        {
                            string dir;
                            if (release.InstallLocation == null)
                            {
                                dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{checkedListBox1.CheckedItems[0]}\BepInEx\plugins", Regex.Replace(release.Name, @"\s+", string.Empty));
                                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                            }
                            else
                            {
                                dir = Path.Combine($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\mmmInstances\{checkedListBox1.CheckedItems[0]}\", release.InstallLocation);
                            }
                            File.WriteAllBytes(Path.Combine(dir, fileName), file);

                            var dllFile = Path.Combine($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\mmmInstances\{checkedListBox1.CheckedItems[0]}\", @"BepInEx\plugins", fileName);
                            if (File.Exists(dllFile))
                            {
                                File.Delete(dllFile);
                            }
                        }
                        else
                        {
                            UnzipFile(file, (release.InstallLocation != null) ? Path.Combine($@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\mmmInstances\{checkedListBox1.CheckedItems[0]}\", release.InstallLocation) : $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\mmmInstances\{checkedListBox1.CheckedItems[0]}\");
                        }
                        UpdateStatus(string.Format("Installed {0}!", release.Name));
                    }
                }
                UpdateStatus("Install complete!");
                ChangeInstallButtonState(true);

                this.Invoke((MethodInvoker)(() =>
                {
                    //Invoke so we can call from any thread
                    buttonToggleMods.Enabled = true;
                }));
            }
            else
            {
                DoorstopInstanceChange(DefaultDoorstopPath);
                ChangeInstallButtonState(false);
                UpdateStatus("Starting install sequence...");
                foreach (ReleaseInfo release in releases)
                {
                    if (release.Install)
                    {
                        UpdateStatus(string.Format("Downloading...{0}", release.Name));
                        byte[] file = DownloadFile(release.Link);
                        UpdateStatus(string.Format("Installing...{0}", release.Name));
                        string fileName = Path.GetFileName(release.Link);
                        if (Path.GetExtension(fileName).Equals(".dll"))
                        {
                            string dir;
                            if (release.InstallLocation == null)
                            {
                                dir = Path.Combine(InstallDirectory, @"BepInEx\plugins", Regex.Replace(release.Name, @"\s+", string.Empty));
                                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                            }
                            else
                            {
                                dir = Path.Combine(InstallDirectory, release.InstallLocation);
                            }
                            File.WriteAllBytes(Path.Combine(dir, fileName), file);

                            var dllFile = Path.Combine(InstallDirectory, @"BepInEx\plugins", fileName);
                            if (File.Exists(dllFile))
                            {
                                File.Delete(dllFile);
                            }
                        }
                        else
                        {
                            UnzipFile(file, (release.InstallLocation != null) ? Path.Combine(InstallDirectory, release.InstallLocation) : InstallDirectory);
                        }
                        UpdateStatus(string.Format("Installed {0}!", release.Name));
                    }
                }
                UpdateStatus("Install complete!");
                ChangeInstallButtonState(true);

                this.Invoke((MethodInvoker)(() =>
                {
                    //Invoke so we can call from any thread
                    buttonToggleMods.Enabled = true;
                }));
            }
        }
        
        private void UnzipFile(byte[] data, string directory)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                using (var unzip = new Unzip(ms))
                {
                    unzip.ExtractToDirectory(directory);
                }
            }
        }
        
        private void ChangeInstallButtonState(bool enabled)
        {
            this.Invoke((MethodInvoker)(() =>
            {
                buttonInstall.Enabled = enabled;
            }));
        }
        
        private byte[] DownloadFile(string url)
        {
            WebClient client = new WebClient();
            client.Proxy = null;
            return client.DownloadData(url);
        }
        
        private void listViewMods_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ReleaseInfo release = (ReleaseInfo)e.Item.Tag;

            if (release.Dependencies.Count > 0)
            {
                foreach (ListViewItem item in listViewMods.Items)
                {
                    ReleaseInfo plugin = (ReleaseInfo)item.Tag;

                    if (plugin.Name == release.Name) continue;

                    // if this depends on plugin
                    if (release.Dependencies.Contains(plugin.Name))
                    {
                        if (e.Item.Checked)
                        {
                            item.Checked = true;
                            item.ForeColor = System.Drawing.Color.DimGray;
                        }
                        else
                        {
                            release.Install = false;
                            if (releases.Count(x => plugin.Dependents.Contains(x.Name) && x.Install) <= 1)
                            {
                                item.Checked = false;
                                item.ForeColor = System.Drawing.Color.Black;
                            }
                        }
                    }
                }
            }

            // don't allow user to uncheck if a dependent is checked
            if (release.Dependents.Count > 0)
            {
                if (releases.Count(x => release.Dependents.Contains(x.Name) && x.Install) > 0)
                {
                    e.Item.Checked = true;
                }
            }

            if (release.Name.Contains("BepInEx")) { e.Item.Checked = true; };
            release.Install = e.Item.Checked;
        }
        private void listViewMods_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            buttonModInfo.Enabled = listViewMods.SelectedItems.Count > 0;
        }
        void listViewMods_DoubleClick(object sender, EventArgs e)
        {
            OpenLinkFromRelease();
        }
        void viewInfoToolStripMenuItem_Click(object sender, EventArgs e) {OpenLinkFromRelease();}
        void buttonOpenWiki_Click(object sender, EventArgs e)
        {
            Process.Start("https://loafiat.github.io/GorillaTag-Modding-Guide/#/pc-guide");
        }
        void buttonDiscordLink_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/monkemod");
        }
        private void buttonOpenGameFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(InstallDirectory))
                Process.Start(InstallDirectory);
        }

        private void buttonOpenConfigFolder_Click(object sender, EventArgs e)
        {
            var configDirectory = Path.Combine(InstallDirectory, @"BepInEx\config");
            if (Directory.Exists(configDirectory))
                Process.Start(configDirectory);
        }

        private void buttonOpenBepInExFolder_Click(object sender, EventArgs e)
        {
            var pluginsDirectory = Path.Combine(InstallDirectory, "BepInEx/plugins");
            if (Directory.Exists(pluginsDirectory))
                Process.Start(pluginsDirectory);
        }
        void buttonUninstallAll_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show(
                "You are about to delete all your mods. This cannot be undone!\n\nAre you sure you wish to continue?",
                "Confirm Delete",
                MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                UpdateStatus("Uninstalling all mods");
                
                var pluginsPath = Path.Combine(InstallDirectory, @"BepInEx\plugins");
                
                try
                {
                    foreach (var d in Directory.GetDirectories(pluginsPath))
                    {
                        Directory.Delete(d, true);
                    }

                    foreach (var f in Directory.GetFiles(pluginsPath))
                    {
                        File.Delete(f);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatus("Failed to uninstall mods.");
                    return;
                }

                UpdateStatus("All mods uninstalled successfully!");
            }
        }
        void buttonModInfo_Click(object sender, EventArgs e)
        {
            OpenLinkFromRelease();
        }
        
        private void OpenLinkFromRelease()
        {
            if (listViewMods.SelectedItems.Count > 0)
            {
                ReleaseInfo release = (ReleaseInfo)listViewMods.SelectedItems[0].Tag;
                UpdateStatus($"Opening GitHub page for {release.Name}");
                Process.Start(string.Format("https://github.com/{0}", release.GitPath));
            }
            
        }
        private void buttonToggleMods_Click(object sender, EventArgs e)
        {
            if (modsDisabled)
            {
                if (File.Exists(Path.Combine(InstallDirectory, "mods.disable")))
                {
                    File.Move(Path.Combine(InstallDirectory, "mods.disable"), Path.Combine(InstallDirectory, "winhttp.dll"));
                    buttonToggleMods.Text = "Disable Mods";
                    buttonToggleMods.BackColor = System.Drawing.Color.Transparent;
                    modsDisabled = false;
                    UpdateStatus("Enabled mods!");
                }
            }
            else
            {
                if (File.Exists(Path.Combine(InstallDirectory, "winhttp.dll")))
                {
                    File.Move(Path.Combine(InstallDirectory, "winhttp.dll"), Path.Combine(InstallDirectory, "mods.disable"));
                    buttonToggleMods.Text = "Enable Mods";
                    buttonToggleMods.BackColor = System.Drawing.Color.IndianRed;
                    modsDisabled = true;
                    UpdateStatus("Disabled mods!");
                }
            }
        }
        void Form1_Load(object sender, EventArgs e)
        {
            releases = new List<ReleaseInfo>();

            labelVersion.Text = "Monke Mod Manager v1.1";
            
            MmmConfig();
            
            if (InstallDirectory != @"" && Directory.Exists(InstallDirectory))
            {
                textBoxDirectory.Text = InstallDirectory;
            }
            else if(File.Exists(Path.Combine(DefaultSteamInstallDirectory, "Gorilla Tag.exe")))
            {
                InstallDirectory = DefaultSteamInstallDirectory;
                textBoxDirectory.Text = InstallDirectory;
                EditmmmConfig(InstallDirectory);
            }
            else if (File.Exists(Path.Combine(DefaultOculusInstallDirectory, "Gorilla Tag.exe")))
            {
                InstallDirectory = DefaultOculusInstallDirectory;
                textBoxDirectory.Text = InstallDirectory;
                EditmmmConfig(InstallDirectory);
            }
            else
            {
                ShowErrorFindingDirectoryMessage();
            }
            
            ConfigFix();
            
            AddInstancesToList();
            
            if (!File.Exists(Path.Combine(InstallDirectory, "winhttp.dll")))
            {
                if (File.Exists(Path.Combine(InstallDirectory, "mods.disable")))
                {
                    buttonToggleMods.Text = "Enable Mods";
                    modsDisabled = true;
                    buttonToggleMods.BackColor = System.Drawing.Color.IndianRed;
                    buttonToggleMods.Enabled = true;
                }
                else
                {
                    buttonToggleMods.Enabled = false;
                }
            }
            else
            {
                buttonToggleMods.Enabled = true;
            }
            new Thread(() =>
            {
                LoadRequiredPlugins();
            }).Start();
        }

        private void UpdateStatus(string status)
        {
            string formattedText = string.Format("Status: {0}", status);
            this.Invoke((MethodInvoker)(() =>
            { //Invoke so we can call from any thread
                labelStatus.Text = formattedText;
            }));
        }
        
        private CookieContainer PermCookie;
        private string DownloadSite(string URL)
        {
            try
            {
                if (PermCookie == null) { PermCookie = new CookieContainer(); }
                HttpWebRequest RQuest = (HttpWebRequest)HttpWebRequest.Create(URL);
                RQuest.Method = "GET";
                RQuest.KeepAlive = true;
                RQuest.CookieContainer = PermCookie;
                RQuest.ContentType = "application/x-www-form-urlencoded";
                RQuest.Referer = "";
                RQuest.UserAgent = "Monke-Mod-Manager";
                RQuest.Proxy = null;
                HttpWebResponse Response = (HttpWebResponse)RQuest.GetResponse();
                StreamReader Sr = new StreamReader(Response.GetResponseStream());
                string Code = Sr.ReadToEnd();
                Sr.Close();
                return Code;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("403"))
                {
                    MessageBox.Show("Failed to fetch info, GitHub has most likely rate limited you, please check back in 15 - 30 minutes", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Failed to fetch info, please check your internet connection", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show("You will still be able to use MMM but you won't be able to install mods.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateStatus("Failed to fetch info");
                return null;
            }
        }
        
        private void LoadReleases()
        {
            var decodedMods = JSON.Parse(DownloadSite("https://raw.githubusercontent.com/The-Graze/MonkeModInfo/master/modinfo.json"));
            var decodedGroups = JSON.Parse(DownloadSite("https://raw.githubusercontent.com/The-Graze/MonkeModInfo/master/groupinfo.json"));
            
            var allMods = decodedMods.AsArray;
            var allGroups = decodedGroups.AsArray;

            for (int i = 0; i < allMods.Count; i++)
            {
                JSONNode current = allMods[i];
                ReleaseInfo release = new ReleaseInfo(current["name"], current["author"], current["version"], current["group"], current["download_url"], current["install_location"], current["git_path"], current["dependencies"].AsArray);
                //UpdateReleaseInfo(ref release);
                releases.Add(release);
            }


            IOrderedEnumerable<KeyValuePair<string, JSONNode>> keyValuePairs = allGroups.Linq.OrderBy(x => x.Value["rank"]);
            for (int i = 0; i < allGroups.Count; i++)
            {
                JSONNode current = allGroups[i];
                if (releases.Any(x => x.Group == current["name"]))
                {
                    groups.Add(current["name"], groups.Count());
                }
            }
            groups.Add("Uncategorized", groups.Count());

            foreach (ReleaseInfo release in releases)
            {
                foreach (string dep in release.Dependencies)
                {
                    releases.Where(x => x.Name == dep).FirstOrDefault()?.Dependents.Add(release.Name);
                }
            }
        }
        
        
        public static void ConfigFix()
        {
            if (!File.Exists(Path.Combine(InstallDirectory, @"BepInEx\config\BepInEx.cfg")))
            {
                return;
            }

            string c = File.ReadAllText(Path.Combine(InstallDirectory, @"BepInEx\config\BepInEx.cfg"));
            if (!c.Contains("HideManagerGameObject = false"))
            {
                return;
            }
               
            string e = c.Replace("HideManagerGameObject = false", "HideManagerGameObject = true");
            File.WriteAllText(Path.Combine(InstallDirectory, @"BepInEx\config\BepInEx.cfg"), e);
        }        
        
        public static void DoorstopInstanceChange(string InstancePath)
        {
            if (!File.Exists(Path.Combine(InstallDirectory, @"doorstop_config.ini")))
            {
                return;
            }

            string c = File.ReadAllText(Path.Combine(InstallDirectory, @"doorstop_config.ini"));
               
            string e = c.Replace(@"target_assembly=BepInEx\core\BepInEx.Preloader.dll", $@"target_assembly={InstancePath}");
            File.WriteAllText(Path.Combine(InstallDirectory, @"doorstop_config.ini"), e);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            ConfigEditor configEditor = new ConfigEditor();
            configEditor.Show();
        }

        private void MmmConfig()
        {
            string MMMPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            string confPath = Path.Combine(MMMPath, "MMM.config.conf");
            
            if (!File.Exists(confPath))
            {
                File.WriteAllText(confPath, @"InstallDirectory=" + InstallDirectory);
            }
            
            string[] content = File.ReadAllLines(confPath);
            
            foreach (string line in content)
            {
                if (!line.StartsWith("InstallDirectory="))
                    continue;
                string value = line.Substring("InstallDirectory=".Length);
                if (!string.IsNullOrWhiteSpace(value))
                {
                    InstallDirectory = @value;
                }
                break;
            }
        }

        private void AddInstancesToList()
        {
            
            string instancesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "mmmInstances");
            if (!Directory.Exists(instancesPath))  
                Directory.CreateDirectory(instancesPath);
            
            checkedListBox1.Items.Clear();
            
            foreach (var directory in Directory.GetDirectories(instancesPath))
            {
                string folderName = Path.GetFileName(directory);
                checkedListBox1.Items.Add(folderName);
            }
        }
        
        private void LoadRequiredPlugins()
        {
            UpdateStatus("Getting latest version info...");
            LoadReleases();
            this.Invoke((MethodInvoker)(() =>
            {//Invoke so we can call from current thread
             //Update checkbox's text
                Dictionary<string, int> includedGroups = new Dictionary<string, int>();

                for (int i = 0; i < groups.Count(); i++)
                {
                    var key = groups.First(x => x.Value == i).Key;
                    var value = listViewMods.Groups.Add(new ListViewGroup(key, HorizontalAlignment.Left));
                    groups[key] = value;
                }

                foreach (ReleaseInfo release in releases)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = release.Name;
                    if (!String.IsNullOrEmpty(release.Version)) item.Text = $"{release.Name} - {release.Version}";
                    if (!String.IsNullOrEmpty(release.Tag)) { item.Text = string.Format("{0} - ({1})",release.Name, release.Tag); };
                    item.SubItems.Add(release.Author);
                    item.Tag = release;
                    if (release.Install)
                    {
                        listViewMods.Items.Add(item);
                    }
                    CheckDefaultMod(release, item);

                    if (release.Group == null || !groups.ContainsKey(release.Group))
                    {
                        item.Group = listViewMods.Groups[groups["Uncategorized"]];
                    }
                    else if (groups.ContainsKey(release.Group))
                    {
                        int index = groups[release.Group];
                        item.Group = listViewMods.Groups[index];
                    }
                    else
                    {
                        //int index = listViewMods.Groups.Add(new ListViewGroup(release.Group, HorizontalAlignment.Left));
                        //item.Group = listViewMods.Groups[index];
                    }
                }

                tabControlMain.Enabled = true;
                buttonInstall.Enabled = true;

            }));
           
            UpdateStatus("Release info updated!");

        }
                
        private void CheckDefaultMod(ReleaseInfo release, ListViewItem item)
        {
            if (release.Name.Contains("BepInEx"))
            {
                item.Checked = true;
                item.ForeColor = System.Drawing.Color.DimGray;
            }
            else
            {
                release.Install = false;
            }
        }
        private void NotFoundHandler()
        {
            bool found = false;
            while (found == false)
            {
                using (var fileDialog = new OpenFileDialog())
                {
                    fileDialog.FileName = "Gorilla Tag.exe";
                    fileDialog.Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*";
                    fileDialog.FilterIndex = 1;
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string path = fileDialog.FileName;
                        if (Path.GetFileName(path).Equals("Gorilla Tag.exe"))
                        {
                            InstallDirectory = Path.GetDirectoryName(path);
                            textBoxDirectory.Text = InstallDirectory;
                            found = true;
                            EditmmmConfig(InstallDirectory);
                        }
                        else
                        {
                            MessageBox.Show("That's not Gorilla Tag.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Process.GetCurrentProcess().Kill();
                    }
                }
            }
        }
        
        private void ShowErrorFindingDirectoryMessage()
        {
            MessageBox.Show("We couldn't seem to find your Gorilla Tag installation, please press \"OK\" and point us to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            NotFoundHandler();
            this.TopMost = true;
        }

        private void EditmmmConfig(string e)
        {
            string newInstallDir = e;
            
            string[] lines = File.ReadAllLines(configFilePath);
            bool found = false;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("InstallDirectory="))
                {
                    lines[i] = "InstallDirectory=" + newInstallDir;
                    found = true;
                    break;
                }
            }
            
            if (!found)
            {
                using (StreamWriter sw = File.AppendText(configFilePath))
                {
                    sw.WriteLine("InstallDirectory=" + newInstallDir);
                }
            }
            else
            {
                File.WriteAllLines(configFilePath, lines);
            }
        }
        private void Instance_Click(object sender, EventArgs e)
        {
            string result = ShowInputDialog("Enter the name for your new Instance.", "Instance Creator");

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Instance name can't be null.", "Instance Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{result}")))
            {
                MessageBox.Show("Instance name is already in use. Try a different name.", "Instance Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{result}")))
            {
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"mmmInstances")))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"mmmInstances"));
                }
                
                Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{result}"));
                byte[] file = DownloadFile("https://github.com/BepInEx/BepInEx/releases/download/v5.4.23.2/BepInEx_win_x64_5.4.23.2.zip");
                UnzipFile(file, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{result}"));
                if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{result}\BepInEx\config")))
                {
                    Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{result}\BepInEx\config"));
                    File.Copy(Path.Combine(InstallDirectory, @"BepInEx\config\BepInEx.cfg"), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{result}\BepInEx\config\BepInEx.cfg"));
                }
                DoorstopInstanceChange(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{result}\BepInEx\core\BepInEx.Preloader.dll"));
                UpdateStatus("Instance Creation Success!");
            }
        }
        
        private void RefrshIns_Click(object sender, EventArgs e)
        {
            AddInstancesToList();
        }
        
        static string ShowInputDialog(string prompt, string title)
        {
            Form form = new Form()
            {
                Width = 300,
                Height = 150,
                Text = title,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            Label label = new Label() { Left = 10, Top = 10, Text = prompt, AutoSize = true };
            TextBox textBox = new TextBox() { Left = 10, Top = 30, Width = 260 };
            Button btnOK = new Button() { Text = "OK", Left = 110, Width = 80, Top = 60, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancel", Left = 200, Width = 80, Top = 60, DialogResult = DialogResult.Cancel };

            form.Controls.Add(label);
            form.Controls.Add(textBox);
            form.Controls.Add(btnOK);
            form.Controls.Add(btnCancel);

            form.AcceptButton = btnOK;
            form.CancelButton = btnCancel;

            btnOK.Click += (sender, e) => form.Close();
            btnCancel.Click += (sender, e) => { textBox.Text = ""; form.Close(); };

            if (form.ShowDialog() == DialogResult.OK)
            {
                return textBox.Text;
            }
            return "";
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                var result = MessageBox.Show("Are you sure you want to enable Instances.", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    InstanceEnabled = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count > 0)
            {
                string checkedItem = checkedListBox1.CheckedItems[0].ToString();
                Directory.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), $@"mmmInstances\{checkedItem}"), true);
                UpdateStatus($"Delete Instance: {checkedItem}");
            }
        }
        
        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (i != e.Index)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            UpdateStatus("Checking for updates...");
            Int16 version = Convert.ToInt16(DownloadSite("https://raw.githubusercontent.com/NgbatzYT/MonkeModManager/master/update"));
            if (version > CurrentVersion)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    MessageBox.Show("A new version is available! Please download it for the latest features.", "Update available!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Process.Start("https://github.com/NgbatzYT/MonkeModManager/releases/latest");
                }));
            }
        }
    }
}