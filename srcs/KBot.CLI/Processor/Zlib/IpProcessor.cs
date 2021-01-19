using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using KBot.CLI.Files;
using KBot.Common;
using KBot.Common.Logging;
using KBot.Data;

namespace KBot.CLI.Processor.Zlib
{
    public class IpProcessor : ZlibFileProcessor
    {
        public override string Path { get; } = "NostaleData/NSipData.NOS";

        private readonly FileManager fileManager;

        public IpProcessor(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }
        
        protected override void Process(IEnumerable<ZlibFile> files)
        {
            const int startIndex = 13;
            
            Log.Information("Generating icons");
            foreach (ZlibFile file in files)
            {
                byte[] content = file.Content;

                int width = BitConverter.ToInt16(content.Skip(1).Take(2).ToArray(), 0);
                int height = BitConverter.ToInt16(content.Skip(3).Take(2).ToArray(), 0);

                using (var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                {
                    bitmap.MakeTransparent();
                
                    for (int y = 0; y < height; y++)
                    {
                        for(int x = 0; x < width; x++)
                        {
                            byte gb = content[startIndex + y * 2 * width + x * 2];
                            byte ar = content[startIndex + y * 2 * width + x * 2 + 1];
                            int g = gb >> 4;
                            int b = gb & 0xF;
                            int a = ar >> 4;
                            int r = ar & 0xF;
                        
                            bitmap.SetPixel(x, y, Color.FromArgb(a * 0x11, r * 0x11, g * 0x11, b * 0x11));
                        }
                    }
                    
                    fileManager.SaveBitmap($"{Database.IconPath}/{file.Id}", bitmap);
                }
            }
            
            Log.Information($"Saved {files.Count()} icons into {Database.IconPath} folder");
        }
    }
}