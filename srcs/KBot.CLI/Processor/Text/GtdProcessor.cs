using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KBot.CLI;
using KBot.CLI.Files;
using KBot.CLI.Reader;
using KBot.Common;
using KBot.Common.Logging;
using KBot.Data;

namespace KBot.CLI.Processor
{
    public class GtdProcessor : TextFileProcessor
    {
        public override string Path { get; } = "NostaleData/NSgtdData.NOS";

        private readonly FileManager manager;
        
        public GtdProcessor(FileManager manager)
        {
            this.manager = manager;
        }

        protected override void Process(IEnumerable<TextFile> files)
        {
            TextFile monster = files.First(x => x.Name == "monster.dat");
            TextFile item = files.First(x => x.Name == "Item.dat");
            TextFile map = files.First(x => x.Name == "MapIDData.dat");
            
            Log.Information($"Generating {Database.MonsterPath}");
            TextContent monsterContent = TextReader.FromString(Encoding.Default.GetString(monster.Content))
                .TrimLines()
                .SkipCommentedLines("#")
                .SplitLineContent('\r')
                .SkipEmptyLines()
                .GetContent();
            
            Dictionary<int, MonsterData> monsters = new Dictionary<int, MonsterData>();
            IEnumerable<TextRegion> monsterRegions = monsterContent.GetRegions("VNUM");
            foreach (TextRegion region in monsterRegions)
            {
                int modelId = region.GetLine("VNUM").GetValue<int>(1);
                int level = region.GetLine("LEVEL").GetValue<int>(1);
                string nameKey = region.GetLine("NAME").GetValue(1);

                monsters[modelId] = new MonsterData
                {
                    NameKey = nameKey,
                    Level = level
                };
            }
            
            Log.Information($"Generating {Database.ItemPath}");
            TextContent itemContent = TextReader.FromString(Encoding.Default.GetString(item.Content))
                .TrimLines()
                .SkipCommentedLines("#")
                .SplitLineContent('\r')
                .SkipEmptyLines()
                .GetContent();
            
            Dictionary<int, ItemData> items = new Dictionary<int, ItemData>();
            IEnumerable<TextRegion> itemRegions = itemContent.GetRegions("VNUM");
            foreach (TextRegion region in itemRegions)
            {
                int gameKey = region.GetLine("VNUM").GetValue<int>(1);
                string nameKey = region.GetLine("NAME").GetValue(1);

                items[gameKey] = new ItemData
                {
                    NameKey = nameKey
                };
            }
            
            Log.Information($"Generating {Database.MapPath}");
            TextContent mapContent = TextReader.FromString(Encoding.Default.GetString(map.Content))
                .SkipLines(x => x.StartsWith("DATA"))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent(' ')
                .TrimLines()
                .GetContent();
            
            Dictionary<int, MapData> maps = new Dictionary<int, MapData>();
            foreach (TextLine line in mapContent.Lines)
            {
                int firstMapId = line.GetValue<int>(0);
                int secondMapId = line.GetValue<int>(1);
                string nameKey = line.GetValue(4);

                for (int i = firstMapId; i <= secondMapId; i++)
                {
                    maps[i] = new MapData
                    {
                        NameKey = nameKey,
                        Grid = Array.Empty<byte>()
                    };
                }
            }
            
            Log.Information($"Saving {Database.MonsterPath} with {monsters.Count} monsters");
            manager.Save(monsters, Database.MonsterPath);
            
            Log.Information($"Saving {Database.ItemPath} with {items.Count} items");
            manager.Save(items, Database.ItemPath);
            
            Log.Information($"Saving {Database.MapPath} with {maps.Count} maps");
            manager.Save(maps, Database.MapPath);
        }
    }
}