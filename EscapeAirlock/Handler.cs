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
        DoorNametagExtension ESCPAE_PRIMARY;
        DoorNametagExtension ESCPAE_SECONDARY;

        bool AllowInteraction = true;

        public void OnWaitingForPlayers()
        {
            ESCPAE_PRIMARY = DoorNametagExtension.NamedDoors["ESCAPE_PRIMARY"];
            ESCPAE_SECONDARY = DoorNametagExtension.NamedDoors["ESCAPE_SECONDARY"];

            ESCPAE_SECONDARY.TargetDoor.NetworkTargetState = true;
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

            if(doorInteractable == "ESCAPE_PRIMARY")
            {
                ESCPAE_SECONDARY.TargetDoor.NetworkTargetState = doorStatus;
            }

            else

            if(doorInteractable == "ESCAPE_SECONDARY")
            {
                ESCPAE_PRIMARY.TargetDoor.NetworkTargetState = doorStatus;
            }

            Timing.CallDelayed(1.5f, () => AllowInteraction = true);
        }
    }
}
