﻿using Quaver.API.Enums;

namespace Quaver.Modifiers.Mods.Mania
{
    internal class ManiaModNoPause : IGameplayModifier
    {
        public string Name { get; set; } = "No Pause";

        public ModIdentifier ModIdentifier { get; set; } = ModIdentifier.NoPause;

        public ModType Type { get; set; } = ModType.Special;

        public string Description { get; set; } = "A wise man once said, \"Pausing is cheating.\"";

        public bool Ranked { get; set; } = true;

        public float ScoreMultiplierAddition { get; set; } = 1.0f;

        public ModIdentifier[] IncompatibleMods { get; set; } =
        {
            ModIdentifier.Autoplay,

        };

        public void InitializeMod()
        {
        }
    }
}