using System;
using System.Drawing;
using System.Windows.Media.Imaging;
using KBot.Common.Extension;
using KBot.Data;
using KBot.Data.Translation;
using KBot.Game.Enum;

namespace KBot.Game.Battle
{
    public class SkillFactory
    {
        private readonly Database database;
        private readonly LanguageService languageService;
        
        public SkillFactory(Database database, LanguageService languageService)
        {
            this.database = database;
            this.languageService = languageService;
        }

        public Skill CreateSkill(int skillId)
        {
            SkillData data = database.GetSkillData(skillId);
            
            string name = languageService.GetTranslation(TranslationCategory.Skill, data.NameKey);
            Bitmap icon = database.GetImage(ImageType.Icon, data.Icon);
            
            return new Skill
            {
                Id = skillId,
                CastId = data.CastId,
                Cooldown = data.Cooldown,
                CastTime = data.CastTime,
                MpCost = data.MpCost,
                LastUse = DateTime.MinValue,
                Name = name,
                ZoneRange = data.ZoneRange,
                Range = data.Range,
                Category = (SkillCategory)data.Category,
                Type = (SkillType)data.Type,
                HitType = (HitType)data.HitType,
                Target = (SkillTarget)data.Target,
                IsCombo = data.IsCombo,
                Icon = icon.ToBitmapSource()
            };
        }
    }
}