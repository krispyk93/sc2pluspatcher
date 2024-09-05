using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC2PlusPatcher
{
    public class PlayStation2
    {

        public static void NewCSS(FileStream fs, BinaryWriter bw)
        {
            byte[] css = new byte[16];

            // Load CSS Data
            using (FileStream fs2 = new FileStream("Patch\\cssps2.bin", FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs2))
                {

                    int size = (int)fs2.Length;
                    css = br.ReadBytes(size);
                }
            }

            fs.Seek(0x390958, SeekOrigin.Begin);
            bw.Write(css);
        }

        public static void CSSIndices(FileStream fs, BinaryWriter bw)
        {
            //byte[] indices = new byte[] { 0x1D, 0x00, 0x13, 0x0B, 0x0D, 0x0E, 0x0F, 0x1D, 0x1D, 0x1D, 0x1D, 0x07, 0x0A,
            //    0x04, 0x1D, 0x10, 0x1D, 0x0C, 0x11, 0x1B, 0x12, 0x09, 0x05, 0x06, 0x15, 0x16, 0x08, 0x01, 0x03, 0x02, 0x17, 0x14, 0x18 };
            //byte[] indices = new byte[] {

            fs.Seek(0x390DAB, SeekOrigin.Begin);
            bw.Write(4); // Link
            fs.Seek(0x390DAD, SeekOrigin.Begin);
            bw.Write(9); // Spawn
        }

        public static void HumanData(FileStream fs, BinaryWriter bw)
        {
            Human.Entry link = Human.LinkEntryCreate_PS2();
            Human.Entry spawn = Human.SpawnEntryCreate_PS2();

            fs.Seek(0x2FD5E0, SeekOrigin.Begin);
            Human.WriteEntry(bw, link);
            fs.Seek(0x2FD7D0, SeekOrigin.Begin);
            Human.WriteEntry(bw, spawn);
        }

        public static void FileIndex(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x2FDBE6, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x03D6, Patcher.endian); // Link
            fs.Seek(0x2FDBEA, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x03FD, Patcher.endian); // Spawn
        }

        public static void EnglishFilesEnable(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x2FDC13, SeekOrigin.Begin);
            bw.Write((byte)1); // Link
            fs.Seek(0x2FDC15, SeekOrigin.Begin);
            bw.Write((byte)1); // Spawn
        }
        public static void CharacterSelectable(FileStream fs, BinaryWriter bw)
        {
            byte[] data = new byte[] { 0x01, 0x01, 0x01, 0x01 };

            //fs.Seek(0x194422, SeekOrigin.Begin); // inferno
            //bw.Write(data);
            fs.Seek(0x2FF112, SeekOrigin.Begin); // Link
            bw.Write(data);
            fs.Seek(0x2FF11E, SeekOrigin.Begin); // Spawn
            bw.Write(data);
        }

        public static void WeaponDemoUnlockBytes(FileStream fs, BinaryWriter bw)
        {
            //fs.Seek(0x195E9E, SeekOrigin.Begin);
            //Helper.writeInt16(bw, 0x2F, Patcher.endian); // Inferno
            fs.Seek(0x2FF6BE, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2F, Patcher.endian); // Link
            fs.Seek(0x2FF6C2, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2F, Patcher.endian); // Spawn
        }

        public static void MuseumUnlockBytes(FileStream fs, BinaryWriter bw)
        {
            //fs.Seek(0x195EE6, SeekOrigin.Begin);
            //Helper.writeInt16(bw, 0x2E, Patcher.endian); // Inferno
            fs.Seek(0x2FF706, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2E, Patcher.endian); // Link
            fs.Seek(0x2FF70A, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2E, Patcher.endian); // Spawn
        }

        public static void ArcadeEndings(FileStream fs, BinaryWriter bw)
        {
            short[] data = new short[4] { 0x004B, 0x000B, 0x0029, 0x0000 };
            fs.Seek(0x35A488, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // inferno
            fs.Seek(0x35A4C8, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // Link
            fs.Seek(0x35A4D8, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // spawn
        }

        public static void Patch(string dolPath)
        {
            // patch elf
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
                    //UnlockEverything(fs, bw);
                    //CenterCSS(fs, bw);
                    //LinkName(fs, bw);
                    NewCSS(fs, bw);
                    CSSIndices(fs, bw);
                    HumanData(fs, bw);
                    FileIndex(fs, bw);
                    EnglishFilesEnable(fs, bw);
                    CharacterSelectable(fs, bw);
                    WeaponDemoUnlockBytes(fs, bw);
                    MuseumUnlockBytes(fs, bw);
                    ArcadeEndings(fs, bw);

                    Patcher.WriteString(Patcher.statusTextBox, String.Format("ELF patch successful!"));
                }
            }
        }
    }
}
