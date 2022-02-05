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

    /// <summary>
    /// All command configs.
    /// </summary>
    public class CommandConfigs
    {
        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="EmpCommand"/> command.
        /// </summary>
        public EmpCommand Emp { get; set; } = new EmpCommand();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="SupplyDropCommand"/> command.
        /// </summary>
        public SupplyDropCommand SupplyDrop { get; set; } = new SupplyDropCommand();

        /// <summary>
        /// Gets or sets a configurable instance of the <see cref="TeslaToggleCommand"/> command.
        /// </summary>
        public TeslaToggleCommand TeslaToggle { get; set; } = new TeslaToggleCommand();

        /// <summary>
        /// Registers all commands.
        /// </summary>
        public void RegisterCommands()
        {
            QueryProcessor.DotCommandHandler.RegisterCommand(Emp);
            QueryProcessor.DotCommandHandler.RegisterCommand(SupplyDrop);
            QueryProcessor.DotCommandHandler.RegisterCommand(TeslaToggle);
        }

        /// <summary>
        /// Registers all commands.
        /// </summary>
        public void UnregisterCommands()
        {
            QueryProcessor.DotCommandHandler.UnregisterCommand(Emp);
            QueryProcessor.DotCommandHandler.UnregisterCommand(SupplyDrop);
            QueryProcessor.DotCommandHandler.UnregisterCommand(TeslaToggle);
        }
    }
}