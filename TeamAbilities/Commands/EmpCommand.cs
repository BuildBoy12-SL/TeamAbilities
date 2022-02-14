﻿// -----------------------------------------------------------------------
// <copyright file="EmpCommand.cs" company="Build">
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
    public class EmpCommand : ICommand
    {
        /// <inheritdoc />
        public string Command { get; set; } = "emp";

        /// <inheritdoc />
        public string[] Aliases { get; set; } = Array.Empty<string>();

        /// <inheritdoc />
        public string Description { get; set; } = "Initiates a site-wide blackout.";

        /// <inheritdoc />
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "This command must be executed from the game level.";
                return false;
            }

            return Plugin.Instance.Config.AbilityConfigs.Emp.Execute(player, out response);
        }
    }
}