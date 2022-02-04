// -----------------------------------------------------------------------
// <copyright file="Player.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Events.Handlers
{
    using Exiled.Events;
    using Exiled.Events.Extensions;
    using TeamAbilities.Events.EventArgs;

    /// <summary>
    /// Player related events.
    /// </summary>
    public static class Player
    {
        /// <summary>
        /// Invoked after a player presses a detectable key.
        /// </summary>
        public static event Events.CustomEventHandler<SentKeypressEventArgs> SentKeypress;

        /// <summary>
        /// Called after a player presses a detectable key.
        /// </summary>
        /// <param name="ev">The <see cref="SentKeypressEventArgs"/> instance.</param>
        public static void OnSentKeypress(SentKeypressEventArgs ev) => SentKeypress.InvokeSafely(ev);
    }
}