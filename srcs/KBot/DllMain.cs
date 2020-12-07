using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using KBot.Common.Extension;
using KBot.Common.Logging;
using KBot.Data;
using KBot.Data.Extension;
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

        private static readonly LoggerBridge LoggerBridge;
        private static readonly LoggerCallback LoggerCallback;

        static DllMain()
        {
            LoggerCallback = str =>
            {
                Log.Debug($"[KBot.Interop] {str}");
            };
            LoggerBridge = new LoggerBridge();
        }

        [DllExport]
        public static void Main()
        {
            AllocConsole();

            Log.Logger = new Logger("KBot");

            LoggerBridge.SetCallback(LoggerCallback);

            Log.Debug("Initializing services");
            IServiceProvider services = new ServiceCollection()
                .AddPacketFactory()
                .AddNetworkManager()
                .AddFactories()
                .AddFileManager()
                .AddLanguageService()
                .AddDatabase()
                .AddTransient<App>()
                .BuildServiceProvider();

            AppDomain.CurrentDomain.UnhandledException += (obj, ex) =>
            {
                Log.Error("Unhandled exception", (Exception) ex.ExceptionObject);
            };
            
            Log.Debug("Starting app thread");
            var thread = new Thread(() =>
            {
                App app = services.GetRequiredService<App>();

                app.InitializeComponent();
                app.Run();
            });
            
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
    }
}