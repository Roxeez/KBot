using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace KBot.Common.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddImplementingTypes<T>(this IServiceCollection services)
        {
            IEnumerable<Type> types = typeof(T).Assembly.GetTypes()
                .Where(x => !x.IsInterface)
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(T).IsAssignableFrom(x));

            foreach (Type type in types)
            {
                services.AddTransient(typeof(T), type);
            }

            return services;
        }
        public static IServiceCollection AddFileManager(this IServiceCollection services)
        {
            return services.AddSingleton<FileManager>();
        }
    }
}