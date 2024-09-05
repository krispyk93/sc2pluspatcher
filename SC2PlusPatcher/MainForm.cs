using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SC2PlusPatcher
{
    public partial class MainForm : Form
    {
        CommonOpenFileDialog folderDialog = new CommonOpenFileDialog();
        CommonOpenFileDialog olkFolderDialog = new CommonOpenFileDialog();
        CommonOpenFileDialog exeFolderDialog = new CommonOpenFileDialog();


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            folderDialog.IsFolderPicker = true;
            //versionComboBox.SelectedIndex = 0;
            //OLK.statusTextBox = statusTextBox;
            Patcher.statusTextBox = statusTextBox;

            // settings
            modTextBox.Text = Patcher.modPath = folderDialog.DefaultFileName = Properties.Settings.Default.ModPath;
            olkTextBox.Text = Patcher.olkPath = olkOpenDialog.FileName = Properties.Settings.Default.OlkPath;
            exeTextBox.Text = Patcher.exePath = exeOpenDialog.FileName = Properties.Settings.Default.ExePath;

            Patcher.bExpand = expandCheckBox.Checked = Properties.Settings.Default.ExpandOlk;
            Patcher.bPatchExe = exePatchCheckBox.Checked = Properties.Settings.Default.PatchExe;
            Patcher.bPatchFiles = olkPatchCheckBox.Checked = Properties.Settings.Default.PatchOlk;

            Patcher.console = (Patcher.Console)Properties.Settings.Default.Version;
            versionComboBox.SelectedIndex = Properties.Settings.Default.Version;
        }

        private void modPathButton_Click(object sender, EventArgs e)
        {
            if (folderDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Properties.Settings.Default.ModPath = modTextBox.Text = Patcher.modPath = folderDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void olkPathButton_Click(object sender, EventArgs e)
        {
            if (olkOpenDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.OlkPath = olkTextBox.Text = Patcher.olkPath = olkOpenDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void exePathButton_Click(object sender, EventArgs e)
        {
            if (exeOpenDialog.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ExePath = exeTextBox.Text = Patcher.exePath = exeOpenDialog.FileName;
                Properties.Settings.Default.Save();
            }
        }

        private void WriteString(RichTextBox rtb, string s)
        {
            rtb.Text += s + "\n";
            rtb.SelectionStart = rtb.Text.Length;
            rtb.ScrollToCaret();
        }

        private void patchButton_Click(object sender, EventArgs e)
        {
            if (Patcher.bExpand)
            {

                if (Patcher.olkPath != null)
                {
                    OLK.Expand(Patcher.olkPath);
                }
                else
                {
                    WriteString(statusTextBox, "OLK path not specified!");
                }
            }

            /*
            WriteString(statusTextBox, "Adding Siegfried...");
            if (Patcher.olkPath != null)
            {
                OLK.ExpandPlus(Patcher.olkPath);
            }
            else
            {
                WriteString(statusTextBox, "OLK path not specified!");
            }
            */

            if (Patcher.bPatchFiles)
            {
                if (Patcher.modPath != null)
                {
                    if (Patcher.olkPath != null)
                    {
                        OLK.ReplaceFiles();
                    }
                    else
                    {
                        WriteString(statusTextBox, "OLK path not specified! Skipping OLK patch...");
                    }
                }
                else
                {
                    WriteString(statusTextBox, "Mod folder path not specified! Skipping OLK patch...");
                }
            }



            if (Patcher.bPatchExe)
            {
                if (Patcher.exePath != null)
                {
                    Patcher.Console c = Patcher.console;

                    switch (c)
                    {
                        case Patcher.Console.GC:
                            GameCube.Patch(Patcher.exePath);
                            break;
                        case Patcher.Console.XBOX:
                            Xbox.Patch(Patcher.exePath);
                            break;
                        case Patcher.Console.PS2:
                            PlayStation2.Patch(Patcher.exePath);
                            //MessageBox.Show("Not implemented yet!");
                            break;
                    }
                }
                else
                {
                    WriteString(statusTextBox, "Executable path not specified!");
                }
            }

        }

        private void expandCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.ExpandOlk =  Patcher.bExpand = expandCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void exePatchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PatchExe = Patcher.bPatchExe = exePatchCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void olkPatchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.PatchOlk = Patcher.bPatchFiles = olkPatchCheckBox.Checked;
            Properties.Settings.Default.Save();
        }

        private void versionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Patcher.Console c = Patcher.console = (Patcher.Console)versionComboBox.SelectedIndex;

            switch (c)
            {
                case Patcher.Console.GC:
                    Patcher.endian = Helper.Endian.Big;
                    exePatchCheckBox.Text = "Patch DOL";
                    exeOpenDialog.FileName = "main.dol";
                    exeOpenDialog.Filter = "DOL files|*.dol|All files|*.*";
                    break;
                case Patcher.Console.XBOX:
                    Patcher.endian = Helper.Endian.Little;
                    exePatchCheckBox.Text = "Patch XBE";
                    exeOpenDialog.FileName = "Default.xbe";
                    exeOpenDialog.Filter = "XBE files|*.xbe|All files|*.*";
                    break;
                case Patcher.Console.PS2:
                    Patcher.endian = Helper.Endian.Little;
                    exePatchCheckBox.Text = "Patch ELF";
                    exeOpenDialog.FileName = "SLUS_206.43";
                    exeOpenDialog.Filter = "ELF files|*.43|All files|*.*";
                    break;
            }

            Properties.Settings.Default.Version = (int)c;
            Properties.Settings.Default.Save();
        }

        private void patchesItem_Click(object sender, EventArgs e)
        {

        }
    }
}
