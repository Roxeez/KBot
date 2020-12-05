using System.Collections.Generic;
using System.Linq;
using KBot.CLI.Files;
using KBot.CLI.Openers;
using KBot.Common.Logging;

namespace KBot.CLI.Processor
{
    public abstract class TextFileProcessor : IFileProcessor
    {
        public abstract string Path { get; }

        public void Process()
        {
            IEnumerable<TextFile> files = TextOpener.Open(Path);
            if (!files.Any())
            {
                return;
            }
            
            Process(files);
        }
        
        protected abstract void Process(IEnumerable<TextFile> files);
    }
}