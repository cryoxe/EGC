﻿using System;
using System.Runtime.CompilerServices;
using HarmonyLib;

namespace EGC.Extensions
{
    // From PCE
    [Serializable]
    public class CharacterStatModifiersAdditionalData
    {
        public int markovChoice;
        public int egocentrismPower;
        public int numberOfGaster;

        public bool hasBooMush;
        public bool hasOneUpMush;
        public bool hasPoisonMush;
        public bool hasMiniMush;
        public CharacterStatModifiersAdditionalData()
        {
            markovChoice = 0;
            egocentrismPower = 0;
            numberOfGaster = 0;

            hasOneUpMush = false;
            hasPoisonMush = false;
            hasBooMush = false;
            hasMiniMush = false;
        }
    }
    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData> data =
            new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData>();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(this CharacterStatModifiers characterstats)
        {
            return data.GetOrCreateValue(characterstats);
        }

        public static void AddData(this CharacterStatModifiers characterstats, CharacterStatModifiersAdditionalData value)
        {
            try
            {
                data.Add(characterstats, value);
            }
            catch (Exception) { }
        }

    }
    // reset additional CharacterStatModifiers when ResetStats is called
    [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
    internal class CharacterStatModifiersPatchResetStats
    {
        private static void Prefix(CharacterStatModifiers __instance)
        {
            __instance.GetAdditionalData().markovChoice = 0;
        }
    }
}