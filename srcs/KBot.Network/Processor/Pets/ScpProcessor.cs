using System.Linq;
using KBot.Common.Logging;
using KBot.Data;
using KBot.Data.Translation;
using KBot.Event;
using KBot.Event.Characters;
using KBot.Game;
using KBot.Game.Entities;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Game.Pets;
using KBot.Network.Packet.Pets;

namespace KBot.Network.Processor.Pets
{
    public class ScpProcessor : PacketProcessor<Scp>
    {
        private readonly Database database;
        private readonly LanguageService languageService;
        private readonly EventPipeline eventPipeline;

        public ScpProcessor(Database database, LanguageService languageService, EventPipeline eventPipeline)
        {
            this.database = database;
            this.languageService = languageService;
            this.eventPipeline = eventPipeline;
        }

        protected override void Process(GameSession session, Scp packet)
        {
            Character character = session.Character;

            if (character.Pet != null && character.Pet.Id == packet.PetId)
            {
                if (packet.Loyalty != character.Pet.Loyalty)
                {
                    eventPipeline.Process(session, new PetLoyaltyChanged());
                }
               
                return;
            }
            
            MonsterData data = database.GetMonsterData(packet.ModelId);
            string name = languageService.GetTranslation(TranslationCategory.Monster, data.NameKey);
            
            var pet = new OwnedPet
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
            
            character.Pets.Add(pet);

            if (pet.IsTeamMember)
            {
                Npc entity = character.Map.GetEntity<Npc>(EntityType.Npc, pet.EntityId);
                if (entity == null)
                {
                    return;
                }
                
                character.Pet = new Pet(character, pet, entity);
            }
        }
    }
}