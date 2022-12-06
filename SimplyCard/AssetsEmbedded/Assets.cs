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
        //Mario
        private static readonly AssetBundle MarioArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("block", typeof(EGC).Assembly);
        public static GameObject MarioArt = MarioArtBundle.LoadAsset<GameObject>("C_Block");

        private static readonly AssetBundle SuperMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("supermush", typeof(EGC).Assembly);
        public static GameObject SuperMushArt = SuperMushArtBundle.LoadAsset<GameObject>("C_SuperMush");

        private static readonly AssetBundle MiniMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("minimush", typeof(EGC).Assembly);
        public static GameObject MiniMushArt = MiniMushArtBundle.LoadAsset<GameObject>("C_MiniMush");

        private static readonly AssetBundle BooMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("boomush", typeof(EGC).Assembly);
        public static GameObject BooMushArt = BooMushArtBundle.LoadAsset<GameObject>("C_BooMush");

        private static readonly AssetBundle OneUpMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("oneupmush", typeof(EGC).Assembly);
        public static GameObject OneUpMushArt = OneUpMushArtBundle.LoadAsset<GameObject>("C_OneUpMush");

        private static readonly AssetBundle PoisonMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("poisonmush", typeof(EGC).Assembly);
        public static GameObject PoisonMushArt = PoisonMushArtBundle.LoadAsset<GameObject>("C_PoisonMush");


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
        public static AudioClip GasterBlasterNoise = GasterBlasterSoundBundle.LoadAsset<AudioClip>("A_GatserBlaster_Noise");

        private static readonly AssetBundle GasterBlasterArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_art", typeof(EGC).Assembly);
        public static GameObject GasterBlasterArt = GasterBlasterArtBundle.LoadAsset<GameObject>("C_GasterBlaster");

        private static readonly AssetBundle GasterBlasterBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_sprite", typeof(EGC).Assembly);
        public static GameObject GasterBlasterSprite = GasterBlasterBundle.LoadAsset<GameObject>("S_GasterBlaster");

    }
}
