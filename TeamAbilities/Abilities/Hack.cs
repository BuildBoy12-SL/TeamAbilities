// -----------------------------------------------------------------------
// <copyright file="Hack.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities.Abilities
{
    using System;
    using System.Collections.Generic;
    using CommandSystem;
    using Exiled.API.Features;
    using TeamAbilities.API;
    using YamlDotNet.Serialization;

    /// <inheritdoc />
    public class Hack : Ability
    {
        /// <inheritdoc />
        public override string Command { get; set; } = "Hack";

        /// <inheritdoc />
        public override string[] Aliases { get; set; } = Array.Empty<string>();

        /// <inheritdoc />
        public override string Description { get; set; } = "Attempts to execute another role's ability.";

        /// <inheritdoc />
        public override HashSet<RoleType> RequiredRoles { get; set; } = new HashSet<RoleType>
        {
            RoleType.ChaosConscript,
            RoleType.ChaosMarauder,
            RoleType.ChaosRepressor,
            RoleType.ChaosRifleman,
        };

        /// <inheritdoc />
        [YamlIgnore]
        public override bool GlobalCooldown { get; set; } = true;

        /// <inheritdoc />
        [YamlIgnore]
        public override int Cooldown { get; set; }

        /// <summary>
        /// Gets or sets the translations to use in the ability.
        /// </summary>
        public HackTranslations Translations { get; set; } = new HackTranslations();

        /// <inheritdoc />
        public override bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get(sender);
            if (player == null)
            {
                response = "This command must be executed from the game level.";
                return false;
            }

            if (arguments.Count == 0)
            {
                response = Translations.AbilityUnspecified;
                return false;
            }

            Ability ability = Get(arguments.At(0));
            return ability == null ? Execute(player, out response) : ability.Execute(player, out response, true);
        }

        /// <inheritdoc />
        protected override bool RunAbility(Player player, out string response)
        {
            response = Translations.AbilityNotFound;
            return false;
        }

        /// <summary>
        /// Contains translatable strings for the <see cref="Hack"/> ability.
        /// </summary>
        public class HackTranslations
        {
            /// <summary>
            /// Gets or sets the response to give when an ability that does not exist is attempted to be used.
            /// </summary>
            public string AbilityNotFound { get; set; } = "That ability does not exist.";

            /// <summary>
            /// Gets or sets the response to give when the player does not specify an ability.
            /// </summary>
            public string AbilityUnspecified { get; set; } = "Specify the ability to hack.";
        }
    }
}