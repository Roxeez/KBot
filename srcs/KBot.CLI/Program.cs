using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using Ionic.Zlib;
using KBot.CLI.Processor;
using KBot.CLI.Processor.Text;
using KBot.CLI.Processor.Zlib;
using KBot.Common;
using KBot.Common.Logging;
using KBot.Data.Translation;

namespace KBot.CLI
{
    public static class Program
    {
        private const string Header = @" _   ________       _         _____  _     _____ 
| | / /| ___ \     | |       /  __ \| |   |_   _|
| |/ / | |_/ / ___ | |_      | /  \/| |     | |  
|    \ | ___ \/ _ \| __|     | |    | |     | |  
| |\  \| |_/ / (_) | |_   _  | \__/\| |_____| |_ 
\_| \_/\____/ \___/ \__| (_)  \____/\_____/\___/ 
                                                 ";
        private static readonly string Separator = new string('=', Console.WindowHeight > 0 ? Console.WindowWidth - 1 : 20);
        
        private static readonly FileManager FileManager = new FileManager();
        private static readonly List<IFileProcessor> Processors = new List<IFileProcessor>
        {
            new GtdProcessor(FileManager),
            new TcProcessor(FileManager),
        };

        static Program()
        {
            IEnumerable<Language> languages = Enum.GetValues(typeof(Language)).Cast<Language>();
            foreach (Language language in languages)
            {
                Processors.Add(new LangProcessor(FileManager, language));
            }
        }
        
        public static void Main(string[] args)
        {
            Log.Logger = new Logger("KBot.CLI");
            
            AppDomain.CurrentDomain.UnhandledException += (obj, e) =>
            {
                Log.Error("Unhandled exception", (Exception)e.ExceptionObject);
            };
            
            Console.Title = "KBot.CLI";
            PrintHeader("KBot - Command Line Interface");

            string parameters = string.Join(" ", args);
            if (!File.Exists("NostaleClientX.exe"))
            {
                Log.Error("This application need to be executed into your NosTale folder");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(Separator);
                Console.ResetColor();
                Console.ReadKey();
                return;
            }

            string version = "0.0.0.0";
            if (FileManager.HasFile("db/version"))
            {
                version = FileManager.Load<string>("db/version");
            }
            
            FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo("NostaleClientX.exe");
            if (versionInfo.FileVersion == version && !parameters.Contains("--force"))
            {
                Log.Information($"Database is already up to date ({version})");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(Separator);
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            
            Log.Information($"Updating database from {version} to {versionInfo.FileVersion}");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(Separator);
            Console.ResetColor();

            FileManager.Delete("db");
            
            foreach (IFileProcessor processor in Processors)
            {
                Log.Information($"Decrypting {processor.Path}");
                
                processor.Process();
                
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine(Separator);
                Console.ResetColor();
            }
            
            FileManager.Save(versionInfo.FileVersion, "db/version");

            Log.Information($"Database successfully updated to {versionInfo.FileVersion}");
            
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(Separator);
            Console.ResetColor();

            Console.ReadKey();
        }
        
        private static void PrintHeader(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(Separator);
            Console.ForegroundColor = ConsoleColor.Cyan;
   
            foreach (string s in Header.Split('\n'))
            {
                Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0," + (Console.WindowWidth / 2 + s.Length / 2) + "}", s));
            }
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(Separator);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(string.Format(CultureInfo.CurrentCulture, "{0," + (Console.WindowWidth / 2 + text.Length / 2) + "}", text));
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(Separator);
            
            Console.ResetColor();
        }
    }
}