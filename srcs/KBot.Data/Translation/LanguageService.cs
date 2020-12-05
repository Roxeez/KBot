using System;
using System.Collections.Generic;
using KBot.Common;
using KBot.Common.Extension;

namespace KBot.Data.Translation
{
    public class LanguageService
    {
        private readonly FileManager fileManager;
        private Dictionary<TranslationCategory, Dictionary<string, string>> translations;
        
        public const string LanguagePath = "db/languages";

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

        public void Load(Language language)
        {
            translations = fileManager.Load<Dictionary<TranslationCategory, Dictionary<string, string>>>($"{LanguagePath}/{language.ToString().ToUpper()}.json");
        }

        public bool CanBeLoaded(Language language)
        {
            return fileManager.HasFile($"{LanguagePath}/{language.ToString().ToUpper()}.json");
        }
    }
}