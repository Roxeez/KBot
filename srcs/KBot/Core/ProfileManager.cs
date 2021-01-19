using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using KBot.Common;
using KBot.Core.Configuration;

namespace KBot.Core
{
    public class ProfileManager
    {
        private readonly FileManager fileManager;

        private const string directory = "profiles";
        
        public ProfileManager(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        public IEnumerable<string> GetProfiles()
        {
            if (!fileManager.HasDirectory(directory))
            {
                return Array.Empty<string>();
            }
            
            return fileManager.GetFiles(directory).Select(Path.GetFileName);
        }

        public Profile LoadProfile(string name)
        {
            return fileManager.Load<Profile>($"{directory}/{name}");
        }

        public void SaveProfile(string name, Profile profile)
        {
            fileManager.Save(profile, $"{directory}/{name}");
        }
    }
}