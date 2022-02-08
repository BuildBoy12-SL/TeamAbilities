// -----------------------------------------------------------------------
// <copyright file="FacilityGlitchHandler.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.FacilityGlitches
{
    using System.Collections.Generic;
    using Exiled.API.Features;
    using Interactables.Interobjects.DoorUtils;
    using MEC;
    using NorthwoodLib.Pools;

    /// <summary>
    /// Handles facility wide glitches.
    /// </summary>
    public class FacilityGlitchHandler
    {
        private readonly Plugin plugin;
        private CoroutineHandle glitchCoroutine;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacilityGlitchHandler"/> class.
        /// </summary>
        /// <param name="plugin">An instance of the <see cref="Plugin"/> class.</param>
        public FacilityGlitchHandler(Plugin plugin) => this.plugin = plugin;

        /// <summary>
        /// Starts the glitch sequence.
        /// </summary>
        public void Start()
        {
            glitchCoroutine = Timing.RunCoroutine(RunFacilityGlitches());
        }

        /// <summary>
        /// Stops the glitch sequence.
        /// </summary>
        public void Stop()
        {
            Timing.KillCoroutines(glitchCoroutine);
        }

        private IEnumerator<float> RunFacilityGlitches()
        {
            while (true)
            {
                if (!Round.IsStarted)
                {
                    yield return Timing.WaitForSeconds(1f);
                    continue;
                }

                yield return Timing.WaitForSeconds(plugin.Config.FacilityGlitches.Interval);
                if (!Round.IsStarted || Exiled.Loader.Loader.Random.Next(100) > plugin.Config.FacilityGlitches.GlitchChance)
                    continue;

                float glitchDuration = UnityEngine.Random.Range(plugin.Config.FacilityGlitches.MinimumGlitchDuration, plugin.Config.FacilityGlitches.MaximumGlitchDuration);
                List<(Room, RoomState)> roomPair = ListPool<(Room, RoomState)>.Shared.Rent(GetRooms());
                foreach ((Room room, _) in roomPair)
                {
                    room.TurnOffLights(glitchDuration);
                    bool isAdditionalRoom = plugin.Config.FacilityGlitches.AdditionalRooms.Contains(room.Type);
                    foreach (Door door in room.Doors)
                    {
                        if (isAdditionalRoom || door.RequiredPermissions.RequiredPermissions == KeycardPermissions.None)
                            door.IsOpen = !door.IsOpen;
                    }
                }

                yield return Timing.WaitForSeconds(glitchDuration);
                foreach ((_, RoomState roomState) in roomPair)
                {
                    foreach (RoomState.DoorState doorState in roomState.DoorStates)
                        doorState.Restore();
                }

                ListPool<(Room, RoomState)>.Shared.Return(roomPair);
            }
        }

        private IEnumerable<(Room, RoomState)> GetRooms()
        {
            foreach (Room room in Map.Rooms)
            {
                if (plugin.Config.FacilityGlitches.AdditionalRooms.Contains(room.Type) ||
                    HasValidDoor(room))
                {
                    yield return (room, new RoomState(room));
                }
            }
        }

        private bool HasValidDoor(Room room)
        {
            foreach (Door door in room.Doors)
            {
                if (door.RequiredPermissions.RequiredPermissions == KeycardPermissions.None)
                    return true;
            }

            return false;
        }
    }
}