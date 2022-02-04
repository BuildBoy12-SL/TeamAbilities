// -----------------------------------------------------------------------
// <copyright file="SentKeypressEventArgs.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Events.EventArgs
{
    using Exiled.API.Features;
    using TeamAbilities.API;

    /// <summary>
    /// Contains all information after a player sends a key.
    /// </summary>
    public class SentKeypressEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SentKeypressEventArgs"/> class.
        /// </summary>
        /// <param name="player"><inheritdoc cref="Player"/></param>
        /// <param name="key"><inheritdoc cref="Key"/></param>
        public SentKeypressEventArgs(Player player, DetectableKey key)
        {
            Player = player;
            Key = key;
        }

        /// <summary>
        /// Gets the player that pressed the key.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Gets the key that was pressed.
        /// </summary>
        public DetectableKey Key { get; }
    }
}