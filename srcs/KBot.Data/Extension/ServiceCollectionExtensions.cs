using KBot.Data.Translation;
using Microsoft.Extensions.DependencyInjection;

namespace KBot.Data.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLanguageService(this IServiceCollection services)
        {
            return services.AddSingleton<LanguageService>();
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            return services.AddSingleton<Database>();
        }
    }
}