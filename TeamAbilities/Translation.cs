// -----------------------------------------------------------------------
// <copyright file="Translation.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities
{
    using Exiled.API.Interfaces;

    /// <inheritdoc />
    public class Translation : ITranslation
    {
        /// <summary>
        /// Gets or sets the response a player will receive when they attempt to use an ability belonging to another role.
        /// </summary>
        public string IncorrectRole { get; set; } = "You are not a role that can execute this ability.";

        /// <summary>
        /// gets or sets the response a player will receive when they attempt to use an ability while there is a cooldown in place.
        /// </summary>
        public string OnCooldown { get; set; } = "You must wait {duration} seconds until you can use this command.";
    }
}