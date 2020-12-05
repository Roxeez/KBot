using KBot.Common.Extension;
using KBot.Network.Packet;
using KBot.Network.Processor;
using Microsoft.Extensions.DependencyInjection;

namespace KBot.Network.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPacketFactory(this IServiceCollection services)
        {
            return services.AddImplementingTypes<IPacketCreator>()
                .AddSingleton<PacketFactory>();
        }

        public static IServiceCollection AddNetworkManager(this IServiceCollection services)
        {
            return services.AddImplementingTypes<IPacketProcessor>()
                .AddSingleton<NetworkManager>();
        }
    }
}