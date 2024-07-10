using Exiled.API.Features;
using Exiled.API.Enums;
using System;

namespace hours
{
    public class PlayerData
    {
        public string name { get; set; }
        public double PlayTime { get; set; }
    }

    public class hours : Plugin<Config>
    {
        public override PluginPriority Priority { get; } = PluginPriority.First;


        private static readonly hours Singleton = new hours();


        public bool can_execute = false;
        public bool can_play = false;
        private EventHandlers server;

        public static hours Instance => Singleton;


        private hours()
        {

        }


        public override void OnEnabled()
        {
            RegisterEvents();
            base.OnEnabled();
        }


        public override void OnDisabled()
        {
            UnregisterEvents();
            base.OnDisabled();
        }


        private void RegisterEvents()
        {
            server = new EventHandlers();
            Exiled.Events.Handlers.Server.RoundStarted += server.onStart;
            Exiled.Events.Handlers.Player.Spawning += server.onSpawned;
        }


        private void UnregisterEvents()
        {
            Exiled.Events.Handlers.Server.RoundStarted -= server.onStart;
            Exiled.Events.Handlers.Player.Spawning -= server.onSpawned;

            server = null;
        }
    }
}
