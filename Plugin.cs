using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Enums;
using Exiled.API.Features;
using HarmonyLib;
using EPlayer = Exiled.Events.Handlers.Player;
using EServer = Exiled.Events.Handlers.Server;

namespace BetterHints
{
    public class Plugin : Plugin<Config>
    {
        public static Plugin Instance;
        public override PluginPriority Priority { get; } = PluginPriority.First;
        public override string Name { get; } = "BetterHints";
        public override string Author { get; } = "Steven4547466";
        public override Version Version { get; } = new Version(1, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(2, 1, 19);
        public override string Prefix { get; } = "BetterHints";

        int harmonyPatches = 0;
        private Harmony HarmonyInstance { get; set; }

        public Handlers.Player player { get; set; }
        public Handlers.Server server { get; set; }

        public override void OnEnabled()
        {
            base.OnEnabled();
            Instance = this;
            RegisterEvents();
            HarmonyInstance = new Harmony($"steven4547466.betterhints-{++harmonyPatches}");
            HarmonyInstance.PatchAll();
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
            UnregisterEvents();
            HarmonyInstance.UnpatchAll();
            Instance = null;
        }

        public void RegisterEvents() 
        {
            player = new Handlers.Player();
            server = new Handlers.Server();
            EPlayer.Joined += player.OnJoined;
            EPlayer.Left += player.OnLeft;
            EPlayer.Died += player.OnDied;

            EServer.RestartingRound += server.OnRestartingRound;
            //EServer.SendingConsoleCommand += server.OnConsoleCommand;
        }

        public void UnregisterEvents()
        {
            EPlayer.Joined -= player.OnJoined;
            EPlayer.Left -= player.OnLeft;
            EPlayer.Died -= player.OnDied;

            EServer.RestartingRound -= server.OnRestartingRound;
            //EServer.SendingConsoleCommand -= server.OnConsoleCommand;

            player = null;
            server = null;
        }
    }
}
