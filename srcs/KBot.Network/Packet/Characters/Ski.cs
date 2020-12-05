using System;
using System.Collections.Generic;


namespace KBot.Network.Packet.Characters
{
    public class Ski : IPacket
    {
        public int[] Skills { get; set; }
    }

    public class SkiCreator : IPacketCreator
    {
        public string Header { get; } = "ski";
        
        public IPacket Create(string[] content)
        {
            var skills = new List<int>();
            
            foreach (string entry in content)
            {
                string[] skillId = entry.Split('|');
                if (skillId.Length > 0)
                {
                    skills.Add(Convert.ToInt32(skillId[0]));
                    continue;
                }
                
                skills.Add(Convert.ToInt32(entry));
            }
            
            return new Ski
            {
                Skills = skills.ToArray()
            };
        }
    }
}