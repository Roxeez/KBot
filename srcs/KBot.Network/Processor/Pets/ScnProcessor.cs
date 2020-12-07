using KBot.Data;
using KBot.Data.Translation;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Pets;
using KBot.Network.Packet.Pets;

namespace KBot.Network.Processor.Pets
{
    public class ScnProcessor : PacketProcessor<Scn>
    {
        private readonly Database database;
        private readonly LanguageService languageService;

        public ScnProcessor(Database database, LanguageService languageService)
        {
            this.database = database;
            this.languageService = languageService;
        }

        protected override void Process(GameSession session, Scn packet)
        {
            Character character = session.Character;

            MonsterData data = database.GetMonsterData(packet.ModelId);
            string name = languageService.GetTranslation(TranslationCategory.Monster, data.NameKey);

            var partner = new OwnedPartner
            {
                Id = packet.PetId,
                EntityId = packet.EntityId,
                ModelId = packet.ModelId,
                Name = packet.Name == "-" || packet.Name == "@" ? name : packet.Name,
                IsSummonable = packet.IsSummonable,
                IsTeamMember = packet.IsTeamMember,
                Level = packet.Level,
                Loyalty = packet.Loyalty
            };
            
            character.Partners.Add(partner);

            if (packet.IsTeamMember)
            {
                LivingEntity entity = character.Map.GetEntity<LivingEntity>(EntityType.Npc, packet.EntityId);
                if (entity == null)
                {
                    return;
                }


                character.Partner = new Partner(partner, entity)
                {
                    Level = partner.Level, 
                    Name = partner.Name
                };
            }
        }
    }
}