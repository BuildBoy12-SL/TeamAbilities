// -----------------------------------------------------------------------
// <copyright file="CommandConfigs.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Configs
{
    using RemoteAdmin;
    using TeamAbilities.Commands;

    public class CommandConfigs
    {
        public SupplyDropCommand SupplyDrop { get; set; } = new SupplyDropCommand();

        public TeslaToggleCommand TeslaToggle { get; set; } = new TeslaToggleCommand();

        public void RegisterCommands()
        {
            QueryProcessor.DotCommandHandler.RegisterCommand(SupplyDrop);
            QueryProcessor.DotCommandHandler.RegisterCommand(TeslaToggle);
        }
    }
}