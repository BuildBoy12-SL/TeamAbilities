// -----------------------------------------------------------------------
// <copyright file="ScanCommand.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Commands
{
    using System;
    using CommandSystem;
    using Exiled.API.Features;

    /// <inheritdoc />
    public class ScanCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "scan";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = Array.Empty<string>();

        /// <inheritdoc />
        public string Description { get; set; } = "Scans the facility for the roles of all personnel.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "This command must be executed from the game level.";
                return false;
            }

            return Plugin.Instance.Config.AbilityConfigs.Scan.Execute(player, out response);
        }
    }
}