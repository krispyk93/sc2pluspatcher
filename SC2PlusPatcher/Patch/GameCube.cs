using System;
using System.Collections.Generic;
using System.IO;

namespace SC2PlusPatcher
{
    public class GameCube
    {
        public static void SetCostumeCount(FileStream fs, BinaryWriter bw, int id, short count)
        {
            int offset = (id * 0xF8) + 0x272A60 + 0x8A;

            fs.Seek(offset, SeekOrigin.Begin);
            Helper.writeInt16(bw, count, Patcher.endian);
        }

        public static void Costumes(FileStream fs, BinaryWriter bw)
        {
            SetCostumeCount(fs, bw, 1, 6); // Mitsurugi
            SetCostumeCount(fs, bw, 2, 5); // Mina
            SetCostumeCount(fs, bw, 3, 7); // Taki
            SetCostumeCount(fs, bw, 4, 6); // Maxi
            SetCostumeCount(fs, bw, 5, 6); // Voldo
            SetCostumeCount(fs, bw, 6, 8); // Sophitia
            SetCostumeCount(fs, bw, 0xB, 7); // Ivy
            SetCostumeCount(fs, bw, 0xC, 4); // Kilik
            SetCostumeCount(fs, bw, 0xD, 7); // Xianghua
            SetCostumeCount(fs, bw, 0xF, 5); // Yoshimitsu
            SetCostumeCount(fs, bw, 0x11, 7); // Nightmare
            SetCostumeCount(fs, bw, 0x12, 8); // Astaroth
            SetCostumeCount(fs, bw, 0x13, 2); // Inferno
            SetCostumeCount(fs, bw, 0x14, 6); // Cervantes
            SetCostumeCount(fs, bw, 0x15, 6); // Raphael
            SetCostumeCount(fs, bw, 0x16, 6); // Talim
            SetCostumeCount(fs, bw, 0x17, 7); // Cassandra
            SetCostumeCount(fs, bw, 0x18, 5); // Charade
            SetCostumeCount(fs, bw, 0x19, 5); // Necrid
            SetCostumeCount(fs, bw, 0x1A, 5); // Yunsung
            SetCostumeCount(fs, bw, 0x1B, 7); // Link
            SetCostumeCount(fs, bw, 0x1D, 4); // Spawn
            SetCostumeCount(fs, bw, 0x20, 7); // Berserker
        }

        public static void InfernoFlamesDisable(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x3F998, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x2C0000FF);
            fs.Seek(0x3FA1C, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x48003B85); // original
        }
        public static void InfernoFlamesEnable(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x3F998, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x2C000013);
            fs.Seek(0x3FA1C, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x60000000); // not sure what this function does but game crashes if not nop'd
        }
        public static void InfernoFullDamage(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x9A390, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x2C0000FF);
            fs.Seek(0x9A3B4, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x2C0000FF); // checks if attacker is inferno, not sure what this check does.
        }

        public static void InfernoCutsceneDisable(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x8DD70, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x2C0000FF);
        }

        public static void P2InputFix(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x8E268, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x38000004);
        }

        public static void Unknown(FileStream fs, BinaryWriter bw)
        {
            // allows moveset to be used on match start
            fs.Seek(0x271D40, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x1C, Patcher.endian); // heihachi
            Helper.writeInt16(bw, 0x1D, Patcher.endian); // spawn
            Helper.writeInt16(bw, 0x7, Patcher.endian); // siegfried
        }

        public static void OpponentTable(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x8E234, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x2C03001A);
            Helper.writeUInt32B(bw, 0x38A0001A);
        }

        public static void CSSIndices(FileStream fs, BinaryWriter bw)
        {
            byte[] indices = new byte[] { 0x1D, 0x00, 0x13, 0x0B, 0x0D, 0x0E, 0x0F, 0x11, 0x1D, 0x1D, 0x1D, 0x07, 0x0A,
                0x04, 0x1D, 0x10, 0x1D, 0x0C, 0x16, 0x1B, 0x12, 0x09, 0x05, 0x06, 0x1C, 0x15, 0x08, 0x02, 0x01, 0x03, 0x17, 0x14, 0x18 };
            

            fs.Seek(0x24D958, SeekOrigin.Begin);
            bw.Write(indices);
        }

        public static void CharacterSelectable(FileStream fs, BinaryWriter bw)
        {
            byte[] data = new byte[] { 0x01, 0x01, 0x01, 0x01 };

            fs.Seek(0x2764AE, SeekOrigin.Begin); // siegfried
            bw.Write(data);
            fs.Seek(0x2764F6, SeekOrigin.Begin); // inferno
            bw.Write(data);
            fs.Seek(0x27652C, SeekOrigin.Begin); // heihachi
            bw.Write(data);
            fs.Seek(0x276532, SeekOrigin.Begin); // spawn
            bw.Write(data);
        }

        public static void FileIndex(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x274A66, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x0424, Patcher.endian); // siegfried

            fs.Seek(0x274A90, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x03D6, Patcher.endian); // heihachi
            Helper.writeInt16(bw, 0x03FD, Patcher.endian); // spawn
        }

        public static void WeaponDemoUnlockBytes(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x29C21E, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2F, Patcher.endian); // Inferno
            fs.Seek(0x29C230, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2F, Patcher.endian); // Heihachi
            Helper.writeInt16(bw, 0x2F, Patcher.endian); // Spawn
        }

        public static void MuseumUnlockBytes(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x29C262, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2E, Patcher.endian); // Inferno
            fs.Seek(0x29C274, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0x2E, Patcher.endian); // Heihachi
            Helper.writeInt16(bw, 0x2E, Patcher.endian); // Spawn
        }

        public static void EnglishFilesEnable(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x274AB8, SeekOrigin.Begin);
            bw.Write((byte)1); // Heihachi
            bw.Write((byte)1); // Spawn
        }

        public static void HumanData(FileStream fs, BinaryWriter bw)
        {
            Human.Entry heihachi = Human.HeihachiEntryCreate_GCN();
            Human.Entry spawn = Human.SpawnEntryCreate_GCN();

            fs.Seek(0x274580, SeekOrigin.Begin);
            Human.WriteEntry(bw, heihachi);
            Human.WriteEntry(bw, spawn);

            Human.Entry siegfried = Human.SiegfriedEntryCreate_GCN();
            fs.Seek(0x273128, SeekOrigin.Begin);
            Human.WriteEntry(bw, siegfried);
        }

        public static void UnlockStages(FileStream fs, BinaryWriter bw)
        {
            //byte[] bytes = new byte[9] { 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            byte[] bytes = new byte[18] { 1, 0, 0, 
                                         1, 1, 1, 1, 
                                         1, 1, 1, 1,
                                         1, 1, 1, 1,
                                         1, 1, 1};
            fs.Seek(0x2795D5, SeekOrigin.Begin);
            bw.Write(bytes);
        }

        public static void UnlockEverything(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x141E94, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x38600003);
        }

        public static void InfernoStageSelection(FileStream fs, BinaryWriter bw)
        {
            byte[] bytes = new byte[8] { 0x93, 0, 0, 0, 0, 0, 0, 0};

            fs.Seek(0x24DA18, SeekOrigin.Begin);
            Helper.writeInt16(bw, 0xB, Patcher.endian);
            Helper.writeInt16(bw, 1, Patcher.endian);
            bw.Write(bytes);
        }

        public static void NewStageSelection(FileStream fs, BinaryWriter bw)
        {
            //byte[] mp_bytes = new byte[8] { 0x87, 0x8D, 0, 0, 0, 3, 0, 0 }; // money pit
            byte[] mp_bytes = new byte[8] { 0x8D, 0, 0, 0, 3, 0, 0, 0 }; // money pit
            byte[] ec_bytes = new byte[8] { 0x8F, 0, 0, 0, 0, 0, 0, 0 }; // egyptian crypt
            byte[] l_bytes = new byte[8] { 0x8B, 0, 0, 0, 0, 0, 0, 0 }; // labyrinth

            fs.Seek(0x24DA18, SeekOrigin.Begin);

            /* Money pit */
            Helper.writeInt16(bw, 0xE, Patcher.endian);
            Helper.writeInt16(bw, 1, Patcher.endian);
            bw.Write(mp_bytes);

            /* Egyptian Crypt */
            Helper.writeInt16(bw, 0xF, Patcher.endian);
            Helper.writeInt16(bw, 1, Patcher.endian);
            bw.Write(ec_bytes);

            /* Labyrinth */
            Helper.writeInt16(bw, 0x10, Patcher.endian);
            Helper.writeInt16(bw, 1, Patcher.endian);
            bw.Write(l_bytes);


        }

        public static void RandomAlternateStages(FileStream fs, BinaryWriter bw)
        {
            /* Money Pit */
            fs.Seek(0xB1E88, SeekOrigin.Begin);
            Helper.writeUInt32(bw, 0x3B200003, Patcher.endian);

            /* Egyptian Crypt */
            fs.Seek(0xB1EA4, SeekOrigin.Begin);
            Helper.writeUInt32(bw, 0x3B200000, Patcher.endian);

            /* Labyrinth */
            fs.Seek(0xB1EC8, SeekOrigin.Begin);
            Helper.writeUInt32(bw, 0x3B200000, Patcher.endian);
            fs.Seek(0xB1ED0, SeekOrigin.Begin);
            Helper.writeUInt32(bw, 0x3B200000, Patcher.endian);
        }

        public static void SpawnName(FileStream fs, BinaryWriter bw)
        {
            char[] chars = new char[0x11] { 'S','$','l','4','P','$','l','1','2','A','$','l','6', 'W', 'N', '\0', '\0' };

            fs.Seek(0x28BD7C, SeekOrigin.Begin);
            bw.Write(chars);

        }

        public static void CenterCSS(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x2B541C, SeekOrigin.Begin);
            Helper.writeUInt32(bw, 0x43210000, Patcher.endian);
        }

        public static void ArcadeEndings(FileStream fs, BinaryWriter bw)
        {
            short[] data = new short[4] { 0x004B, 0x000B, 0x0000, 0x0029 };
            short[] dataL = new short[4] { 0x0046, 0x000A, 0x0000, 0x0015 };
            short[] dataA = new short[4] { 0x0053, 0x000C, 0x0000, 0x0049 };
            short[] dataB = new short[4] { 0x004C, 0x0009, 0x0000, 0x002D };

            fs.Seek(0x2A0780, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // siegfried
            fs.Seek(0x2A07E0, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // inferno
            fs.Seek(0x2A0828, SeekOrigin.Begin);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // heihachi
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, data[i], Patcher.endian); // spawn
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, dataL[i], Patcher.endian); // lizardman
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, dataA[i], Patcher.endian); // assassin
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, dataB[i], Patcher.endian); // berserker
        }

        public static void InfernoWeaponSkip(FileStream fs, BinaryWriter bw)
        {
            uint data = 0x4814A340;                  // b 0x802977E0
            uint[] data2 = new uint[6] { 0x2C030018, // cmpwi r3, 0x18
                                         0x41820010, // beq 0x802977F4
                                         0x2C030013, // cmpwi r3, 0x13
                                         0x41820008, // beq 0x802977F4
                                         0x4BEB5CB8, // b 0x8014D4A8
                                         0x4BEB5FD4};// b 0x8014D7C8

            fs.Seek(0x14A240, SeekOrigin.Begin); // 8014D4A0
            Helper.writeUInt32(bw, data, Patcher.endian);
            fs.Seek(0x2947E0, SeekOrigin.Begin); // 802977D0
            for (int i = 0; i < 6; i++) Helper.writeUInt32(bw, data2[i], Patcher.endian);
        }

        public static void ArcadeIntro(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0xAA383, SeekOrigin.Begin);
            bw.Write((byte)0xC);
        }

        public static void GameSettings(FileStream fs, BinaryWriter bw)
        {
            // vs 3 rounds
            fs.Seek(0x2845D8, SeekOrigin.Begin);
            Helper.writeInt32(bw, 2, Patcher.endian);
            Helper.writeInt32(bw, 2, Patcher.endian);

            // vs time 50 seconds
            //fs.Seek(0x2846E0, SeekOrigin.Begin);
            //Helper.writeInt32(bw, 4, Patcher.endian);
            //Helper.writeInt32(bw, 4, Patcher.endian);

            // widescreen default
            fs.Seek(0x284A50, SeekOrigin.Begin);
            Helper.writeInt32(bw, 1, Patcher.endian);
            Helper.writeInt32(bw, 1, Patcher.endian);

            // hud
            fs.Seek(0x2848EC, SeekOrigin.Begin); // upper display max
            Helper.writeInt32B(bw, 0x1D0);

            fs.Seek(0x28499C, SeekOrigin.Begin); // lower display max
            Helper.writeInt32B(bw, 0x50);
        }

        public static void SkipScreens(FileStream fs, BinaryWriter bw)
        {
            // nintendo frames
            fs.Seek(0xA9938, SeekOrigin.Begin);
            Helper.writeInt32B(bw, 0x2C1D0002);

            // 3rd party frames
            fs.Seek(0xA9AFC, SeekOrigin.Begin);
            Helper.writeInt32B(bw, 0x2C000000);

        }

        public static void SiegfriedSound(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x26B004, SeekOrigin.Begin); // voice
            Helper.writeUInt32B(bw, 0x8026AB84);

            fs.Seek(0x26B088, SeekOrigin.Begin); // unknown
            Helper.writeUInt32B(bw, 0x80265EE0);

            fs.Seek(0x26B10C, SeekOrigin.Begin); // weapon sfx
            Helper.writeUInt32B(bw, 0x80265EE0);
        }

        public static void SiegfriedIntroWin(FileStream fs, BinaryWriter bw)
        {
            fs.Seek(0x2569D7, SeekOrigin.Begin); // number of intro cutscenes
            bw.Write((byte)4);
            fs.Seek(0x2569FB, SeekOrigin.Begin); // number of win cutscenes
            bw.Write((byte)5);
        }

        public static void SiegfriedWeaponFix(FileStream fs, BinaryWriter bw)
        {
            // related to drawing weapon trail when attacking
            short[] data = new short[12] { 0x0072, 0x0072, 0x0073, 0x0074, 0x0075, 0x0076, 0x0077, 0x0078, 0x0079, 0x007A, 0x007B, 0x007C };

            fs.Seek(0x2A5528, SeekOrigin.Begin);
            for (int i = 0; i < 12; i++) Helper.writeInt16B(bw, data[i]);

            // something to do with the bone the weapon is attached to?
            // fixes his AA move
            fs.Seek(0x276128, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x800A0E20);

        }

        public static void SiegfriedName(FileStream fs, BinaryWriter bw)
        {
            //char[] name = new char[] { 'S','$','1', '1', 'I', '$', '1', '1', 'E', '$', '1', '3', 'G', 'F', 
            //    '$', '1', '2', 'R', '$', '1', '1', 'I', '$', '1', '1', 'E', '$', '1', '2', 'D' };

            char[] name = "SIEGFRIED".ToCharArray();

            fs.Seek(0x28B9F4, SeekOrigin.Begin); // pointer to name
            Helper.writeUInt32B(bw, 0x80297810);

            fs.Seek(0x294810, SeekOrigin.Begin); // name data
            for (int i = 0; i < name.Length; i++) bw.Write(name[i]);
        }

        public static void NewCSS(FileStream fs, BinaryWriter bw, byte[] css)
        {
            fs.Seek(0x24D520, SeekOrigin.Begin);
            bw.Write(css);
        }

        public static byte[] CSS_LoadData(FileStream fs, BinaryReader br)
        {
            byte[] bytes = new byte[16];
            int size = (int)fs.Length;
            bytes = br.ReadBytes(size);

            return bytes;
        }

        public static void FileMallocIncrease(FileStream fs, BinaryWriter bw)
        {
            /* Increase size of file buffer,SYSTEM_DATA buffer and buffer offsets after by 0x10000 */

            /* Total file buffer size */
            fs.Seek(0xB72B8, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3C6000EC);

            /* SYSTEM_DATA size */
            fs.Seek(0xB74A4, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3C60000B);

            /* COMMON MOTION offset */
            fs.Seek(0xB7520, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA4000B);

            /* TEXT_DATA offset */
            fs.Seek(0xB7620, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40013);

            /* SOUND_DATA offset */
            fs.Seek(0xB742C, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3C830014);
            fs.Seek(0xB79A4, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3C830014);
            fs.Seek(0xB7E90, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3C830014);

            /* REG_PIC offset */
            fs.Seek(0xB759C, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40014);

            /* NORMAL_DATA offset */
            fs.Seek(0xB76CC, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3C840019);

            /* EFFECT offset */
            fs.Seek(0xB7A1C, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40019);
            fs.Seek(0xB8114, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40019);

            /* MODEL offset 1 */
            fs.Seek(0xB7B14, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40039);
            fs.Seek(0xB7F08, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40039);

            /* MOTION offset 1 */
            fs.Seek(0xB7B98, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40050);
            fs.Seek(0xB7F90, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40050);

            /* MODEL offset 2 */
            fs.Seek(0xB7C18, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40064);
            fs.Seek(0xB8010, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40064);

            /* MOTION offset 2 */
            fs.Seek(0xB7C9C, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA4007C);
            fs.Seek(0xB8094, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA4007C);

            /* NORMAL(CHRSELE) offset */
            fs.Seek(0xB7AA4, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA4008C);

            /* STAGE offset */
            fs.Seek(0xB8194, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA40090);

            /* READ_TEMP offset */
            fs.Seek(0xB77CC, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA400BD);
            fs.Seek(0xB7DA0, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA400BD);
            fs.Seek(0xB8214, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA400BD);


            /* CROSSFADE offset */
            fs.Seek(0xB7748, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA400DA);
            fs.Seek(0xB7D1C, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA400DA);
            fs.Seek(0xB8290, SeekOrigin.Begin);
            Helper.writeUInt32B(bw, 0x3CA400DA);


        }

        public static void Patch(string dolPath)
        {
            byte[] css = new byte[16];

            // check if dol is valid
            // TODO: Come up with a better method
            using (FileStream fs = new FileStream(dolPath, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    fs.Seek(0x1C, SeekOrigin.Begin);
                    int val = Helper.readInt32B(br);
                    if (val != 0x0020D400)
                    {
                        Patcher.WriteString(Patcher.statusTextBox, String.Format("Unknown DOL. Skipping..."));
                        return;
                    }
                }
            }

            // Load CSS Data
            string cssname = "Patch\\cssgc.bin";

            using (FileStream fs2 = new FileStream(cssname, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs2))
                {
                    css = CSS_LoadData(fs2, br);
                }
            }

            // patch dol
            using (FileStream fs = new FileStream(dolPath, FileMode.Open))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    HumanData(fs, bw);
                    Costumes(fs, bw);
                    //InfernoFlamesDisable(fs, bw);
                    InfernoFlamesEnable(fs, bw);
                    InfernoFullDamage(fs, bw);
                    InfernoCutsceneDisable(fs, bw);
                    P2InputFix(fs, bw);
                    Unknown(fs, bw);
                    OpponentTable(fs, bw);
                    CSSIndices(fs, bw);
                    CharacterSelectable(fs, bw);
                    FileIndex(fs, bw);
                    WeaponDemoUnlockBytes(fs, bw);
                    MuseumUnlockBytes(fs, bw);
                    EnglishFilesEnable(fs, bw);
                    UnlockEverything(fs, bw);
                    UnlockStages(fs, bw);
                    //InfernoStageSelection(fs, bw);
                    NewStageSelection(fs, bw);
                    RandomAlternateStages(fs, bw);
                    InfernoWeaponSkip(fs, bw);
                    SpawnName(fs, bw);
                    CenterCSS(fs, bw);
                    ArcadeEndings(fs, bw);
                    ArcadeIntro(fs, bw);
                    SkipScreens(fs, bw);
                    GameSettings(fs, bw);
                    NewCSS(fs, bw, css);
                    FileMallocIncrease(fs, bw);

                    SiegfriedSound(fs, bw);
                    SiegfriedIntroWin(fs, bw);
                    SiegfriedWeaponFix(fs, bw);
                    SiegfriedName(fs, bw);
                    

                    Patcher.WriteString(Patcher.statusTextBox, String.Format("DOL patch successful!"));
                }
            }
        }
    }
}
