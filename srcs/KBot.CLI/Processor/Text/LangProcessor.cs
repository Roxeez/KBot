using System.Collections.Generic;
using System.Linq;
using System.Text;
using KBot.CLI.Files;
using KBot.CLI.Reader;
using KBot.Common;
using KBot.Common.Logging;
using KBot.Data.Translation;

namespace KBot.CLI.Processor.Text
{
    public class LangProcessor : TextFileProcessor
    {
        private readonly Language language;
        private readonly FileManager fileManager;
        
        public LangProcessor(FileManager fileManager, Language language)
        {
            this.language = language;
            this.fileManager = fileManager;
            
            Path = $"NostaleData/NSlangData_{language.ToString().ToUpper()}.NOS";
        }

        public override string Path { get; }
        
        protected override void Process(IEnumerable<TextFile> files)
        {
            TextFile monster = files.First(x => x.Name.Contains("monster"));
            TextFile item = files.First(x => x.Name.Contains("Item"));
            TextFile skill = files.First(x => x.Name.Contains("Skill"));
            TextFile map = files.First(x => x.Name.Contains("MapIDData"));
            TextFile card = files.First(x => x.Name.Contains("Card"));
            
            Log.Information("Generating monster translations");
            TextContent monsterContent = TextReader.FromString(Encoding.Default.GetString(monster.Content))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .TrimLines()
                .SplitLineContent('\t')
                .GetContent();
            
            var monsters = new Dictionary<string, string>();
            foreach (TextLine line in monsterContent.Lines)
            {
                monsters[line.GetFirstValue()] = line.GetLastValue();
            }
            
            Log.Information("Generating item translations");
            TextContent itemContent = TextReader.FromString(Encoding.Default.GetString(item.Content))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .TrimLines()
                .SplitLineContent('\t')
                .GetContent();
            
            var items = new Dictionary<string, string>();
            foreach (TextLine line in itemContent.Lines)
            {
                items[line.GetFirstValue()] = line.GetLastValue();
            }
            
            Log.Information("Generating skill translations");
            TextContent skillContent = TextReader.FromString(Encoding.Default.GetString(skill.Content))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .TrimLines()
                .SplitLineContent('\t')
                .GetContent();

            var skills = new Dictionary<string, string>();
            foreach (TextLine line in skillContent.Lines)
            {
                skills[line.GetFirstValue()] = line.GetLastValue();
            }
            
            Log.Information("Generating buff translations");
            TextContent cardContent = TextReader.FromString(Encoding.Default.GetString(card.Content))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .TrimLines()
                .SplitLineContent('\t')
                .GetContent();

            var buffs = new Dictionary<string, string>();
            foreach (TextLine line in cardContent.Lines)
            {
                buffs[line.GetFirstValue()] = line.GetLastValue();
            }
            
            Log.Information("Generating map translations");
            TextContent mapContent = TextReader.FromString(Encoding.Default.GetString(map.Content))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .TrimLines()
                .SplitLineContent('\t')
                .GetContent();

            var maps = new Dictionary<string, string>();
            foreach (TextLine line in mapContent.Lines)
            {
                maps[line.GetFirstValue()] = line.GetLastValue();
            }

            var translations = new Dictionary<TranslationCategory, Dictionary<string, string>>()
            {
                [TranslationCategory.Monster] = monsters,
                [TranslationCategory.Map] = maps,
                [TranslationCategory.Item] = items,
                [TranslationCategory.Skill] = skills,
                [TranslationCategory.Buff] = buffs
            };
            
            Log.Information($"Saving {translations.Sum(x => x.Value.Count)} translations into {LanguageService.LanguagePath}/{language.ToString()}.json");
            fileManager.Save(translations, $"{LanguageService.LanguagePath}/{language.ToString()}.json");
        }
    }
}