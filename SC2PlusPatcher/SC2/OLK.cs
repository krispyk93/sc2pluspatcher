using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SC2PlusPatcher
{
    public class OLK
    {
        public static List<string> folders = new List<string> { 
            "motinfo", "cpudata", "cdata", "stage", "human" 
        };

        public static Header rootHeader;

        public class Header
        {
            public int Count;
            public char[] Magic = new char[4] {'o', 'l', 'n', 'k' };
            public int Unk1;
            public uint Unk2;

            public Entry RootEntry;
            public List<Entry> Entries = new List<Entry>();

            public int Offset;

            public List<Header> OLKEntries = new List<Header>();
            public int OLKCount;

            public Header()
            {

            }

            public Header(BinaryReader br, int offset)
            {
                Count = br.ReadInt32();
                Magic = br.ReadChars(4);
                Unk1 = br.ReadInt32();
                Unk2 = br.ReadUInt32();

                RootEntry = new Entry(br);

                for (int i = 0; i < Count; i++)
                {
                    Entries.Add(new Entry(br));
                }

                Offset = offset;
            }
        }
        public class Entry {
            public int Offset;
            public int Size;
            public uint Dt;
            public uint Unk;

            public Entry(BinaryReader br)
            {
                Offset = br.ReadInt32();
                Size = br.ReadInt32();
                Dt = br.ReadUInt32();
                Unk = br.ReadUInt32();
            }

            public Entry(int offset, int size, uint dt, uint unk)
            {
                Offset = offset;
                Size = size;
                Dt = dt;
                Unk = unk;
            }
        }

        public class FlistEntry
        {
            public int FileIndex;
            public string FilePath;

        }

        public static void WriteEntry(BinaryWriter bw, Entry entry)
        {
            bw.Write(entry.Offset);
            bw.Write(entry.Size);
            bw.Write(entry.Dt);
            bw.Write(entry.Unk);
        }

        public static Header GetEntries(FileStream fs, BinaryReader br)
        {
            Header rootHead = new Header(br, 0);

            for (int i = 0; i < rootHead.Count; i++)
            {
                OLK.Entry entry = rootHead.Entries[i];
                int offset = entry.Offset + rootHead.RootEntry.Offset + rootHead.Offset;
                // get olk entries
                fs.Seek(offset, SeekOrigin.Begin);
                rootHead.OLKEntries.Add(new Header(br, offset));
            }

            return rootHead;
        }

        public static void Expand(string olkPath)
        {
            int mitsuIdx = 0x2E;
            int nightmareIdx = 0x1B4;

            int humanFiles = 0x27;
            int addTableSize = 0x1000;

            int mitsuStart = 0;
            int mitsuEnd = 0;
            int mitsuSize = 0;

            int nightmareStart = 0;
            int nightmareEnd = 0;
            int nightmareSize = 0;

            int olkDataStart = 0;
            int olkDataEnd = 0;

            byte[] olkData = new byte[16];
            byte[] mitsuData = new byte[16];
            byte[] nightmareData = new byte[16];

            byte[] newTableData = new byte[addTableSize];

            Patcher.WriteString(Patcher.statusTextBox, String.Format("Expanding OLK..."));

            using (FileStream fs = new FileStream(olkPath, FileMode.Open))
            {

                // Get OLK information
                using (BinaryReader br = new BinaryReader(fs))
                {
                    rootHeader = GetEntries(fs, br);

                    Patcher.WriteString(Patcher.statusTextBox, String.Format("{0} loaded!", Patcher.olkPath));
                    // get mitsurugi data
                    Header olkEntry = rootHeader.OLKEntries[4];

                    if (olkEntry.Count > 0x424)
                    {
                        Patcher.WriteString(Patcher.statusTextBox, String.Format("OLK already expanded. Aborting..."));

                        return;
                    }

                    mitsuStart = olkEntry.Entries[mitsuIdx].Offset + olkEntry.RootEntry.Offset + olkEntry.Offset;
                    mitsuEnd = olkEntry.Entries[mitsuIdx + humanFiles].Offset + olkEntry.RootEntry.Offset + olkEntry.Offset;
                    mitsuSize = mitsuEnd - mitsuStart;

                    nightmareStart = olkEntry.Entries[nightmareIdx].Offset + olkEntry.RootEntry.Offset + olkEntry.Offset;
                    nightmareEnd = olkEntry.Entries[nightmareIdx + humanFiles].Offset + olkEntry.RootEntry.Offset + olkEntry.Offset;
                    nightmareSize = nightmareEnd - nightmareStart;

                    //Patcher.WriteString(Patcher.statusTextBox, String.Format("Mitsurugi Files Offset: {0:X8} - {1:X8}", mitsuStart, mitsuEnd));

                    // read mitsurugi's data
                    fs.Seek(mitsuStart, SeekOrigin.Begin);
                    mitsuData = br.ReadBytes(mitsuSize);

                    // read nightmare's data
                    fs.Seek(nightmareStart, SeekOrigin.Begin);
                    nightmareData = br.ReadBytes(nightmareSize);

                    // get rest of root olk data

                    olkDataStart = olkEntry.RootEntry.Offset + olkEntry.Offset;
                    olkDataEnd = rootHeader.RootEntry.Offset + rootHeader.RootEntry.Size;
                    int olkDataSize = olkDataEnd - olkDataStart;

                    //Patcher.WriteString(Patcher.statusTextBox, String.Format("OLK Data Offset: {0:X8} - {1:X8}", olkDataStart, olkDataEnd));

                    fs.Seek(olkDataStart, SeekOrigin.Begin);
                    olkData = br.ReadBytes(olkDataSize);
                }
            }


            using (FileStream fs = new FileStream(olkPath, FileMode.Open))
            {
                // expand olk
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    int newDataStart = olkDataStart + addTableSize;

                    // expand table
                    fs.Seek(olkDataStart, SeekOrigin.Begin);
                    bw.Write(newTableData);

                    // write olk data
                    bw.Write(olkData);

                    // write mitsurugi data
                    bw.Write(mitsuData);
                    bw.Write(mitsuData);

                    // write nightmare data
                    bw.Write(nightmareData);
                    bw.Write(nightmareData);

                    // add new file offsets/sizes
                    Header olkEntry = rootHeader.OLKEntries[4];
                    int entryOffset = (olkEntry.Count * 0x10) + olkEntry.Offset + 0x20;

                    Patcher.WriteString(Patcher.statusTextBox, String.Format("Adding entries to human OLK...", entryOffset));

                    int addSize;

                    // heihachi/spawn
                    fs.Seek(entryOffset, SeekOrigin.Begin);
                    for (int i = 0; i < 2; i++)
                    {
                        addSize = (i * mitsuSize) + (olkDataEnd - mitsuStart);
                        for (int j = 0; j < humanFiles; j++)
                        {
                            int offset = olkEntry.Entries[mitsuIdx + j].Offset + addSize;
                            int size = olkEntry.Entries[mitsuIdx + j].Size;
                            uint dt = olkEntry.Entries[mitsuIdx + j].Dt;

                            Entry entry = new Entry(offset, size, dt, 0);
                            WriteEntry(bw, entry);
                        }
                    }


                    // Nightmare
                    addSize = (2 * mitsuSize) + (olkDataEnd - nightmareStart);
                    for (int j = 0; j < humanFiles; j++)
                    {
                        int offset = olkEntry.Entries[nightmareIdx + j].Offset + addSize;
                        int size = olkEntry.Entries[nightmareIdx + j].Size;
                        uint dt = olkEntry.Entries[nightmareIdx + j].Dt;

                        Entry entry = new Entry(offset, size, dt, 0);
                        WriteEntry(bw, entry);
                    }

                    // fix human olk count/offsets/size
                    fs.Seek(olkEntry.Offset, SeekOrigin.Begin);
                    bw.Write(olkEntry.Count + (humanFiles * 3)); //  * new characters added 
                    fs.Seek(olkEntry.Offset + 0x10, SeekOrigin.Begin);
                    bw.Write(olkEntry.RootEntry.Offset + addTableSize);
                    bw.Write(olkEntry.RootEntry.Size + (mitsuSize * 2) + nightmareSize);

                    // fix root olk size end human entry size
                    int newSize = (mitsuSize * 2) + nightmareSize + addTableSize;
                    fs.Seek(0x14, SeekOrigin.Begin);
                    bw.Write(rootHeader.RootEntry.Size + newSize);
                    fs.Seek(0x64, SeekOrigin.Begin);
                    bw.Write(rootHeader.Entries[4].Size + newSize);

                    Patcher.WriteString(Patcher.statusTextBox, String.Format("Finished expanding OLK!"));
                }
            }

            olkData = new byte[16];
            mitsuData = new byte[16];
            nightmareData = new byte[16];
            newTableData = new byte[16];
            GC.Collect();
        }

        public static void ExpandPlus(string olkPath)
        {
            int nightmareIdx = 0x1B4;
            int humanFiles = 0x27;
            //int addTableSize = 0x1000;

            int nightmareStart = 0;
            int nightmareEnd = 0;
            int nightmareSize = 0;
            int olkDataStart = 0;
            int olkDataEnd = 0;

            byte[] olkData = new byte[16];
            byte[] nightmareData = new byte[16];
            //byte[] newTableData = new byte[addTableSize];

            Patcher.WriteString(Patcher.statusTextBox, String.Format("Expanding OLK..."));

            using (FileStream fs = new FileStream(olkPath, FileMode.Open))
            {

                // Get OLK information
                using (BinaryReader br = new BinaryReader(fs))
                {
                    rootHeader = GetEntries(fs, br);

                    Patcher.WriteString(Patcher.statusTextBox, String.Format("{0} loaded!", Patcher.olkPath));

                    // get nightmare data
                    Header olkEntry = rootHeader.OLKEntries[4];

                    if (olkEntry.Count > 0x424)
                    {
                        Patcher.WriteString(Patcher.statusTextBox, String.Format("OLK already expanded. Aborting..."));

                        return;
                    }

                    nightmareStart = olkEntry.Entries[nightmareIdx].Offset + olkEntry.RootEntry.Offset + olkEntry.Offset;
                    nightmareEnd = olkEntry.Entries[nightmareIdx + humanFiles].Offset + olkEntry.RootEntry.Offset + olkEntry.Offset;
                    nightmareSize = nightmareEnd - nightmareStart;

                    //Patcher.WriteString(Patcher.statusTextBox, String.Format("Mitsurugi Files Offset: {0:X8} - {1:X8}", mitsuStart, mitsuEnd));

                    fs.Seek(nightmareStart, SeekOrigin.Begin);
                    nightmareData = br.ReadBytes(nightmareSize);

                    // get rest of root olk data

                    olkDataStart = olkEntry.RootEntry.Offset + olkEntry.Offset;
                    olkDataEnd = rootHeader.RootEntry.Offset + rootHeader.RootEntry.Size;
                    int olkDataSize = olkDataEnd - olkDataStart;

                    //Patcher.WriteString(Patcher.statusTextBox, String.Format("OLK Data Offset: {0:X8} - {1:X8}", olkDataStart, olkDataEnd));

                    //fs.Seek(olkDataStart, SeekOrigin.Begin);
                    //olkData = br.ReadBytes(olkDataSize);
                }
            }
            using (FileStream fs = new FileStream(olkPath, FileMode.Open))
            {
                // expand olk
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    //int newDataStart = olkDataStart + addTableSize;

                    // expand table
                    //fs.Seek(olkDataStart, SeekOrigin.Begin);
                    //bw.Write(newTableData);

                    // write olk data
                    //bw.Write(olkData);


                    // write nightmare data
                    fs.Seek(olkDataEnd, SeekOrigin.Begin);
                    bw.Write(nightmareData);
                    bw.Write(nightmareData);

                    // add new file offsets/sizes
                    Header olkEntry = rootHeader.OLKEntries[4];
                    int entryOffset = (olkEntry.Count * 0x10) + olkEntry.Offset + 0x20;

                    Patcher.WriteString(Patcher.statusTextBox, String.Format("Adding entries to human OLK...", entryOffset));

                    fs.Seek(entryOffset, SeekOrigin.Begin);
                    int addSize = (olkDataEnd - nightmareStart);
                    for (int j = 0; j < humanFiles; j++)
                    {
                        int offset = olkEntry.Entries[nightmareIdx + j].Offset + addSize;
                        int size = olkEntry.Entries[nightmareIdx + j].Size;
                        uint dt = olkEntry.Entries[nightmareIdx + j].Dt;

                        Entry entry = new Entry(offset, size, dt, 0);
                        WriteEntry(bw, entry);
                    }

                    // fix human olk count/offsets/size
                    fs.Seek(olkEntry.Offset, SeekOrigin.Begin);
                    bw.Write(olkEntry.Count + humanFiles);
                    fs.Seek(olkEntry.Offset + 0x14, SeekOrigin.Begin);
                    //bw.Write(olkEntry.RootEntry.Offset + addTableSize);
                    bw.Write(olkEntry.RootEntry.Size + nightmareSize);

                    // fix root olk size end human entry size
                    int newSize = nightmareSize;// + addTableSize;
                    fs.Seek(0x14, SeekOrigin.Begin);
                    bw.Write(rootHeader.RootEntry.Size + newSize);
                    fs.Seek(0x64, SeekOrigin.Begin);
                    bw.Write(rootHeader.Entries[4].Size + newSize);

                    Patcher.WriteString(Patcher.statusTextBox, String.Format("Finished expanding OLK!"));
                }
            }

            olkData = new byte[16];
            nightmareData = new byte[16];
            //newTableData = new byte[16];
            GC.Collect();
        }

        public static List<FlistEntry> FlistGetEntries(string flistPath, string folderPath)
        {
            List<FlistEntry> flistFiles = new List<FlistEntry>();

            using (FileStream fs = new FileStream(flistPath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (!line.Contains("#")) // skip # comments
                        {
                            FlistEntry flistFile = new FlistEntry();
                            int idx = Convert.ToInt32(line.Split()[0], 16);
                            string fn = line.Split()[1];
                            flistFile.FileIndex = idx;
                            flistFile.FilePath = Path.Combine(folderPath, fn);

                            flistFiles.Add(flistFile);
                        }
                    }
                }
            }

            return flistFiles;
        }

        public static byte[] LoadFile(string fileName)
        {
            byte[] f = new byte[16];

            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    int size = (int)fs.Length;
                    f = br.ReadBytes(size);
                    //Patcher.WriteString(Patcher.statusTextBox, String.Format("Size: {0:X8}", size));
                }
            }
            return f;
        }

        public static void FixEntryOffsets(FileStream fs, BinaryWriter bw, Header h, int sizeDif, int idx)
        {
            for (int i = idx + 1; i < h.Count; i++)
            {
                int offset = h.Entries[i].Offset += sizeDif;
                int entryOffset = h.Offset + 0x20 + (i * 0x10);
                fs.Seek(entryOffset, SeekOrigin.Begin);
                bw.Write(offset);
            }
        }

        public static void ReplaceFile(FileStream fs, BinaryWriter bw, FlistEntry f, ref Header rh, int olkIdx, byte[] newFile)
        {
            //Header olkEntry = rh.OLKEntries[olkIdx];

            int offset = rh.OLKEntries[olkIdx].Entries[f.FileIndex].Offset + rh.OLKEntries[olkIdx].RootEntry.Offset + rh.OLKEntries[olkIdx].Offset;
            int size = rh.OLKEntries[olkIdx].Entries[f.FileIndex].Size;
            int sizeAligned = 0;
            if (size != 0) sizeAligned = Helper.AlignInt(size, 0x800);

            int newSize = newFile.Length;
            int newSizeAligned = Helper.AlignInt(newSize, 0x800);

            int sizeDif = newSizeAligned - sizeAligned;

            Patcher.WriteString(Patcher.statusTextBox, String.Format(
                "Replacing file 0x{0:X4} in olk {2} (0x{1:X8}) with {3}",
                f.FileIndex, offset, olkIdx, f.FilePath)
                );


            //if (sizeAligned == newSizeAligned) // write new data over old if padded size is same
            if (sizeDif == 0)
            {
                fs.Seek(offset, SeekOrigin.Begin);
                bw.Write(newFile);
            }
            else
            {
                BinaryReader br = new BinaryReader(fs);

                // read last part of olk
                int dataEnd = (int)fs.Length;
                int dataStart = offset + sizeAligned;
                int dataSize = dataEnd - dataStart;

                fs.Seek(dataStart, SeekOrigin.Begin);
                byte[] data = br.ReadBytes(dataSize);

                // write file over old
                fs.Seek(offset, SeekOrigin.Begin);
                bw.Write(newFile);

                // pad bytes
                int padSize = newSizeAligned - newSize;
                Helper.PadBytes(bw, padSize);

                // write rest of olk file
                bw.Write(data);
                data = new byte[16];

                // fix offsets after replaced file
                //FixEntryOffsets(fs, bw, rh.OLKEntries[olkIdx], sizeDif, f.FileIndex);
                for (int i = f.FileIndex + 1; i < rh.OLKEntries[olkIdx].Count; i++)
                {
                    int o = rh.OLKEntries[olkIdx].Entries[i].Offset += sizeDif;
                    int eo = rh.OLKEntries[olkIdx].Offset + 0x20 + (i * 0x10);
                    fs.Seek(eo, SeekOrigin.Begin);
                    bw.Write(o);
                }


                // fix olk size
                int newOlkSize = rh.OLKEntries[olkIdx].RootEntry.Size += sizeDif;
                fs.Seek(rh.OLKEntries[olkIdx].Offset + 0x14, SeekOrigin.Begin);
                bw.Write(newOlkSize);

                // fix root olk entry size/offsets
                int newOlkSize2 = rh.Entries[olkIdx].Size += sizeDif;
                fs.Seek(0x20 + (olkIdx * 0x10) + 4, SeekOrigin.Begin);
                bw.Write(newOlkSize2);

                for (int i = olkIdx +1; i < rh.Count; i++)
                {
                    int o = rh.Entries[i].Offset += sizeDif;
                    fs.Seek(0x20 + (i * 0x10), SeekOrigin.Begin);
                    bw.Write(o);
                }

                // fix root size
                int s = rh.RootEntry.Size += sizeDif;
                fs.Seek(0x14, SeekOrigin.Begin);
                bw.Write(s);

                // trim
                s += rh.RootEntry.Offset;
                fs.SetLength(s);

                //Patcher.WriteString(Patcher.statusTextBox, String.Format("0x{0:X8} = {1:X8} - {2:X8} = {3:X8}", newOlkSize2, newSizeAligned, sizeAligned, sizeDif));
            }

            // fix replaced file size
            int entryOffset = rh.OLKEntries[olkIdx].Offset + 0x20 + (f.FileIndex * 0x10);
            fs.Seek(entryOffset + 4, SeekOrigin.Begin);
            bw.Write(newSize);
            //Patcher.WriteString(Patcher.statusTextBox, String.Format("{1:X8} - {2:X8} = {3:X8}", offset, newSizeAligned, sizeAligned, sizeDif));
        }

        public static void ReplaceFiles()
        {
            int olkIdx = 0;

            bool bReplaced = false;

            Patcher.WriteString(Patcher.statusTextBox, String.Format("Patching {0}", Patcher.olkPath));

            foreach (string folder in folders)
            {
                string folderPath = Path.Combine(Patcher.modPath, folder);
                string flistPath = Path.Combine(folderPath, "flist.txt");
                if (File.Exists(flistPath))
                {
                    Patcher.WriteString(Patcher.statusTextBox, String.Format("Reading {0}", flistPath));

                    // get flist entries
                    List<FlistEntry> flistFiles = FlistGetEntries(flistPath, folderPath);

                    // get olk header info
                    
                    Header rh = new Header();

                    using (FileStream fs = new FileStream(Patcher.olkPath, FileMode.Open))
                    {
                        using (BinaryReader br = new BinaryReader(fs))
                        {
                            rh = GetEntries(fs, br);
                        }
                    }

                    // Replace file
                    using (FileStream fs = new FileStream(Patcher.olkPath, FileMode.Open))
                    {
                        using (BinaryWriter bw = new BinaryWriter(fs))
                        {
                            foreach (FlistEntry e in flistFiles)
                            {
                                byte[] newFile = new byte[16];
                                string fp = Path.GetFullPath(e.FilePath);
                                // Load file to replace with
                                if (File.Exists(fp))
                                {
                                    newFile = LoadFile(e.FilePath);
                                } else
                                {
                                    Patcher.WriteString(Patcher.statusTextBox, String.Format("Couldn't find {0}, skipping...", fp));
                                }

                                // replace file
                                ReplaceFile(fs, bw, e, ref rh, olkIdx, newFile);

                                bReplaced = true;
                                GC.Collect();
                            }
                        }
                    }
                }

                olkIdx += 1;
            }

            GC.Collect();
            if (bReplaced)
            {
                Patcher.WriteString(Patcher.statusTextBox, String.Format("Files replaced!"));
            } else
            {
                Patcher.WriteString(Patcher.statusTextBox, String.Format("No files found!"));
            }
        }
    }
}
