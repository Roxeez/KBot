using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using KBot.CLI.Files;
using KBot.Common;
using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Data;

namespace KBot.CLI.Processor.Zlib
{
    public class TcProcessor : ZlibFileProcessor
    {
        public override string Path { get; } = "NostaleData/NStcData.NOS";

        private readonly FileManager fileManager;
        
        private static readonly Dictionary<int, Color> Colors = new Dictionary<int, Color>
        {
            [0x0] = Color.FromArgb(255, 255, 255),
            [0x1] = Color.FromArgb(100, 100, 100),
            [0x2] = Color.FromArgb(100, 150, 100),
            [0x3] = Color.FromArgb(0, 0, 0),
            [0x9] = Color.FromArgb(100, 100, 150),
            [0x0a] = Color.FromArgb(0, 50, 200),
            [0x0b] = Color.FromArgb(150, 100, 100),
            [0x0d] = Color.FromArgb(150, 150, 100),
            [0x10] = Color.FromArgb(150, 100, 150),
            [0x11] = Color.FromArgb(0, 200, 50),
            [0x12] = Color.FromArgb(200, 50, 0),
            [0x13] = Color.FromArgb(250, 230, 10),
            [0xFF] = Color.FromArgb(150, 0, 255)
        };
        
        public TcProcessor(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }
        
        protected override void Process(IEnumerable<ZlibFile> files)
        {
            Log.Information($"Loading existing {Database.MapPath}");
            Dictionary<int, MapData> data = fileManager.Load<Dictionary<int, MapData>>(Database.MapPath);

            Log.Information("Generating map grids");
            var grids = files.ToDictionary(x => x.Id, x => x.Content);
            foreach(KeyValuePair<int, MapData> pair in data)
            {
                byte[] grid = grids.GetValue(pair.Key);
                if (grid == null)
                {
                    grid = new byte[0];
                }

                pair.Value.Grid = grid;
            }
            
            Log.Information("Generating maps preview");
            foreach (ZlibFile file in files)
            {
                byte[] content = file.Content;

                int width = BitConverter.ToInt16(content.Skip(0).Take(2).ToArray(), 0);
                int height = BitConverter.ToInt16(content.Skip(2).Take(2).ToArray(), 0);
                
                using (var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb))
                {
                    bitmap.MakeTransparent();
                
                    for (int y = 0; y < height; y++)
                    {
                        for(int x = 0; x < width; x++)
                        {
                            byte value = content[4 + y * width + x];
                            if (Colors.ContainsKey(value))
                            {
                                bitmap.SetPixel(x, y, Colors[value]);
                            }
                            else
                            {
                                bitmap.SetPixel(x, y, Colors[0xFF]);
                            }
                        }
                    }

                    fileManager.SaveBitmap($"{Database.MapPreviewPath}/{file.Id}", bitmap);
                }
            }
            
            Log.Information($"Saved {files.Count()} maps preview into {Database.MapPreviewPath}");
            
            Log.Information($"Saving {grids.Count} generated map grid into {Database.MapPath}");
            fileManager.Save(data, Database.MapPath);
        }
    }
}