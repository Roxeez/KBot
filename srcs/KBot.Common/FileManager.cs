using System;
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