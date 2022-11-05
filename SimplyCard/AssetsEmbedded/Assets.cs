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
        private static readonly AssetBundle MarkovArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("portraitofmarkov", typeof(EGC).Assembly);
        public static GameObject PortraitOfMarkovArt = MarkovArtBundle.LoadAsset<GameObject>("C_PortraitOfMarkov");

        //OMORI
        private static readonly AssetBundle SomethingSoundBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("something_noise", typeof(EGC).Assembly);
        public static AudioClip SomethingNoise = SomethingSoundBundle.LoadAsset<AudioClip>("A_Something_Noise");

        private static readonly AssetBundle SomethingArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("something_art", typeof(EGC).Assembly);
        public static GameObject SomethingArt = SomethingArtBundle.LoadAsset<GameObject>("C_Something");

        //Undertale
        private static readonly AssetBundle GasterBlasterSoundBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_noise", typeof(EGC).Assembly);
        public static AudioClip GasterBlasterNoise = GasterBlasterSoundBundle.LoadAsset<AudioClip>("A_GasterBlaster_Noise");

        private static readonly AssetBundle GasterBlasterArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_art", typeof(EGC).Assembly);
        public static GameObject GasterBlasterArt = GasterBlasterArtBundle.LoadAsset<GameObject>("C_GasterBlaster");

        private static readonly AssetBundle GasterBlasterBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_sprite", typeof(EGC).Assembly);
        public static GameObject GasterBlasterSprite = GasterBlasterBundle.LoadAsset<GameObject>("S_GasterBlaster");

    }
}
