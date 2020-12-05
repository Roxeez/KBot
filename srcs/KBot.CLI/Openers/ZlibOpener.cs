using System;
using System.Collections.Generic;
using System.IO;
using Ionic.Zlib;
using KBot.CLI.Files;

namespace KBot.CLI.Openers
{
    public class ZlibOpener
    {
        public static IEnumerable<ZlibFile> Open(string path)
        {
            var files = new List<ZlibFile>();
            using (var reader = new BinaryReader(File.OpenRead(path)))
            {
                byte[] header = reader.ReadBytes(0x10);
                int count = reader.ReadInt32();

                byte[] separator = reader.ReadBytes(1);
                for (int i = 0; i != count; i++)
                {
                    int id = reader.ReadInt32();
                    int offset = reader.ReadInt32();

                    long previous = reader.BaseStream.Position;
                    reader.BaseStream.Seek(offset, SeekOrigin.Begin);

                    int creation = reader.ReadInt32();
                    int size = reader.ReadInt32();
                    int compressedSize = reader.ReadInt32();
                    bool compressed = Convert.ToBoolean(reader.ReadBytes(1)[0]);
                    byte[] data = reader.ReadBytes(compressedSize);

                    if (compressed)
                    {
                        data = ZlibStream.UncompressBuffer(data);
                    }
                    
                    files.Add(new ZlibFile
                    {
                        Id = id,
                        Content = data
                    });
                    
                    reader.BaseStream.Seek(previous, SeekOrigin.Begin);
                }
            }

            return files;
        }
    }
}