using System.Windows;
using KBot.Common;
using KBot.Common.Logging;
using KBot.Data;
using KBot.Data.Translation;
using KBot.Game;
using KBot.Game.Enum;
using KBot.Game.Extension;
using KBot.Network;
using KBot.Network.Packet;
using KBot.Window;
using KBot.Window.Context;

namespace KBot
{
    public partial class App
    {
        private readonly GameSession session;
        private readonly NetworkManager networkManager;
        private readonly PacketFactory packetFactory;
        private readonly LanguageService languageService;
        private readonly Database database;

        private const Language SelectedLanguage = Language.UK;

        public App(NetworkManager networkManager, PacketFactory packetFactory, LanguageService languageService, Database database)
        {
            session = new GameSession();
            
            this.networkManager = networkManager;
            this.packetFactory = packetFactory;
            this.languageService = languageService;
            this.database = database;
        }
        
        protected override void OnStartup(StartupEventArgs e)
        {
            Log.Information($"Starting application for session {session.Id}");

            if (!database.CanBeLoaded())
            {
                MessageBox.Show("Missing some database files, please execute KBot.CLI.exe");
                return;
            }

            if (!languageService.CanBeLoaded(SelectedLanguage))
            {
                MessageBox.Show("Missing some translation files, please execute KBot.CLI.exe");
                return;
            }
            
            Log.Debug($"Loading language service with language {SelectedLanguage}");
            languageService.Load(SelectedLanguage);
            
            Log.Debug("Loading game database");
            database.Load();
            
            session.PacketReceived += packet =>
            {
                IPacket typedPacket = packetFactory.CreateTypedPacket(packet);
                if (typedPacket == null)
                {
                    return;
                }
                
                networkManager.Process(session, typedPacket);
            };

            session.PacketSend += packet =>
            {
                IPacket typedPacket = packetFactory.CreateTypedPacket(packet);
                if (typedPacket == null)
                {
                    return;
                }

                networkManager.Process(session, typedPacket);
            };

            var window = new MainWindow
            {
                DataContext = new MainWindowContext(session)
            };

            Log.Debug("Showing main window");
            window.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.Debug("Stopping application gracefully");
            session.Dispose();
        }
    }
}