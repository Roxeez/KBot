using KBot.Common.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace KBot.Event.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventPipeline(this IServiceCollection services)
        {
            return services.AddSingleton<EventPipeline>()
                .AddImplementingTypes<IEventProcessor>();
        }
    }
}