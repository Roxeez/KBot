using System.Collections.Generic;
using System.Linq;
using KBot.CLI.Files;
using KBot.CLI.Openers;
using KBot.Common.Logging;

namespace KBot.CLI.Processor
{
    public abstract class FileProcessor
    {
        public abstract string Path { get; }

        public void Process()
        {
            Log.Information($"Decrypting {Path}");
            
            IEnumerable<TextFile> files = TextOpener.Open(Path);
            if (!files.Any())
            {
                return;
            }
            
            Log.Information($"Processing {Path}");
            Process(files);
        }
        
        protected abstract void Process(IEnumerable<TextFile> files);
    }
}