using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Endian = SC2PlusPatcher.Helper.Endian;

namespace SC2PlusPatcher
{
    public class Patcher
    {
        public static string modPath;
        public static string olkPath;
        public static string exePath;

        public static bool bExpand = false;
        public static bool bPatchExe = false;
        public static bool bPatchFiles = false;

        public static Endian endian = Endian.Big;
        public static Console console = Console.GC;

        public static int charInfoOffset;

        public static RichTextBox statusTextBox;

        public enum Console : byte
        {
            GC,
            XBOX,
            PS2
        }

        public static void SetOffsets(Console c)
        {
            switch (c)
            {
                case Console.GC:
                    CSS.tableOffset = 0x24D520;
                    Human.infoOffset = 0x272A60;
                    CSS.xPosOff = 0x2B541C;
                    CSS.idxTableOff = 0x24D958;
                    return;
                case Console.XBOX:
                    CSS.tableOffset = 0x16D670;
                    Human.infoOffset = 0x190FF8;
                    CSS.xPosOff = 0x0;
                    CSS.idxTableOff = 0x16DAA8;
                    return;
                case Console.PS2:
                    CSS.tableOffset = 0x390958;
                    Human.infoOffset = 0x2FBBB8;
                    CSS.xPosOff = 0x0;
                    CSS.idxTableOff = 0x390D90;
                    return;
            }
        }

        public static void WriteString(RichTextBox rtb, string s)
        {
            rtb.Text += s + "\n";
            rtb.SelectionStart = rtb.Text.Length;
            rtb.ScrollToCaret();
        }
    }
}
