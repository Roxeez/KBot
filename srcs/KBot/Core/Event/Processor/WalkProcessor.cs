using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KBot.Common.Extension;
using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;

namespace KBot.Core.Event.Processor
{
    public class WalkProcessor : EventProcessor<WalkEvent>
    {
        private readonly Bot bot;
        private static readonly Pen Pen = new Pen(Color.DarkBlue, 0.1f);
        
        public WalkProcessor(Bot bot)
        {
            this.bot = bot;
        }

        protected override void Process(GameSession session, WalkEvent e)
        {
            if (!bot.IsRecordingPath)
            {
                return;
            }
            
            using (var graphics = Graphics.FromImage(session.Character.Map.Preview))
            {
                if (bot.Path.Count != 0)
                {
                    Position last = bot.Path.Last();
                    graphics.DrawLine(Pen, last.X, last.Y, e.To.X, e.To.Y);
                    graphics.FillRectangle(Brushes.DarkRed, last.X, last.Y, 1, 1);
                }
                
                graphics.FillRectangle(Brushes.DarkRed, e.To.X, e.To.Y, 1, 1);
            }
            
            bot.MapPreview = session.Character.Map.Preview.ToBitmapSource();

            bot.Path.Add(e.To);
        }
    }
}
