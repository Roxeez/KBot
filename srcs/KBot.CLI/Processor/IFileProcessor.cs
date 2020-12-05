namespace KBot.CLI.Processor
{
    public interface IFileProcessor
    {
        string Path { get; }
        void Process();
    }
}