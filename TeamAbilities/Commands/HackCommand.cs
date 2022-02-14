// -----------------------------------------------------------------------
// <copyright file="HackCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;
    using TeamAbilities.API;

    /// <inheritdoc />
    public class HackCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "hack";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = Array.Empty<string>();

        /// <inheritdoc />
        public string Description { get; set; } = "Attempts to execute another role's ability.";

        /// <summary>
        /// Gets or sets the response to give when the player does not specify an ability.
        /// </summary>
        public string AbilityUnspecified { get; set; } = "Specify the ability to hack.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "This command must be executed from the game level.";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = AbilityUnspecified;
                return false;
            }

            Ability ability = Ability.Get(arguments.At(0));
            return ability == null ? Plugin.Instance.Config.AbilityConfigs.Hack.Execute(player, out response) : ability.Execute(player, out response, true);
        }
    }
}