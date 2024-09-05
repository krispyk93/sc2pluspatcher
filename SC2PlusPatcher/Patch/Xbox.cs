using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC2PlusPatcher
{
    public class Xbox
    {
        public static void UnlockEverything(FileStream fs, BinaryWriter bw)
        {
            uint[] asm = new uint[] { 
                0x57B80303,
                0x0303B94A,
                0x000000BF,
                0x68B33B00,
                0xF3AB66AB,
                0xE8B7FEFF,
                0xFF33C0A3,
                0x4CB33B00,
                0xA350B33B,
                0x00A354B3,
                0x3B00A358,
                0xB33B00A3,
                0x5CB33B00,
                0x66A360B3,
                0x3B005FC3
            };

            fs.Seek(0x465F0, SeekOrigin.Begin);
            for (int i = 0; i < 15; i++) Helper.writeUInt32B(bw, asm[i]);
        }

        public static void CenterCSS(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x16A3FC, SeekOrigin.Begin);
            Helper.writeUInt32(bw, 0x43210000, Patcher.endian);
        }

        public static void LinkName(FileStream fs, BinaryWriter bw)
        {
            char[] name = new char[0x10] { 'L', '$', 'l', '2', 'I', 'N', 'K', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0', '\0' };

            fs.Seek(0x16C428, SeekOrigin.Begin);
            bw.Write(name);

        }

        public static void CSSIndices(FileStream fs, BinaryWriter bw)
        {
            byte[] indices = new byte[] { 0x1D, 0x00, 0x13, 0x0B, 0x0D, 0x0E, 0x0F, 0x1D, 0x1D, 0x1D, 0x1D, 0x07, 0x0A,
                0x04, 0x1D, 0x10, 0x1D, 0x0C, 0x11, 0x1B, 0x12, 0x09, 0x05, 0x06, 0x15, 0x16, 0x08, 0x01, 0x03, 0x02, 0x17, 0x14, 0x18 };

            fs.Seek(0x16DAA8, SeekOrigin.Begin);
            bw.Write(indices);
        }

        public static void NewCSS(FileStream fs, BinaryWriter bw)
        {
            byte[] css = new byte[16];

            // Load CSS Data
            using (FileStream fs2 = new FileStream("Patch\\cssxbox.bin", FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs2))
                {

                    int size = (int)fs2.Length;
                    css = br.ReadBytes(size);
                }
            }

            fs.Seek(0x16D670, SeekOrigin.Begin);
            bw.Write(css);
        }

        public static void HumanData(FileStream fs, BinaryWriter bw)
        {
            Human.Entry link = Human.LinkEntryCreate_XBOX();
            Human.Entry heihachi = Human.HeihachiEntryCreate_XBOX();

            fs.Seek(0x192A20, SeekOrigin.Begin);
            Human.WriteEntry(bw, link);
            Human.WriteEntry(bw, heihachi);
        }

        public static void FileIndex(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x193026, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x03D6, Patcher.endian); // Link
            Helper.writeInt16(bw, 0x03FD, Patcher.endian); // Heihachi
        }

        public static void EnglishFilesEnable(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x19304F, SeekOrigin.Begin);
            bw.Write((byte)0); // Link
            bw.Write((byte)1); // Heihachi
        }
        public static void CharacterSelectable(FileStream fs, BinaryWriter bw)
        {
            byte[] data = new byte[] { 0x01, 0x01, 0x01, 0x01 };

            //fs.Seek(0x194422, SeekOrigin.Begin); // inferno
            //bw.Write(data);
            fs.Seek(0x194452, SeekOrigin.Begin); // Link
            bw.Write(data);
            fs.Seek(0x194458, SeekOrigin.Begin); // Heihachi
            bw.Write(data);
        }

        public static void WeaponDemoUnlockBytes(FileStream fs, BinaryWriter bw)
        {
            //fs.Seek(0x195E9E, SeekOrigin.Begin);
            //Helper.writeInt16(bw, 0x2F, Patcher.endian); // Inferno
            fs.Seek(0x195EAE, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2F, Patcher.endian); // Link
            Helper.writeInt16(bw, 0x2F, Patcher.endian); // Heihachi
        }

        public static void MuseumUnlockBytes(FileStream fs, BinaryWriter bw)
        {
            //fs.Seek(0x195EE6, SeekOrigin.Begin);
            //Helper.writeInt16(bw, 0x2E, Patcher.endian); // Inferno
            fs.Seek(0x195EF6, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2E, Patcher.endian); // Link
            Helper.writeInt16(bw, 0x2E, Patcher.endian); // Heihachi
        }

        public static void ArcadeEndings(FileStream fs, BinaryWriter bw)
        {
            short[] data = new short[4] { 0x004B, 0x000B, 0x0029, 0x0000 };
            short[] data2 = new short[4] { 0x0056, 0x000B, 0x0055, 0x0000 }; // fix spawn's
            short[] data3 = new short[4] { 0x0046, 0x000A, 0x0015, 0x0000 }; // sophitia
            short[] data4 = new short[4] { 0x0053, 0x000C, 0x0049, 0x0000 }; // yun sung
            short[] data5 = new short[4] { 0x004C, 0x0009, 0x002D, 0x0000 }; // astaroth

            fs.Seek(0x342980, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // siegfried

            fs.Seek(0x3429E0, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // inferno

            fs.Seek(0x342A20, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // Link
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // Heihachi
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data2[i], Patcher.endian); // Spawn
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data3[i], Patcher.endian); // Lizard Man
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data4[i], Patcher.endian); // Assassin
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data5[i], Patcher.endian); // Berserker
        }

        public static void SiegfriedName(FileStream fs, BinaryWriter bw)
        {
            //char[] name = new char[] { 'S','$','1', '1', 'I', '$', '1', '1', 'E', '$', '1', '3', 'G', 'F', 
            //    '$', '1', '2', 'R', '$', '1', '1', 'I', '$', '1', '1', 'E', '$', '1', '2', 'D' };

            char[] name = "SIEGFRIED".ToCharArray();

            fs.Seek(0x19602C, SeekOrigin.Begin); // pointer to name
            bw.Write(0x179150);

            fs.Seek(0x16C430, SeekOrigin.Begin); // name data
            for (int i = 0; i < name.Length; i++) bw.Write(name[i]);
        }

        public static void SiegfriedIntroWin(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x221EE7, SeekOrigin.Begin); // number of intro cutscenes
            bw.Write((byte)4);
            fs.Seek(0x221F0B, SeekOrigin.Begin); // number of win cutscenes
            bw.Write((byte)5);
        }

        public static void SiegfriedSound(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x3581FC, SeekOrigin.Begin); // voice
            bw.Write(0x003611A0);

            fs.Seek(0x358284, SeekOrigin.Begin); // unknown
            bw.Write(0x003614C0);

            fs.Seek(0x35830C, SeekOrigin.Begin); // weapon sfx
            bw.Write(0x0035C4D0);
        }

        public static void SiegfriedWeaponFix(FileStream fs, BinaryWriter bw)
        {
            // related to drawing weapon trail when attacking
            short[] data = new short[12] { 0x0072, 0x0072, 0x0073, 0x0074, 0x0075, 0x0076, 0x0077, 0x0078, 0x0079, 0x007A, 0x007B, 0x007C };

            fs.Seek(0x32D338, SeekOrigin.Begin);
            for (int i = 0; i < 12; i++) bw.Write(data[i]);

            // something to do with the bone the weapon is attached to?
            // fixes his AA move
            fs.Seek(0x37BF0, SeekOrigin.Begin);
            bw.Write((byte)5);

        }

        public static void Patch(string dolPath)
        {
            // patch xbe
            using (FileStream fs = new FileStream(dolPath, FileMode.Open))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    //InfernoCostumes(fs, bw);
                    //InfernoFlamesDisable(fs, bw);
                    //InfernoCutsceneDisable(fs, bw);
                    //P2InputFix(fs, bw);
                    //Unknown(fs, bw);
                    //OpponentTable(fs, bw);
                    //UnlockStages(fs, bw);
                    //InfernoStageSelection(fs, bw);
                    //InfernoWeaponSkip(fs, bw);
                    //ArcadeIntro(fs, bw);
                    //SkipScreens(fs, bw);
                    //GameSettings(fs, bw);
                    UnlockEverything(fs, bw);
                    CenterCSS(fs, bw);
                    LinkName(fs, bw);
                    CSSIndices(fs, bw);
                    NewCSS(fs, bw);
                    HumanData(fs, bw);
                    FileIndex(fs, bw);
                    EnglishFilesEnable(fs, bw);
                    CharacterSelectable(fs, bw);
                    WeaponDemoUnlockBytes(fs, bw);
                    MuseumUnlockBytes(fs, bw);
                    ArcadeEndings(fs, bw);

                    SiegfriedName(fs, bw);
                    SiegfriedIntroWin(fs, bw);
                    SiegfriedSound(fs, bw);
                    SiegfriedWeaponFix(fs, bw);

                    Patcher.WriteString(Patcher.statusTextBox, String.Format("XBE patch successful!"));
                }
            }
        }
    }
}
