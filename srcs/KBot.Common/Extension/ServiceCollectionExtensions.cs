using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using KBot.Common.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace KBot.Common.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddImplementingTypes<T>(this IServiceCollection services)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> types = assembly.GetTypes()
                    .Where(x => !x.IsInterface)
                    .Where(x => !x.IsAbstract)
                    .Where(x => typeof(T).IsAssignableFrom(x));

                foreach (Type type in types)
                {
                    Log.Debug($"Adding {type.Name} as {typeof(T).Name}");
                    services.AddTransient(typeof(T), type);
                }
            }

            return services;
        }

        public static IServiceCollection AddAllAsSingleton<T>(this IServiceCollection services)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> types = assembly.GetTypes()
                    .Where(x => !x.IsInterface)
                    .Where(x => !x.IsAbstract)
                    .Where(x => typeof(T).IsAssignableFrom(x));

                foreach (Type type in types)
                {
                    services.AddSingleton(type, type);
                }
            }

            return services;
        }
        
        public static IServiceCollection AddFileManager(this IServiceCollection services)
        {
            return services.AddSingleton<FileManager>();
        }
    }
}