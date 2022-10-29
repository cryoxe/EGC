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

    }
}
