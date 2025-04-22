using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace MonkeModManager
{
    public partial class ConfigEditor : Form
    {
        private readonly string configPath = Path.Combine(Form1.InstallDirectory, @"BepInEx\config\BepInEx.cfg");
        private Dictionary<string, bool> configValues = new Dictionary<string, bool>();

        public ConfigEditor()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            checkedListBox1.DrawMode = DrawMode.OwnerDrawFixed;
            checkedListBox1.DrawItem += checkedListBox1_DrawItem;
            checkedListBox1.ItemCheck += checkedListBox1_ItemCheck;
        }

        private void ConfigEditor_Load(object sender, EventArgs e) => LoadConfig();

        private void LoadConfig()
        {
            checkedListBox1.Items.Clear();
            configValues.Clear();
            if (!File.Exists(configPath))
            {
                UpdateStatus("Config file not found!");
                return;
            }
            int enabledCount = 0;
            foreach (var line in File.ReadLines(configPath))
            {
                if (line.Contains("="))
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim();
                        bool value = parts[1].Trim().ToLower() == "true";
                        if (key == "Enabled")
                        {
                            key = enabledCount == 0 ? "Show Console" : enabledCount == 1 ? "Log to Disk" : key;
                            enabledCount++;
                        }
                        if (key == "HideManagerGameObject")
                        {
                            value = true;
                        }
                        configValues[key] = value;
                        checkedListBox1.Items.Add(key, value);
                    }
                }
            }
            UpdateStatus("Config Loaded Successfully");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(configPath))
            {
                UpdateStatus("Config file not found!");
                return;
            }
            List<string> newConfigLines = new List<string>();
            int enabledCount = 0;
            foreach (var line in File.ReadLines(configPath))
            {
                if (line.Contains("="))
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        string key = parts[0].Trim(), originalKey = key;
                        if (key == "Enabled")
                        {
                            key = enabledCount == 0 ? "Show Console" : enabledCount == 1 ? "Log to Disk" : key;
                            enabledCount++;
                        }
                        if (configValues.ContainsKey(key))
                        {
                            bool isChecked = checkedListBox1.CheckedItems.Contains(key);
                            newConfigLines.Add($"{originalKey}={isChecked.ToString().ToLower()}");
                        }
                        else newConfigLines.Add(line);
                    }
                }
                else newConfigLines.Add(line);
            }
            File.WriteAllLines(configPath, newConfigLines);
            Form1.ConfigFix();
            UpdateStatus("Config Saved Successfully");
        }

        private void button2_Click(object sender, EventArgs e) => LoadConfig();

        private void UpdateStatus(string status) => Invoke((MethodInvoker)(() => label1.Text = $"Status: {status}"));

        private void checkedListBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;
            string text = checkedListBox1.Items[e.Index].ToString();
            bool disabled = text == "HideManagerGameObject";
            bool isChecked = checkedListBox1.GetItemChecked(e.Index);
            e.DrawBackground();
            CheckBoxState state = disabled ? (isChecked ? CheckBoxState.CheckedDisabled : CheckBoxState.UncheckedDisabled) : (isChecked ? CheckBoxState.CheckedNormal : CheckBoxState.UncheckedNormal);
            Size glyphSize = CheckBoxRenderer.GetGlyphSize(e.Graphics, state);
            Point glyphLocation = new Point(e.Bounds.X + 2, e.Bounds.Y + (e.Bounds.Height - glyphSize.Height) / 2);
            CheckBoxRenderer.DrawCheckBox(e.Graphics, glyphLocation, state);
            Rectangle textRect = new Rectangle(glyphLocation.X + glyphSize.Width + 2, e.Bounds.Y, e.Bounds.Width - glyphSize.Width - 4, e.Bounds.Height);
            TextRenderer.DrawText(e.Graphics, text, e.Font, textRect, disabled ? SystemColors.GrayText : e.ForeColor, TextFormatFlags.VerticalCenter);
            e.DrawFocusRectangle();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (checkedListBox1.Items[e.Index].ToString() == "HideManagerGameObject")
                e.NewValue = CheckState.Checked;
        }
    }
}
