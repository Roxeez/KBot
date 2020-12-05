using System;
using System.Collections.Generic;
using KBot.Common;
using KBot.Common.Extension;

namespace KBot.Data.Language
{
    public class LanguageService
    {
        private readonly FileManager fileManager;
        private Dictionary<TranslationCategory, Dictionary<string, string>> translations;
        
        public LanguageService(FileManager fileManager)
        {
            this.fileManager = fileManager;
        }
        
        public string GetTranslation(TranslationCategory category, string key)
        {
            if (translations == null)
            {
                throw new InvalidOperationException("Language service is not correctly loaded");
            }
            
            return translations.GetValue(category)?.GetValue(key) ?? "UNDEFINED_TRANSLATION";
        }

        public void Load()
        {
            translations = fileManager.Load<Dictionary<TranslationCategory, Dictionary<string, string>>>("db/languages/en.json");
        }
    }
}