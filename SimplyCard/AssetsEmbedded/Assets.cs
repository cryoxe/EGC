using UnboundLib;
using UnboundLib.GameModes;
using UnboundLib.Cards;
using SimplyCard.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ExtraGameCards.AssetsEmbedded
{
    internal class Assets
    {
        //DDLC
        private static readonly AssetBundle MarkovBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("portraitofmarkov", typeof(EGC).Assembly);
        public static GameObject PortraitOfMarkovArt = MarkovBundle.LoadAsset<GameObject>("C_PortraitOfMarkov");

        //OMORI
        private static readonly AssetBundle SomethingSoundBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("something_noise", typeof(EGC).Assembly);
        public static AudioClip SomethingNoise = SomethingSoundBundle.LoadAsset<AudioClip>("A_Something_Noise");

        private static readonly AssetBundle SomethingBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("something_art", typeof(EGC).Assembly);
        public static GameObject SomethingArt = SomethingBundle.LoadAsset<GameObject>("C_Something");

        //Undertale
        private static readonly AssetBundle GasterBlasterSoundBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_noise", typeof(EGC).Assembly);
        public static AudioClip GasterBlasterNoise = GasterBlasterSoundBundle.LoadAsset<AudioClip>("A_GasterBlaster_Noise");

        private static readonly AssetBundle GasterBlasterBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_art", typeof(EGC).Assembly);
        public static GameObject GasterBlasterArt = GasterBlasterBundle.LoadAsset<GameObject>("C_GasterBlaster");

    }
}
