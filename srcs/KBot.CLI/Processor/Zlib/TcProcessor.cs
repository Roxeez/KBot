using System.Collections.Generic;
using System.Linq;
using KBot.CLI.Files;
using KBot.Common;
using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Data;

namespace KBot.CLI.Processor.Zlib
{
    public class TcProcessor : ZlibFileProcessor
    {
        public override string Path { get; } = "NostaleData/NStcData.NOS";

        private readonly FileManager manager;
        
        public TcProcessor(FileManager manager)
        {
            this.manager = manager;
        }
        
        protected override void Process(IEnumerable<ZlibFile> files)
        {
            Log.Information($"Loading existing {Database.MapPath}");
            Dictionary<int, MapData> data = manager.Load<Dictionary<int, MapData>>(Database.MapPath);
            
            Log.Information("Generating map grids");
            Dictionary<int, byte[]> grids = files.ToDictionary(x => x.Id, x => x.Content);
            foreach(var pair in data)
            {
                byte[] grid = grids.GetValue(pair.Key);
                if (grid == null)
                {
                    grid = new byte[0];
                }

                pair.Value.Grid = grid;
            }
            
            Log.Information($"Saving {grids.Count} generated map grid into {Database.MapPath}");
            manager.Save(data, Database.MapPath);
        }
    }
}