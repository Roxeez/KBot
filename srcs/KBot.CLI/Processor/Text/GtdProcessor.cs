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

namespace KBot.CLI.Processor.Text
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
            TextFile skill = files.First(x => x.Name == "Skill.dat");
            TextFile map = files.First(x => x.Name == "MapIDData.dat");
            TextFile card = files.First(x => x.Name == "Card.dat");
            
            Log.Information("Generating monsters");
            TextContent monsterContent = TextReader.FromString(Encoding.Default.GetString(monster.Content))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent('\t')
                .TrimLines()
                .GetContent();
            
            var monsters = new Dictionary<int, MonsterData>();
            IEnumerable<TextRegion> monsterRegions = monsterContent.GetRegions("VNUM");
            foreach (TextRegion region in monsterRegions)
            {
                TextLine zSkillLine = region.GetLine("ZSKILL");

                int modelId = region.GetLine("VNUM").GetValue<int>(1);
                int level = region.GetLine("LEVEL").GetValue<int>(1);
                string nameKey = region.GetLine("NAME").GetValue(1);

                monsters[modelId] = new MonsterData
                {
                    NameKey = nameKey,
                    Level = level,
                    BasicAttackRange = zSkillLine.GetValue<int>(2),
                    BasicAttackCastTime = zSkillLine.GetValue<int>(4),
                    BasicAttackCooldown = zSkillLine.GetValue<int>(5),
                };
            }
            
            Log.Information("Generating items");
            TextContent itemContent = TextReader.FromString(Encoding.Default.GetString(item.Content))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent('\t')
                .TrimLines()
                .GetContent();
            
            var items = new Dictionary<int, ItemData>();
            IEnumerable<TextRegion> itemRegions = itemContent.GetRegions("VNUM");
            foreach (TextRegion region in itemRegions)
            {
                TextLine indexLine = region.GetLine("INDEX");
                
                int gameKey = region.GetLine("VNUM").GetValue<int>(1);
                string nameKey = region.GetLine("NAME").GetValue(1);
                
                items[gameKey] = new ItemData
                {
                    NameKey = nameKey,
                    Icon = indexLine.GetValue<int>(5),
                    InventoryType = indexLine.GetValue<int>(1),
                    Type = indexLine.GetValue<int>(2),
                    SubType = indexLine.GetValue<int>(3)
                };
            }
            
            Log.Information("Generating skills");
            TextContent skillContent = TextReader.FromString(Encoding.Default.GetString(skill.Content))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent('\t')
                .TrimLines()
                .GetContent();
            
            var skills = new Dictionary<int, SkillData>();
            IEnumerable<TextRegion> skillRegions = skillContent.GetRegions("VNUM");
            foreach (TextRegion region in skillRegions)
            {
                TextLine vnumLine = region.GetLine("VNUM");
                TextLine nameLine = region.GetLine("NAME");
                TextLine typeLine = region.GetLine("TYPE");
                TextLine dataLine = region.GetLine("DATA");
                TextLine targetLine = region.GetLine("TARGET");
                TextLine costLine = region.GetLine("COST");
                TextLine effectLine = region.GetLine("EFFECT");

                int id = vnumLine.GetValue<int>(1);

                skills[id] = new SkillData
                {
                    NameKey = nameLine.GetValue(1),
                    Category = typeLine.GetValue<int>(1),
                    CastId = typeLine.GetValue<int>(2),
                    CastTime = dataLine.GetValue<int>(5),
                    Cooldown = dataLine.GetValue<int>(6),
                    MpCost = dataLine.GetValue<int>(7),
                    Target = targetLine.GetValue<int>(1),
                    HitType = targetLine.GetValue<int>(2),
                    Range = targetLine.GetValue<short>(3),
                    ZoneRange = targetLine.GetValue<short>(4),
                    Type = targetLine.GetValue<int>(5),
                    IsCombo = costLine.GetValue<int>(3) == 999,
                    Icon = effectLine.GetValue<int>(1)
                };
            }
            
            Log.Information("Generating buffs");
            TextContent cardContent = TextReader.FromString(Encoding.Default.GetString(card.Content))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent('\t')
                .TrimLines()
                .GetContent();
            
            var buffs = new Dictionary<int, BuffData>();
            IEnumerable<TextRegion> cardRegions = cardContent.GetRegions("VNUM");
            foreach (TextRegion region in cardRegions)
            {
                TextLine vnumLine = region.GetLine("VNUM");
                TextLine nameLine = region.GetLine("NAME");
                TextLine groupLine = region.GetLine("GROUP");
                TextLine styleLine = region.GetLine("STYLE");

                int id = vnumLine.GetValue<int>(1);
                string nameKey = nameLine.GetValue(1);

                buffs[id] = new BuffData
                {
                    NameKey = nameKey,
                    GroupId = groupLine.GetValue<int>(1),
                    Level = groupLine.GetValue<int>(2),
                    Category = styleLine.GetValue<int>(1),
                    Effect = styleLine.GetValue<int>(2),
                };
            }
            
            Log.Information("Generating maps");
            TextContent mapContent = TextReader.FromString(Encoding.Default.GetString(map.Content))
                .SkipLines(x => x.StartsWith("DATA"))
                .SkipCommentedLines("#")
                .SkipEmptyLines()
                .SplitLineContent(' ')
                .TrimLines()
                .GetContent();
            
            var maps = new Dictionary<int, MapData>();
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
            
            Log.Information($"Saving {monsters.Count} monsters into {Database.MonsterPath}");
            manager.Save(monsters, Database.MonsterPath);
            
            Log.Information($"Saving {items.Count} items into {Database.ItemPath}");
            manager.Save(items, Database.ItemPath);
            
            Log.Information($"Saving {skills.Count} skills into {Database.SkillPath}");
            manager.Save(skills, Database.SkillPath);
            
            Log.Information($"Saving {buffs.Count} buffs into {Database.BuffPath}");
            manager.Save(buffs, Database.BuffPath);
            
            Log.Information($"Saving {maps.Count} maps into {Database.MapPath}");
            manager.Save(maps, Database.MapPath);
        }
    }
}