// -----------------------------------------------------------------------
// <copyright file="Translation.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities
{
    using Exiled.API.Interfaces;

    public class Translation : ITranslation
    {
        public string IncorrectRole { get; set; } = "You are not a role that can execute this ability.";

        public string OnCooldown { get; set; } = "You must wait {duration} seconds until you can use this command.";
    }
}