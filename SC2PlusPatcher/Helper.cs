using System.IO;

namespace SC2PlusPatcher
{
    public class Helper
    {
        public enum Endian : byte
        {
            Big,
            Little
        }


        public static void writeInt32B(BinaryWriter bw, int Value)
        {
            uint val = (uint)Value;

            uint b0 = (uint)((val & 0x000000FF) << 24);
            uint b1 = (uint)((val & 0x0000FF00) << 8);
            uint b2 = (uint)((val & 0x00FF0000) >> 8);
            uint b3 = (uint)((val & 0xFF000000) >> 24);

            val = (uint)(b0 | b1 | b2 | b3);

            bw.Write(val);
        }

        public static void writeUInt32B(BinaryWriter bw, uint Value)
        {
            uint val = (uint)Value;

            uint b0 = (uint)((val & 0x000000FF) << 24);
            uint b1 = (uint)((val & 0x0000FF00) << 8);
            uint b2 = (uint)((val & 0x00FF0000) >> 8);
            uint b3 = (uint)((val & 0xFF000000) >> 24);

            val = (uint)(b0 | b1 | b2 | b3);

            bw.Write(val);
        }

        public static void writeUInt16B(BinaryWriter bw, ushort Value)
        {
            ushort val = (ushort)Value;

            ushort b0 = (ushort)((val & 0x00FF) << 8);
            ushort b1 = (ushort)((val & 0xFF00) >> 8);

            val = (ushort)(b0 | b1);

            bw.Write(val);
        }

        public static void writeInt16B(BinaryWriter bw, short Value)
        {
            short val = (short)Value;

            ushort b0 = (ushort)((val & 0x00FF) << 8);
            ushort b1 = (ushort)((val & 0xFF00) >> 8);

            val = (short)(b0 | b1);

            bw.Write(val);
        }

        public static byte swapByte(BinaryReader br)
        {
            byte val = br.ReadByte();

            int val2 = val;

            int v0 = ((val2 & 0x33) << 2) | ((val2 & 0xCC) >> 2);
            int v1 = ((v0 & 0xF) << 4) | ((v0 & 0xF0) >> 4);

            byte b0 = (byte)(v1 & 0xFF);

            return b0;
        }

        public static uint swap32(uint val)
        {
            uint b0 = ((val & 0x000000FF) << 24);
            uint b1 = ((val & 0x0000FF00) << 8);
            uint b2 = ((val & 0x00FF0000) >> 8);
            uint b3 = ((val & 0xFF000000) >> 24);

            uint val2 = b0 | b1 | b2 | b3;
            return val2;
        }

        public static short readInt16B(BinaryReader br)
        {
            ushort val = br.ReadUInt16();

            ushort b0 = (ushort)((val & 0x00FF) << 8);
            ushort b1 = (ushort)((val & 0xFF00) >> 8);

            val = (ushort)(b0 | b1);
            short val2 = (short)val;
            return val2;
        }

        public static ushort readUInt16B(BinaryReader br)
        {
            ushort val = br.ReadUInt16();

            ushort b0 = (ushort)((val & 0x00FF) << 8);
            ushort b1 = (ushort)((val & 0xFF00) >> 8);

            val = (ushort)(b0 | b1);
            return val;
        }


        public static int readInt32B(BinaryReader br)
        {
            uint val = br.ReadUInt32();

            uint b0 = ((val & 0x000000FF) << 24);
            uint b1 = ((val & 0x0000FF00) << 8);
            uint b2 = ((val & 0x00FF0000) >> 8);
            uint b3 = ((val & 0xFF000000) >> 24);

            int val2 = (int)(b0 | b1 | b2 | b3);
            return val2;
        }

        public static uint readUInt32B(BinaryReader br)
        {
            uint val = br.ReadUInt32();

            uint b0 = ((val & 0x000000FF) << 24);
            uint b1 = ((val & 0x0000FF00) << 8);
            uint b2 = ((val & 0x00FF0000) >> 8);
            uint b3 = ((val & 0xFF000000) >> 24);

            val = b0 | b1 | b2 | b3;
            return val;
        }

        public static double readDoubleL(BinaryReader br)
        {
            double val = br.ReadDouble();
            return val;
        }

        public static float readFloatL(BinaryReader br)
        {
            float val = br.ReadSingle();
            return val;
        }

        public static uint readUInt32L(BinaryReader br)
        {
            uint val = br.ReadUInt32();
            return val;
        }

        public static int readInt32L(BinaryReader br)
        {
            int val = br.ReadInt32();
            return val;
        }

        public static uint readUInt32(BinaryReader br, Endian e)
        {
            uint val = 0;

            if (e == Endian.Big)
            {
                val = readUInt32B(br);
            }
            else
            {
                val = readUInt32L(br);
            }

            return val;
        }
        public static int readInt32(BinaryReader br, Endian e)
        {
            int val = 0;

            if (e == Endian.Big)
            {
                val = readInt32B(br);
            }
            else
            {
                val = readInt32L(br);
            }

            return val;
        }

        public static void writeUInt32(BinaryWriter bw, uint val, Endian e)
        {
            if (e == Endian.Big)
            {
                //uint v = swap32(val);
                //bw.Write(v);
                writeUInt32B(bw, val);
            }
            else
            {
                bw.Write(val);
            }
        }

        public static void writeInt32(BinaryWriter bw, int val, Endian e)
        {
            if (e == Endian.Big)
            {
                //uint v = swap32((uint)val);
                //bw.Write(v);
                writeInt32B(bw, val);
            }
            else
            {
                bw.Write(val);
            }
        }

        public static void writeInt16(BinaryWriter bw, short val, Endian e)
        {
            if (e == Endian.Big)
            {
                writeInt16B(bw, val);
            }
            else
            {
                bw.Write(val);
            }
        }

        public static void PadBytes(BinaryWriter bw, int size)
        {
            byte[] bytes = new byte[size];
            bw.Write(bytes);
        }

        public static int AlignInt(int val, int align)
        {
            int v;
            int pad;

            pad = align - (val % align);
            v = val + pad;
            return v;
        }
    }
}
