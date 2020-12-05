namespace KBot.CLI.Core.Files
{
    public class TextFile
    {
        public bool IsDat { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public byte[] Content { get; set; }
    }
}