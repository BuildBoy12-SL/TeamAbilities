// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace TeamAbilities
{
    using System.ComponentModel;
    using System.IO;
    using Exiled.API.Features;
    using Exiled.API.Interfaces;
    using Exiled.CustomRoles.API.Features;
    using Exiled.CustomRoles.API.Features.Parsers;
    using Exiled.Loader.Features.Configs;
    using Exiled.Loader.Features.Configs.CustomConverters;
    using TeamAbilities.Configs;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;
    using YamlDotNet.Serialization.NodeDeserializers;

    /// <inheritdoc />
    public class Config : IConfig
    {
        private static readonly ISerializer Serializer = new SerializerBuilder()
            .WithTypeConverter(new VectorsConverter())
            .WithTypeInspector(i => new CommentGatheringTypeInspector(i))
            .WithEmissionPhaseObjectGraphVisitor(a => new CommentsObjectGraphVisitor(a.InnerVisitor))
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .IgnoreFields()
            .Build();

        private static readonly IDeserializer Deserializer = new DeserializerBuilder()
            .WithTypeConverter(new VectorsConverter())
            .WithNodeDeserializer(
                inner => new AbstractNodeNodeTypeResolverWithValidation(inner, new AggregateExpectationTypeResolver<CustomAbility>(UnderscoredNamingConvention.Instance)),
                s => s.InsteadOf<ObjectNodeDeserializer>())
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .IgnoreFields()
            .IgnoreUnmatchedProperties()
            .Build();

        [YamlIgnore]
        public RoleConfigs RoleConfigs { get; private set; }

        /// <inheritdoc />
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debug messages should be shown.
        /// </summary>
        [Description("Indicates whether debug messages should be shown.")]
        public bool ShowDebug { get; set; } = false;

        public string RolesFolder { get; set; } = Path.Combine(Paths.Configs, "TeamAbilities");

        public string RolesFile { get; set; } = "global.yml";

        public void LoadRoleConfigs()
        {
            if (!Directory.Exists(RolesFolder))
                Directory.CreateDirectory(RolesFolder);

            string filePath = Path.Combine(RolesFolder, RolesFile);
            RoleConfigs = !File.Exists(filePath) ? new RoleConfigs() : Deserializer.Deserialize<RoleConfigs>(File.ReadAllText(filePath));
            File.WriteAllText(filePath, Serializer.Serialize(RoleConfigs));
        }
    }
}