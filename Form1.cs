using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using MonkeModManager.Internals;
using MonkeModManager.Internals.SimpleJSON;

namespace MonkeModManager
{
    public partial class Form1 : Form
    {

        private string DefaultOculusInstallDirectory = @"C:\Program Files\Oculus\Software\Software\another-axiom-gorilla-tag";
        private string DefaultSteamInstallDirectory = @"C:\Program Files (x86)\Steam\steamapps\common\Gorilla Tag";
        public static string InstallDirectory = @"";
        Dictionary<string, int> groups = new Dictionary<string, int>();
        Dictionary<string, int> groupss = new Dictionary<string, int>();
        private List<ReleaseInfo> releases;
        private List<ReleaseInfo> releasesA;
        private bool modsDisabled;
        private int CurrentVersion = 10; // actual version is 2.5.0.0 // (big changes update).(Feature update).(minor update).(hotfix)
        public float ClickerMoney;
        public float ClickPower = 1f;
        public readonly string VersionNumber = "2.5.1.0";
        private int monkeAmount = 5;

        public Form1() => InitializeComponent();

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
                        MessageBox.Show("That's not Gorilla Tag! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonInstall_Click(object sender, EventArgs e) => new Thread(Install).Start();

        private void Install()
        {
            try
            {
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
                        UpdateStatus($"Installed {release.Name}!");
                    }
                }
                UpdateStatus("Install complete!");
                ChangeInstallButtonState(true);
                AddonInstall();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddonInstall()
        {
            try
            {
                UpdateStatus("Installing Addons...");
                ChangeInstallButtonState(false);
                foreach (ReleaseInfo release in releasesA)
                {
                    if (release.Install)
                    {
                        string dir;
                        UpdateStatus(string.Format("Downloading...{0}", release.Name));
                        byte[] file = DownloadFile(release.Link);
                        UpdateStatus(string.Format("Installing...{0}", release.Name));
                        string fileName = Path.GetFileName(release.Link);
                        dir = release.InstallLocation == null ? Path.Combine(InstallDirectory, @"BepInEx\plugins", fileName) : Path.Combine(InstallDirectory, release.InstallLocation);
                        if (!Directory.Exists(dir))
                            Directory.CreateDirectory(dir);
                        File.WriteAllBytes(Path.Combine(dir, fileName), file);
                    }
                }
                UpdateStatus("Install complete!");
                ChangeInstallButtonState(true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            if (release.Name.Contains("BepInEx")) { e.Item.Checked = true; }
            
            release.Install = e.Item.Checked;
        }
        private void listViewed_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            ReleaseInfo release = (ReleaseInfo)e.Item.Tag;

            if (release.Dependencies.Count > 0)
            {
                foreach (ListViewItem item in listViewed.Items)
                {
                    ReleaseInfo plugin = (ReleaseInfo)item.Tag;

                    if (plugin.Name == release.Name) continue;

                    if (release.Dependencies.Contains(plugin.Name))
                    {
                        if (e.Item.Checked)
                        {
                            item.Checked = true;
                            item.ForeColor = Color.DimGray;
                        }
                        else
                        {
                            release.Install = false;
                            if (releases.Count(x => plugin.Dependents.Contains(x.Name) && x.Install) <= 1)
                            {
                                item.Checked = false;
                                item.ForeColor = Color.Black;
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

            if (release.Name.Contains("BepInEx")) { e.Item.Checked = true; }
            
            release.Install = e.Item.Checked;
        }
        private void listViewMods_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            buttonModInfo.Enabled = listViewMods.SelectedItems.Count > 0;
        }
        void listViewMods_DoubleClick(object sender, EventArgs e) => OpenLinkFromRelease();
        void viewInfoToolStripMenuItem_Click(object sender, EventArgs e) => OpenLinkFromRelease();
        void buttonDiscordLink_Click(object sender, EventArgs e) => Process.Start("https://discord.gg/monkemod");

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
                    MessageBox.Show($"Something went wrong! Error: {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    UpdateStatus("Failed to uninstall mods.");
                    return;
                }

                UpdateStatus("All mods uninstalled successfully!");
            }
        }
        void buttonModInfo_Click(object sender, EventArgs e) => OpenLinkFromRelease();

        private void OpenLinkFromRelease()
        {
            if (listViewMods.SelectedItems.Count > 0)
            {
                ReleaseInfo release = (ReleaseInfo)listViewMods.SelectedItems[0].Tag;
                UpdateStatus($"Opening GitHub page for {release.Name}");
                Process.Start(string.Format("https://github.com/{0}", release.GitPath));
            }

        }

        void Form1_Load(object sender, EventArgs e)
        {
            InstallDirectory = Properties.Settings.Default.InstallDirectory;

            releases = [];
            releasesA = [];

            labelVersion.Text = $@"Monke Mod Manager v{VersionNumber}";

            if (InstallDirectory != @"" && Directory.Exists(InstallDirectory))
            {
                textBoxDirectory.Text = InstallDirectory;
            }
            else if (File.Exists(Path.Combine(DefaultSteamInstallDirectory, "Gorilla Tag.exe")))
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
            new Thread(LoadRequiredPlugins).Start();

        }

        private void UpdateStatus(string status)
        {
            string formattedText = $"Status: {status}";
            this.Invoke((MethodInvoker)(() =>
            {
                //Invoke so we can call from any thread
                labelStatus.Text = formattedText;
            }));
        }

        private CookieContainer PermCookie;
        private string DownloadSite(string URL)
        {
            try
            {
                PermCookie ??= new CookieContainer();
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
                MessageBox.Show(
                    ex.Message.Contains("403")
                        ? "Failed to fetch info, GitHub has most likely rate limited you, please check back in 15 - 30 minutes"
                        : "Failed to fetch info, please check your internet connection", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show("You will still be able to use MMM but you won't be able to install mods.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateStatus("Failed to fetch info");
                return null;
            }
        }

        #region LoadReleases

        private void LoadReleases()
        {
            var decodedGroups = JSON.Parse(DownloadSite("https://raw.githubusercontent.com/The-Graze/MonkeModInfo/master/groupinfo.json"));
            var decodedMods = JSON.Parse(DownloadSite("https://raw.githubusercontent.com/The-Graze/MonkeModInfo/master/modinfo.json"));


            var allMods = decodedMods.AsArray;
            var allGroups = decodedGroups.AsArray;

            for (int i = 0; i < allMods.Count; i++)
            {
                JSONNode current = allMods[i];
                ReleaseInfo release = new ReleaseInfo(current["name"], current["author"], current["version"], current["group"], current["download_url"], current["install_location"], current["git_path"],
                    current["mod"], current["dependencies"].AsArray);
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

        private void LoadReleasesCosmetic()
        {
            var adecodedMods = JSON.Parse(DownloadSite("https://raw.githubusercontent.com/NgbatzYT/MonkeModInfo/master/addoninfo.json"));
            var adecodedGroups = JSON.Parse(DownloadSite("https://raw.githubusercontent.com/NgbatzYT/MonkeModInfo/master/addonmodinfo.json"));


            var aallMods = adecodedMods.AsArray;
            var aallGroups = adecodedGroups.AsArray;

            for (int i = 0; i < aallMods.Count; i++)
            {
                JSONNode current = aallMods[i];
                ReleaseInfo release = new ReleaseInfo(current["name"], current["author"], current["version"], current["group"], current["download_url"], current["install_location"], current["git_path"],
                    current["mod"], current["dependencies"].AsArray);
                //UpdateReleaseInfo(ref release);
                releasesA.Add(release);
            }

            IOrderedEnumerable<KeyValuePair<string, JSONNode>> keyValuePairs = aallGroups.Linq.OrderBy(x => x.Value["rank"]);
            for (int i = 0; i < aallGroups.Count; i++)
            {
                JSONNode current = aallGroups[i];
                if (releasesA.Any(x => x.Group == current["name"]))
                {
                    groupss.Add(current["name"], groupss.Count());
                }
            }
            groupss.Add("Uncategorized", groupss.Count);

            foreach (ReleaseInfo release in releasesA)
            {
                foreach (string dep in release.Dependencies)
                {
                    releasesA.Where(x => x.Name == dep).FirstOrDefault()?.Dependents.Add(release.Name);
                }
            }
        }

        #endregion

        public void ConfigFix()
        {
            if (!File.Exists(Path.Combine(InstallDirectory, @"BepInEx\config\BepInEx.cfg"))) {
                var eggs = DownloadSite("https://github.com/The-Graze/MonkeModInfo/raw/refs/heads/master/BepInEx.cfg");
                File.WriteAllText(Path.Combine(InstallDirectory, @"BepInEx\config\BepInEx.cfg"), eggs);
            }

            string c = File.ReadAllText(Path.Combine(InstallDirectory, @"BepInEx\config\BepInEx.cfg"));
            if (!c.Contains("HideManagerGameObject = false")) {
                return;
            }

            string e = c.Replace("HideManagerGameObject = false", "HideManagerGameObject = true");
            File.WriteAllText(Path.Combine(InstallDirectory, @"BepInEx\config\BepInEx.cfg"), e);
        }

        #region LoadRequired

        private void LoadRequiredPlugins()
        {
            UpdateStatus("Getting latest version info...");
            LoadReleases();
            this.Invoke((MethodInvoker)(() =>
            {
                //Invoke so we can call from current thread
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
                    if (!String.IsNullOrEmpty(release.Tag)) { item.Text = string.Format("{0} - ({1})", release.Name, release.Tag); }
                    ;
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
                }

                tabControlMain.Enabled = true;
                buttonInstall.Enabled = true;

            }));

            UpdateStatus("Release info updated!");

            LoadRequiredCosmetic();
        }

        private void LoadRequiredCosmetic()
        {
            UpdateStatus("Getting latest addon info...");
            LoadReleasesCosmetic();
            this.Invoke((MethodInvoker)(() =>
            {
                var includedGroups = new Dictionary<string, int>();

                for (int i = 0; i < groupss.Count(); i++)
                {
                    var key = groupss.First(x => x.Value == i).Key;
                    var value = listViewed.Groups.Add(new ListViewGroup(key, HorizontalAlignment.Left));
                    groupss[key] = value;
                }

                foreach (ReleaseInfo release in releasesA)
                {
                    ListViewItem item = new ListViewItem();
                    item.Text = release.Name;
                    if (!String.IsNullOrEmpty(release.Version)) item.Text = $"{release.Name} - {release.Version} - {release.Mod}";
                    if (!String.IsNullOrEmpty(release.Tag)) { item.Text = string.Format("{0} - ({1})", release.Name, release.Tag); }
                    ;
                    item.SubItems.Add(release.Author);
                    item.SubItems.Add(release.Mod);
                    item.Tag = release;
                    if (release.Install)
                    {
                        listViewed.Items.Add(item);
                    }

                    if (release.Group == null || !groupss.ContainsKey(release.Group))
                    {
                        item.Group = listViewed.Groups[groups["Uncategorized"]];
                    }
                    else if (groupss.ContainsKey(release.Group))
                    {
                        int index = groupss[release.Group];
                        item.Group = listViewed.Groups[index];
                    }
                    release.Install = false;
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

        #endregion

        #region location

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
                            MessageBox.Show("That's not Gorilla Tag.exe! please try again!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        Process.GetCurrentProcess().Kill();

                }
            }
        }

        private void ShowErrorFindingDirectoryMessage()
        {
            MessageBox.Show("We couldn't seem to find your Gorilla Tag installation, please press \"OK\" and point us to it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            NotFoundHandler();
            this.TopMost = true;
        }

        #endregion

        #region Random

        private void EditmmmConfig(string e)
        {
            Properties.Settings.Default.InstallDirectory = e;
            InstallDirectory = Properties.Settings.Default.InstallDirectory;
            Properties.Settings.Default.Save();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CheckForUpdates();
        }
        private void CheckForUpdates()
        {
            UpdateStatus("Checking for updates...");
            short version = Convert.ToInt16(DownloadSite("https://raw.githubusercontent.com/NgbatzYT/MonkeModManager/master/update"));
            if (version > CurrentVersion)
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    MessageBox.Show($"You have an old version of mmm!", "Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Process.Start("https://github.com/NgbatzYT/MonkeModManager/releases/latest");
                    Application.Exit();
                }));
            }
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            ClickerMoney += ClickPower;
            label2.Text = $@"Money: ${ClickerMoney}";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (ClickerMoney >= monkeAmount)
            {
                ClickerMoney -= monkeAmount;
                monkeAmount += monkeAmount;
                label2.Text = $@"Money: ${ClickerMoney}";
                button4.Text = $@"Buy Monke (+0.1 Click Power) - ${monkeAmount}";
                ClickPower += 0.1f;
                label3.Text = $@"Click Power: {ClickPower}";
            }
            else UpdateStatus("Not enough money!");
        }

        #endregion

        private void button1_Click(object sender, EventArgs e) => Process.Start("https://github.com/the-graze/MonkeModManager/releases/latest");
        private void pictureBox1_Click(object sender, EventArgs e) => Process.Start("https://www.youtube.com/watch?v=dQw4w9WgXcQ");
        private void button2_Click(object sender, EventArgs e)
        {
            if (ClickerMoney < 5000000000) return;
            ClickerMoney -= 5000000000;
            BANNa.Visible = true;
            ClickPower += 100f;
            label2.Text = $@"Money: ${ClickerMoney}";
            label3.Text = $@"Click Power: {ClickPower}";
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            var steam = GetSteamPath();
            if (InstallDirectory.Contains("another-axiom-gorilla-tag"))
            {
                ToggleMods(true); return; 
            }
            if (steam == null && checkBox1.Checked) { MessageBox.Show(@"Looks like we couldn't get your steam path. Try again later" ,@"Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            
            ToggleMods(true);
            
            if (checkBox1.Checked)
            {
                Process.Start("\"C:\\Program Files (x86)\\Steam\\steam.exe\" -applaunch 1533390 -windowed");
            }
            else
            {
                Process.Start("steam://rungameid/1533390");
            }
        }
        private void ToggleMods(bool enableMods)
        {
            try
            {
                if (enableMods)
                {
                    if (!File.Exists(Path.Combine(InstallDirectory, "winhttp.dll")))
                    {
                        File.Move(Path.Combine(InstallDirectory, "mods.disabled"), Path.Combine(InstallDirectory, "winhttp.dll"));
                    }
                }
                else
                {
                    if (File.Exists(Path.Combine(InstallDirectory, "winhttp.dll")))
                    {
                        File.Move(Path.Combine(InstallDirectory, "winhttp.dll"), Path.Combine(InstallDirectory, "mods.disabled"));
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string GetSteamPath()
        {
            using RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Valve\Steam");
            object value = key?.GetValue("SteamExe");
            return value?.ToString();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            var steam = GetSteamPath();
            if (InstallDirectory.Contains("another-axiom-gorilla-tag"))
            {
                ToggleMods(false); 
                return; 
            }
            if (steam == null && checkBox1.Checked) { MessageBox.Show(@"Looks like we couldn't get your steam path. Try again later" ,@"Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
            
            ToggleMods(false);
            
            if (checkBox1.Checked)
            {
                Process.Start("\"C:\\Program Files (x86)\\Steam\\steam.exe\" -applaunch 1533390 -windowed");
            }
            else
            {
                Process.Start("steam://rungameid/1533390");
            }
        }
    }
}
