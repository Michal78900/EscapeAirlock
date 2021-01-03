using System;
using Exiled.API.Enums;
using Exiled.API.Features;

using PlayerEvent = Exiled.Events.Handlers.Player;
using ServerEvent = Exiled.Events.Handlers.Server;
using Scp079Event = Exiled.Events.Handlers.Scp079;

namespace EscapeAirlock
{
    public class EscapeAirlock : Plugin<Config>
    {
        private static readonly Lazy<EscapeAirlock> LazyInstance = new Lazy<EscapeAirlock>(() => new EscapeAirlock());
        public static EscapeAirlock Instance => LazyInstance.Value;

        public override PluginPriority Priority => PluginPriority.Medium;

        public override string Author => "Michal78900";
        public override Version Version => new Version(1, 0, 0);

        private EscapeAirlock() { }

        private Handler handler;

        public override void OnEnabled()
        {
            base.OnEnabled();

            handler = new Handler();

            ServerEvent.RoundStarted += handler.OnRoundStart;
            Scp079Event.InteractingDoor += handler.OnDoor079;
            PlayerEvent.InteractingDoor += handler.OnDoor;
        }

        public override void OnDisabled()
        {
            base.OnDisabled();

            ServerEvent.RoundStarted -= handler.OnRoundStart;
            Scp079Event.InteractingDoor -= handler.OnDoor079;
            PlayerEvent.InteractingDoor -= handler.OnDoor;

            handler = null;
        }
    }
}
