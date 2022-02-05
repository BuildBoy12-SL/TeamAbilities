// -----------------------------------------------------------------------
// <copyright file="SupplyDropCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;

    public class SupplyDropCommand : ICommand
    {
        /// <inheritdoc />
        public string Command => "supplydrop";

        /// <inheritdoc />
        public string[] Aliases { get; } = Array.Empty<string>();

        /// <inheritdoc />
        public string Description => "Summons a supply drop.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "This command must be executed from the game level.";
                return false;
            }

            return Plugin.Instance.Config.AbilityConfigs.SupplyDrop.Execute(player, out response);
        }
    }
}