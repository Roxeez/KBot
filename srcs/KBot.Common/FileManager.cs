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
        
        public T Load<T>(string name) where T : class
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
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
            using (TextWriter stream = new StreamWriter(File.Create(path)))
            {
                serializer.Serialize(stream, obj);
            }
        }
    }
}