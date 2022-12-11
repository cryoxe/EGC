using BepInEx;
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
using ExtraGameCards.Cards;
using Photon.Pun;
using ExtraGameCards.AssetsEmbedded;
using UnityEngine;
using RarityLib.Utils;

namespace ExtraGameCards
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willis.rounds.modsplus", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]

    public class EGC : BaseUnityPlugin
    {
        private const string ModId = "com.cryoxe.rounds.ExtraGameCards";
        private const string ModName = "ExtraGameCards";
        public const string Version = "1.0.2";
        public const string ModInitials = "EGC";
        public static EGC? Instance { get; private set; }

        internal static CardCategory Normal;
        internal static CardCategory CardManipulation;
        internal static CardCategory Markov;
        internal static CardCategory Lunar;
        internal static CardCategory MarioPowerUps;

        IEnumerator GameStart(IGameModeHandler gm)
        {
            //these categories are now blacklisted (not in common pool)
            foreach (var player in PlayerManager.instance.players)
            {
                if (!ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(Markov))
                {
                    ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(Markov);
                }
                if (!ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(Lunar))
                {
                    ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(Lunar);
                }
                if (!ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(MarioPowerUps))
                {
                    ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(MarioPowerUps);
                }
            }
            yield break;
        }

        void Awake()
        {
            RarityUtils.AddRarity("Lunar", 0.85f, new Color(0.5f, 0.85f, 0.8f), new Color(0.38f, 0.64f, 0.6f));
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        void Start()
        {
            Instance = this;

            GameObject blasterPrefab = Assets.GasterBlasterSprite;
            PhotonView photonView = blasterPrefab.AddComponent<PhotonView>();

            PhotonNetwork.PrefabPool.RegisterPrefab(blasterPrefab.name, blasterPrefab);

            Normal = CustomCardCategories.instance.CardCategory("Normal");
            Markov = CustomCardCategories.instance.CardCategory("Markov");
            Lunar = CustomCardCategories.instance.CardCategory("Lunar");
            MarioPowerUps = CustomCardCategories.instance.CardCategory("MarioPowerUps");
            CardManipulation = CustomCardCategories.instance.CardCategory("CardManipulation");

            //CustomCard.BuildCard<BoneLord>(); //Maybe Add curses + NEED ART + W.I.P.
            CustomCard.BuildCard<Twenty>(); //Maybe add Glasses skin to player, would be funny + NEED ART
            CustomCard.BuildCard<Jar>();    //DONE + NEED ART
            CustomCard.BuildCard<PurpleGuy>();  //rebalancing

            CustomCard.BuildCard<MarioBlock>();
            CustomCard.BuildCard<MiniMushroom>(card => MiniMushroom.miniMushroomCard = card);
            CustomCard.BuildCard<SuperMushroom>(card => SuperMushroom.superMushroomCard = card);
            CustomCard.BuildCard<OneUpMushroom>(card => OneUpMushroom.oneUpMushroomCard = card);
            CustomCard.BuildCard<PoisonousMushroom>(card => PoisonousMushroom.poisonousMushroomCard = card);
            CustomCard.BuildCard<BooMushroom>(card => BooMushroom.booMushroomCard = card);


            CustomCard.BuildCard<BeadsOfFealty>();
            CustomCard.BuildCard<GestureOfTheDrowned>();    
            CustomCard.BuildCard<ShapedGlass>();    
            CustomCard.BuildCard<StoneFluxPauldron>();  
            CustomCard.BuildCard<GlowingMeteorite>();
            CustomCard.BuildCard<Egocentrism>(card => Egocentrism.egocentrismCard = card);

            CustomCard.BuildCard<PortraitOfMarkov>();   //DONE 
            CustomCard.BuildCard<OpenYourThirdEye>();   //rebalancing + NEED ART
            CustomCard.BuildCard<TurningABlindEye>();    //rebalancing + NEED ART
            CustomCard.BuildCard<Trauma>(); //rebalancing + NEED ART
            CustomCard.BuildCard<Madness>();    //rebalancing + NEED ART
            CustomCard.BuildCard<Unimpressed>();  //rebalancing + NEED ART

            CustomCard.BuildCard<Something>(); //IS NOT REMOVED PROPERLY
            CustomCard.BuildCard<GasterBlaster>();//W.I.P. 
            //CustomCard.BuildCard<ClayBullet>();

            //VAMPIRE SURVIVOR
            //Welcome To the Gungeon
            //THE stanley parable
            //Undertale
            //Duck seasons

            //HOOKS !
            GameModeManager.AddHook(GameModeHooks.HookPlayerPickEnd, (gm) => PortraitOfMarkov.MarkovPick());
            GameModeManager.AddHook(GameModeHooks.HookGameStart, GameStart);

            Instance.ExecuteAfterSeconds(1, () =>
            {
                //all cards that are not "lunar" or "markov" are now "normal"
                foreach (var card in UnboundLib.Utils.CardManager.cards.Values)
                {
                    if (card.cardInfo.categories.Contains(Markov))
                    {
                        //UnityEngine.Debug.Log(card.cardInfo.cardName + " is a MARKOV category Card");
                        continue;
                    }
                    else if (card.cardInfo.categories.Contains(Lunar))
                    {
                        //UnityEngine.Debug.Log(card.cardInfo.cardName + " is a LUNAR category Card");
                        continue;
                    }
                    else if (card.cardInfo.categories.Contains(MarioPowerUps))
                    {
                        //UnityEngine.Debug.Log(card.cardInfo.cardName + " is a POWER UP category Card");
                        continue;
                    }
                    else
                    {
                        //UnityEngine.Debug.Log(card.cardInfo.cardName + " is a NORMAL category Card");
                        card.cardInfo.categories = card.cardInfo.categories.AddToArray(Normal);
                    }
                }
            });
        }
    }
}
