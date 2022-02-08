// -----------------------------------------------------------------------
// <copyright file="RoomState.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.FacilityGlitches
{
    using System.Collections.Generic;
    using Exiled.API.Features;

    /// <summary>
    /// Contains the state of the doors in the room from when the state is first created.
    /// </summary>
    public readonly struct RoomState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoomState"/> struct.
        /// </summary>
        /// <param name="room">The room to generate the state from.</param>
        public RoomState(Room room)
        {
            DoorStates = new List<DoorState>();
            foreach (Door door in room.Doors)
                DoorStates.Add(new DoorState(door));
        }

        /// <summary>
        /// Gets a list of door states based off of the doors within the room.
        /// </summary>
        public List<DoorState> DoorStates { get; }

        /// <summary>
        /// Represents the state of a door when the state is first created.
        /// </summary>
        public readonly struct DoorState
        {
            private readonly Door door;
            private readonly bool isOpen;

            /// <summary>
            /// Initializes a new instance of the <see cref="DoorState"/> struct.
            /// </summary>
            /// <param name="door">The door to generate the state from.</param>
            public DoorState(Door door)
            {
                this.door = door;
                isOpen = door.IsOpen;
            }

            /// <summary>
            /// Restores the door's open state to match when it was created.
            /// </summary>
            public void Restore()
            {
                door.IsOpen = isOpen;
            }
        }
    }
}