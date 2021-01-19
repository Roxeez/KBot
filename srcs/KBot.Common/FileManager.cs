using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;

namespace KBot.Common
{
    public class FileManager
    {
        private readonly JsonSerializer serializer;
        private readonly string folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "KBot");

        public FileManager()
        {
            serializer = new JsonSerializer
            {
                Formatting = Formatting.Indented
            };
        }

        public bool HasFile(string name)
        {
            return File.Exists(Path.Combine(folder, name));
        }

        public bool HasDirectory(string name)
        {
            return Directory.Exists(Path.Combine(folder, name));
        }

        public IEnumerable<string> GetFiles(string path)
        {
            return Directory.GetFiles(Path.Combine(folder, path));
        }

        public void Delete(string name)
        {
            string path = Path.Combine(folder, name);
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
            
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public T Load<T>(string name) where T : class
        {
            if (!Directory.Exists(folder))
            {
                throw new InvalidOperationException($"Can't found file {name}");
            }

            string path = Path.Combine(folder, name);
            if (!File.Exists(path))
            {
                throw new InvalidOperationException($"Can't found file {name} in {folder}");
            }

            using (TextReader stream = new StreamReader(File.OpenRead(path)))
            {
                return serializer.Deserialize(stream, typeof(T)) as T;
            }
        }

        public Bitmap LoadBitmap(string name)
        {
            return new Bitmap(Path.Combine(folder, name));
        }

        public void SaveBitmap(string name, Bitmap bitmap)
        {
            string path = Path.Combine(folder, name);
            string parent = Directory.GetParent(path).FullName;
            
            if (!Directory.Exists(parent))
            {
                Directory.CreateDirectory(parent);
            }
            
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        
                bitmap.Save(path, ImageFormat.Png);
            }
        }

        public void Save<T>(T obj, string name)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            
            string path = Path.Combine(folder, name);
            string parent = Directory.GetParent(path).FullName;
            
            if (!Directory.Exists(parent))
            {
                Directory.CreateDirectory(parent);
            }
            
            using (TextWriter stream = new StreamWriter(File.Create(path)))
            {
                serializer.Serialize(stream, obj);
            }
        }
    }
}