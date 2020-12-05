using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using KBot.CLI.Core.Encryption;
using KBot.CLI.Core.Files;

namespace KBot.CLI.Core.Openers
{
    public static class TextOpener
    {
        public static IEnumerable<TextFile> Open(string path)
        {
            List<TextFile> files = new List<TextFile>();
            using (BinaryReader reader = new BinaryReader(File.OpenRead(path)))
            {
                int count = reader.ReadInt32();

                for (int i = 0; i < count; i++)
                {
                    int index = reader.ReadInt32();
                    int nameSize = reader.ReadInt32();
                    string name = Encoding.Default.GetString(reader.ReadBytes(nameSize));
                    bool dat = Convert.ToBoolean(reader.ReadInt32());
                    int fileSize = reader.ReadInt32();
                    byte[] content = reader.ReadBytes(fileSize);
                    
                    byte[] decrypted;
                    if (dat || name.EndsWith(".dat"))
                    {
                        decrypted = Dat.Decrypt(content);
                    }
                    else
                    {
                        decrypted = Lst.Decrypt(content);
                    }
                    
                    files.Add(new TextFile
                    {
                        Index = index,
                        IsDat = dat,
                        Name = name,
                        Content = decrypted
                    });
                }
            }
            
            return files;
        }
    }
}