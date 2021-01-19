using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Context;
using KBot.Core;
using KBot.Data.Extension;
using KBot.Event;
using KBot.Event.Extension;
using KBot.Game;
using KBot.Game.Extension;
using KBot.Interop;
using KBot.Network.Extension;
using Microsoft.Extensions.DependencyInjection;

namespace KBot
{
    public static class DllMain
    {

        [DllImport("kernel32")]
        private static extern bool AllocConsole();

        [DllExport]
        public static void Main()
        {
            AllocConsole();
            
            Log.Logger = new Logger("KBot");

            Log.Debug("Initializing services");
            IServiceProvider services = new ServiceCollection()
                .AddPacketFactory()
                .AddNetworkManager()
                .AddFactories()
                .AddFileManager()
                .AddLanguageService()
                .AddDatabase()
                .AddEventPipeline()
                .AddSingleton<GameSession>()
                .AddSingleton<ProfileManager>()
                .AddSingleton<Bot>()
                .AddImplementingTypes<ITabContext>()
                .AddTransient<App>()
                .BuildServiceProvider();

            EventPipeline pipeline = services.GetRequiredService<EventPipeline>();
            IEnumerable<IEventProcessor> processors = services.GetServices<IEventProcessor>();

            foreach (IEventProcessor processor in processors)
            {
                pipeline.AddProcessor(processor);
            }
            
            Log.Debug("Starting app thread");
            var thread = new Thread(() =>
            {
                AppDomain.CurrentDomain.UnhandledException += (obj, ex) =>
                {
                    Log.Error("Unhandled exception", (Exception) ex.ExceptionObject);
                };

                App app = services.GetRequiredService<App>();

                app.InitializeComponent();
                app.Run();
            });
            
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}