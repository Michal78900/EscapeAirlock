using System;
using System.Linq;
using System.Text;
using Exiled.API.Features;
using Exiled.Events.EventArgs;
using System.Collections.Generic;
using MEC;
using UnityEngine;

using Interactables.Interobjects.DoorUtils;

namespace EscapeAirlock
{
    class Handler
    {
        DoorVariant ESCPAE_PRIMARY;
        DoorVariant ESCPAE_SECONDARY;

        bool AllowInteraction = true;

        public void OnWaitingForPlayers()
        {
            ESCPAE_PRIMARY = DoorNametagExtension.NamedDoors["ESCAPE_PRIMARY"].TargetDoor;
            ESCPAE_SECONDARY = DoorNametagExtension.NamedDoors["ESCAPE_SECONDARY"].TargetDoor;

            ESCPAE_SECONDARY.NetworkTargetState = true;
        }


        public void OnDoor(InteractingDoorEventArgs ev)
        {
            if (ev.Door.GetComponent<DoorNametagExtension>() == false) return;

            var doorname = ev.Door.GetComponent<DoorNametagExtension>().GetName;

            if (doorname == "ESCAPE_PRIMARY" || doorname == "ESCAPE_SECONDARY" && AllowInteraction)
            {
                Airlock(doorname, ev.Door.TargetState);
            }
        }

        public void OnDoor079(InteractingDoorEventArgs ev)
        {
            if (ev.Door.GetComponent<DoorNametagExtension>() == false) return;

            var doorname = ev.Door.GetComponent<DoorNametagExtension>().GetName;

            if (doorname == "ESCAPE_PRIMARY" || doorname == "ESCAPE_SECONDARY" && AllowInteraction)
            {
                Airlock(doorname, ev.Door.TargetState);
            }
        }

        public void Airlock(string doorInteractable, bool doorStatus)
        {
            AllowInteraction = false;

            if(doorInteractable == "ESCAPE_PRIMARY" && ESCPAE_PRIMARY.ActiveLocks == 0 && ESCPAE_SECONDARY.ActiveLocks == 0 && !IsDoorDestroyed(ESCPAE_PRIMARY))
            {
                ESCPAE_SECONDARY.NetworkTargetState = doorStatus;
            }

            else

            if(doorInteractable == "ESCAPE_SECONDARY" && ESCPAE_PRIMARY.ActiveLocks == 0 && ESCPAE_SECONDARY.ActiveLocks == 0 && !IsDoorDestroyed(ESCPAE_SECONDARY))
            {
                ESCPAE_PRIMARY.NetworkTargetState = doorStatus;
            }

            Timing.CallDelayed(1.75f, () => AllowInteraction = true);
        }

        public bool IsDoorDestroyed(DoorVariant door)
        {
            if (door is IDamageableDoor damage)
            {
                if (damage.IsDestroyed)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
