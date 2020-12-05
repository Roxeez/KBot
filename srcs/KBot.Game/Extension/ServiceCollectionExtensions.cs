using KBot.Game.Entities;
using KBot.Game.Maps;
using KBot.Game.Skills;
using Microsoft.Extensions.DependencyInjection;

namespace KBot.Game.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFactories(this IServiceCollection services)
        {
            return services.AddSingleton<MapFactory>()
                .AddSingleton<EntityFactory>()
                .AddSingleton<SkillFactory>();
        }
    }
}