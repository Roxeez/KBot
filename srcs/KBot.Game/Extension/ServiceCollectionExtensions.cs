using KBot.Game.Entities;
using KBot.Game.Inventories;
using KBot.Game.Maps;
using KBot.Game.Battle;
using Microsoft.Extensions.DependencyInjection;

namespace KBot.Game.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFactories(this IServiceCollection services)
        {
            return services.AddSingleton<MapFactory>()
                .AddSingleton<EntityFactory>()
                .AddSingleton<SkillFactory>()
                .AddSingleton<ItemFactory>()
                .AddSingleton<BuffFactory>();
        }
    }
}