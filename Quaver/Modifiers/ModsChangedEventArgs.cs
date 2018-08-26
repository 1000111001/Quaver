﻿using System;
using Quaver.API.Enums;

namespace Quaver.Modifiers
{
    public class ModsChangedEventArgs : EventArgs
    {
        /// <summary>
        ///     The newly activated mods/
        /// </summary>
        public ModIdentifier Mods { get; }

        public ModsChangedEventArgs(ModIdentifier mods) => Mods = mods;
    }
}