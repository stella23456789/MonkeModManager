using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonkeModManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 f = new Form1();
            if (args.Length > 0 && File.Exists(args[0]))
            {
                Form1.InstallDirectory = Properties.Settings.Default.InstallDirectory;
                f.InstallMMMFile(args[0]); 
            }
            Application.Run(f);
        }
    }
}
