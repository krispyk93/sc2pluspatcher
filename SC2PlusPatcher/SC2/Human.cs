using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SC2PlusPatcher
{
    public class Human
    {
        public const int Size = 0xF8;
        public static int infoOffset;
        public enum ID : int
        {
            Dummy,
            Mitsurugi,
            SeungMina,
            Taki,
            Maxi,
            Voldo,
            Sophitia,
            Dummy2,
            Dummy3,
            Dummy4,
            Dummy5,
            Ivy,
            Kilik,
            Xianghua,
            Dummy6,
            Yoshimitsu,
            Dummy7,
            Nightmare,
            Astaroth,
            Inferno,
            Cervantes,
            Raphael,
            Talim,
            Cassandra,
            Charade,
            Necrid,
            YunSeong,
            Link,
            Heihachi,
            Spawn,
            LizardMan,
            Assassin,
            Berserker,
            Null = -1,
            Random = -2
        }

        public class Entry
        {
            public uint NameEn;
            public uint NameJpn;
            public int[] P0 = new int[4];
            public uint NameUpperEn;
            public uint NameUpperJpn;
            public int[] P1 = new int[4];
            public uint NameWeapEn;
            public uint NameWeapJpn;
            public int[] P2 = new int[4];
            public uint NameLocEn;
            public uint NameLocJpn;
            public int[] P3 = new int[4];
            public uint NameUnk;
            public int[] P4 = new int[5];
            public short[] Unk = new short[5];
            public short[] HumanIds = new short[4];
            public short CostumeCount;
            public short[] Unk2 = new short[14];
            public float[] Unk3 = new float[4];
            public byte[] Unk4 = new byte[64];

            public Entry()
            {

            }
        }

        public static void WriteEntry(BinaryWriter bw, Entry entry)
        {
            Helper.writeUInt32(bw, entry.NameEn, Patcher.endian);
            Helper.writeUInt32(bw, entry.NameJpn, Patcher.endian);
            for (int i = 0; i < 4; i++) Helper.writeInt32(bw, entry.P0[i], Patcher.endian);
            Helper.writeUInt32(bw, entry.NameUpperEn, Patcher.endian);
            Helper.writeUInt32(bw, entry.NameUpperJpn, Patcher.endian);
            for (int i = 0; i < 4; i++) Helper.writeInt32(bw, entry.P1[i], Patcher.endian);
            Helper.writeUInt32(bw, entry.NameWeapEn, Patcher.endian);
            Helper.writeUInt32(bw, entry.NameWeapJpn, Patcher.endian);
            for (int i = 0; i < 4; i++) Helper.writeInt32(bw, entry.P2[i], Patcher.endian);
            Helper.writeUInt32(bw, entry.NameLocEn, Patcher.endian);
            Helper.writeUInt32(bw, entry.NameLocJpn, Patcher.endian);
            for (int i = 0; i < 4; i++) Helper.writeInt32(bw, entry.P3[i], Patcher.endian);
            Helper.writeUInt32(bw, entry.NameUnk, Patcher.endian);
            for (int i = 0; i < 5; i++) Helper.writeInt32(bw, entry.P4[i], Patcher.endian);
            for (int i = 0; i < 5; i++) Helper.writeInt16(bw, entry.Unk[i], Patcher.endian);
            for (int i = 0; i < 4; i++) Helper.writeInt16(bw, entry.HumanIds[i], Patcher.endian);
            Helper.writeInt16(bw, entry.CostumeCount, Patcher.endian);
            for (int i = 0; i < 14; i++) Helper.writeInt16(bw, entry.Unk2[i], Patcher.endian);
            for (int i = 0; i < 4; i++)
            {
                byte[] bytes = BitConverter.GetBytes(entry.Unk3[i]);
                int j = BitConverter.ToInt32(bytes, 0);
                Helper.writeInt32(bw, j, Patcher.endian);
            }
            bw.Write(entry.Unk4);
        }

        public static Entry LinkEntryCreate_XBOX()
        {
            Entry entry = new Entry();

            entry.NameEn = 0x177F70;
            entry.NameJpn = 0x177F64;
            entry.NameUpperEn = 0x177F58;
            entry.NameUpperJpn = 0x177F50;
            entry.NameWeapEn = 0x177F44;
            entry.NameWeapJpn = 0x177F40;
            entry.NameLocEn = 0x177F34;
            entry.NameLocJpn = 0x177F40;
            entry.NameUnk = 0x177F30;
            entry.Unk = new short[5] { 0x0014, 0x00A4, 0x0030, 0x0000, 0x0015 };
            entry.HumanIds = new short[4] { 0x001B, 0x001B, 0x001B, 0x001B };
            entry.CostumeCount = 4;
            entry.Unk2 = new short[14] { 
                0x0001, 0x0007, 0x0009, 0x000D, 0x0001, 0x0007, 0x000E, 0x010D,
                0x0710, 0x0007, 0x0009, 0x000D, 0x0001, 0x0007 
            };
            entry.Unk3 = new float[4] { 1.0f, 1.0f, 1.0f, 1.0f };
            entry.Unk4 = new byte[64] { 03, 03, 03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00, 00, 00,
                                        00, 00, 00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03, 03, 03,
                                        03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00,
                                        00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03 };
            return entry;
        }

        public static Entry HeihachiEntryCreate_XBOX()
        {
            Entry entry = new Entry();

            entry.NameEn = 0x177F70;
            entry.NameJpn = 0x177F64;
            entry.NameUpperEn = 0x177F58;
            entry.NameUpperJpn = 0x177F50;
            entry.NameWeapEn = 0x177F44;
            entry.NameWeapJpn = 0x177F40;
            entry.NameLocEn = 0x177F34;
            entry.NameLocJpn = 0x177F40;
            entry.NameUnk = 0x177F30;
            entry.Unk = new short[5] { 0x004B, 0x00B0, 0x0048, 0x0000, 0x0015 };
            entry.HumanIds = new short[4] { 0x001C, 0x001C, 0x001C, 0x001C };
            entry.CostumeCount = 3;
            entry.Unk2 = new short[14] { 
                0x0005, 0x000D, 0x0007, 0x000E, 0x0005, 0x000D, 0x030E, 0x010C,
                0x0710, 0x0005, 0x0000, 0x0000, 0x0000, 0x0000 
            };
            entry.Unk3 = new float[4] { 1.0f, 1.0f, 1.0f, 1.0f };
            entry.Unk4 = new byte[64] { 03, 03, 03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00, 00, 00,
                                        00, 00, 00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03, 03, 03,
                                        03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00,
                                        00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03 };
            return entry;
        }

        public static Entry HeihachiEntryCreate_GCN()
        {
            Entry entry = new Entry();

            entry.NameEn = 0x80279CD8;
            entry.NameJpn = 0x80279CDD;
            entry.NameUpperEn = 0x80279CE4;
            entry.NameUpperJpn = 0x80279CDD;
            entry.NameWeapEn = 0x80279CE9;
            entry.NameWeapJpn = 0x80279CF6;
            entry.NameLocEn = 0x80279D05;
            entry.NameLocJpn = 0x80279CA7;
            entry.NameUnk = 0x80279D0C;
            entry.Unk = new short[5] { 0x004B, 0x00B0, 0x0048, 0x0000, 0x0015 };
            entry.HumanIds = new short[4] { 0x001C, 0x001C, 0x001C, 0x001C };
            entry.CostumeCount = 2;
            entry.Unk2 = new short[14] { 
                0x0005, 0x000D, 0x0007, 0x000E, 0x0005, 0x000D, 0x030E, 0x010C, 
                0x0710, 0x0005, 0x0000, 0x0000, 0x0000, 0x0000 
            };
            entry.Unk3 = new float[4] { 1.0f, 1.0f, 1.0f, 1.0f};
            entry.Unk4 = new byte[64] { 03, 03, 03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00, 00, 00,
                                        00, 00, 00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03, 03, 03,
                                        03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00,
                                        00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03 };
            return entry;
        }

        public static Entry SpawnEntryCreate_GCN()
        {
            Entry entry = new Entry();

            entry.NameEn = 0x80279CD8;
            entry.NameJpn = 0x80279CDD;
            entry.NameUpperEn = 0x80279CE4;
            entry.NameUpperJpn = 0x80279CDD;
            entry.NameWeapEn = 0x80279CE9;
            entry.NameWeapJpn = 0x80279CF6;
            entry.NameLocEn = 0x80279D05;
            entry.NameLocJpn = 0x80279CA7;
            entry.NameUnk = 0x80279D0C;
            entry.Unk = new short[5] { 0x0012, 0x00B0, 0x0048, 0x0000, 0x0015 };
            entry.HumanIds = new short[4] { 0x001D, 0x001D, 0x001D, 0x001D };
            entry.CostumeCount = 4;
            entry.Unk2 = new short[14] { 
                0x0009, 0x0008, 0x000E, 0x000D, 0x0009, 0x000E, 0x0710, 0x010D, 
                0x0003, 0x0004, 0x0000, 0x0000, 0x0000, 0x0000 
            };
            entry.Unk3 = new float[4] { 1.0f, 1.0f, 1.0f, 1.0f };
            entry.Unk4 = new byte[64] { 03, 03, 03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00, 00, 00,
                                        00, 00, 00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03, 03, 03,
                                        03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00,
                                        00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03 };
            return entry;
        }

        public static Entry SiegfriedEntryCreate_GCN()
        {
            Entry entry = new Entry();

            entry.NameEn = 0x80279A9E;
            entry.NameJpn = 0x80279AA8;
            entry.NameUpperEn = 0x80279AB3;
            entry.NameUpperJpn = 0x80279AA8;
            entry.NameWeapEn = 0x80279ABD;
            entry.NameWeapJpn = 0x802799DB;
            entry.NameLocEn = 0x80279A1D;
            entry.NameLocJpn = 0x80279A7B;
            entry.NameUnk = 0x80279A1D;
            entry.Unk = new short[5] { 0x00FF, 0x00A9, 0x0083, 0x0000, 0x0015 };
            entry.HumanIds = new short[4] { 0x0007, 0x0000, 0x0007, 0x0007 };
            entry.CostumeCount = 4;
            entry.Unk2 = new short[14] {
                0x0006, 0x0009, 0x0003, 0x0008, 0x0006, 0x000D, 0x0002, 0x0710,
                0x010E, 0x0004, 0x0000, 0x0000, 0x0000, 0x0000
            };
            entry.Unk3 = new float[4] { 1.0f, 1.0f, 1.0f, 1.0f };
            entry.Unk4 = new byte[64] { 03, 03, 03, 02, 02, 02, 02, 02, 02, 00, 00, 00, 00, 00, 00, 00,
                                        00, 00, 00, 01, 01, 01, 01, 01, 03, 03, 03, 03, 03, 03, 03, 03,
                                        03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00,
                                        00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03 };
            return entry;
        }

        public static Entry LinkEntryCreate_PS2()
        {
            Entry entry = new Entry();

            entry.NameEn = 0x489F30;
            entry.NameJpn = 0x489F48;
            entry.NameUpperEn = 0x489F58;
            entry.NameUpperJpn = 0x489F68;
            entry.NameWeapEn = 0x489F70;
            entry.NameWeapJpn = 0x489F80;
            entry.NameLocEn = 0x489F88;
            entry.NameLocJpn = 0x489F80;
            entry.NameUnk = 0x489F98;
            entry.Unk = new short[5] { 0x0014, 0x00A4, 0x0030, 0x0000, 0x0015 };
            entry.HumanIds = new short[4] { 0x001B, 0x001B, 0x001B, 0x001B };
            entry.CostumeCount = 3;
            entry.Unk2 = new short[14] {
                0x0001, 0x0007, 0x0009, 0x000D, 0x0001, 0x0007, 0x000E, 0x010D,
                0x0710, 0x0007, 0x0009, 0x000D, 0x0001, 0x0007
            };
            entry.Unk3 = new float[4] { 1.0f, 1.0f, 1.0f, 1.0f };
            entry.Unk4 = new byte[64] { 03, 03, 03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00, 00, 00,
                                        00, 00, 00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03, 03, 03,
                                        03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00,
                                        00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03 };
            return entry;
        }

        public static Entry SpawnEntryCreate_PS2()
        {
            Entry entry = new Entry();

            entry.NameEn = 0x489F30;
            entry.NameJpn = 0x489F48;
            entry.NameUpperEn = 0x489F58;
            entry.NameUpperJpn = 0x489F68;
            entry.NameWeapEn = 0x489F70;
            entry.NameWeapJpn = 0x489F80;
            entry.NameLocEn = 0x489F88;
            entry.NameLocJpn = 0x489F80;
            entry.NameUnk = 0x489F98;
            entry.Unk = new short[5] { 0x0012, 0x00B0, 0x0048, 0x0000, 0x0015 };
            entry.HumanIds = new short[4] { 0x001D, 0x001D, 0x001D, 0x001D };
            entry.CostumeCount = 3;
            entry.Unk2 = new short[14] {
                0x0009, 0x0008, 0x000E, 0x000D, 0x0009, 0x000E, 0x0710, 0x010D,
                0x0003, 0x0004, 0x0000, 0x0000, 0x0000, 0x0000
            };
            entry.Unk3 = new float[4] { 1.0f, 1.0f, 1.0f, 1.0f };
            entry.Unk4 = new byte[64] { 03, 03, 03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00, 00, 00,
                                        00, 00, 00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03, 03, 03,
                                        03, 03, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 02, 00, 00,
                                        00, 00, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 01, 03, 03 };
            return entry;
        }
    }
}
