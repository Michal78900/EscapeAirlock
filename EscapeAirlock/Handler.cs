using System;
using System.Linq;
using System.Text;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using MEC;
using UnityEngine;

using EPlayer = Exiled.API.Features.Player;


namespace EscapeAirlock
{
    class Handler
    {
        public List<Door> EscapeDoors = new List<Door>();
        bool isInteractionAllowed = true;

        public void OnRoundStart()
        {
            EscapeDoors.Clear();
            Timing.CallDelayed(0.2f, () =>
            {
                foreach (Door door in Map.Doors)
                {
                    if (door.DoorName == "ESCAPE" || door.DoorName == "ESCAPE_INNER")
                    {
                        EscapeDoors.Add(door);
                    }
                }
                EscapeDoors.Sort();
                EscapeDoors[1].NetworkisOpen = true;
            });
        }

        public void OnDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Door.DoorName == "ESCAPE" || ev.Door.DoorName == "ESCAPE_INNER")
            {
                if(!isInteractionAllowed)
                {
                    ev.IsAllowed = false;
                    return;
                }

                Airlock(ev.Door, ev.Door.isOpen);
            }
        }

        public void OnDoor079(InteractingDoorEventArgs ev)
        {
            if (ev.Door.DoorName == "ESCAPE" || ev.Door.DoorName == "ESCAPE_INNER")
            {
                if (!isInteractionAllowed)
                {
                    ev.IsAllowed = false;
                    return;
                }

                Airlock(ev.Door, ev.Door.isOpen);
            }
        }

        public void Airlock(Door doorInteracting, bool doorStatus)
        {
            // [0] ESCAPE
            // [1] ESCAPE_INNER

            if (isInteractionAllowed)
            {
                isInteractionAllowed = false;

                if (doorInteracting == EscapeDoors[0])
                {
                    EscapeDoors[1].NetworkisOpen = doorStatus;
                }

                else

                if (doorInteracting == EscapeDoors[1])
                {
                    EscapeDoors[0].NetworkisOpen = doorStatus;
                }
            }

            Timing.CallDelayed(1.5f, () => isInteractionAllowed = true);

        }
    }
}
