using System.Collections;
using System.Linq;
using BepInEx;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using EGC.AssetsEmbedded;
using EGC.Cards;
using EGC.Cards.Lunar;
using EGC.Cards.MarioPowerUps;
using EGC.Cards.MarkovChoice;
using EGC.Extensions.SpawnBullet;
using EGC.MonoBehaviours.GasterBlaster;
using HarmonyLib;
using Photon.Pun;
using RarityLib.Utils;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEngine;

namespace EGC
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound")]
    [BepInDependency("pykess.rounds.plugins.moddingutils")]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch")]
    [BepInDependency("com.willis.rounds.modsplus")]
    [BepInDependency("root.rarity.lib")]
    [BepInDependency("com.root.projectile.size.patch")]

    // Declares our mod to BepInEx
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class ExtraGameCards : BaseUnityPlugin
    {
        private const string ModId = "com.cryoxe.rounds.ExtraGameCards";
        private const string ModName = "ExtraGameCards";
        private const string Version = "1.1.6";
        public const string ModInitials = "EGC";
        public static ExtraGameCards? Instance { get; private set; }

        internal static CardCategory Normal = CustomCardCategories.instance.CardCategory("Normal");
        internal static CardCategory CardManipulation = CustomCardCategories.instance.CardCategory("CardManipulation");
        internal static CardCategory Markov = CustomCardCategories.instance.CardCategory("Markov");
        internal static CardCategory Lunar = CustomCardCategories.instance.CardCategory("Lunar");
        internal static CardCategory MarioPowerUps = CustomCardCategories.instance.CardCategory("MarioPowerUps");

        private void Awake()
        {
            RarityUtils.AddRarity(
                "Lunar",
                0.85f,
                new Color(0.5f, 0.85f, 0.8f),
                new Color(0.38f, 0.64f, 0.6f));
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        private void Start()
        {
            Instance = this;

            // INSCRYPTION
            //CustomCard.BuildCard<BoneLord>(); // W.I.P

            // THE BINDING OF ISAAC
            CustomCard.BuildCard<Twenty>(); // DONE

            // ELDEN RING
            CustomCard.BuildCard<Jar>(); // DONE

            // FNAF
            //CustomCard.BuildCard<PurpleGuy>(); // NEED REWORK + NEED ART

            // OMORI
            CustomCard.BuildCard<Something>(); // DONE

            // UNDERTALE
            CustomCard.BuildCard<GasterBlaster>(); // NOT WORKING

            // MARIO POWER-UPS
            CustomCard.BuildCard<MarioBlock>(); // DONE
            CustomCard.BuildCard<MiniMushroom>(card => MiniMushroom.MiniMushroomCard = card); // DONE
            CustomCard.BuildCard<SuperMushroom>(card => SuperMushroom.SuperMushroomCard = card); // DONE
            CustomCard.BuildCard<OneUpMushroom>(card => OneUpMushroom.OneUpMushroomCard = card); // DONE
            CustomCard.BuildCard<PoisonousMushroom>(card => PoisonousMushroom.PoisonousMushroomCard = card); // DONE
            CustomCard.BuildCard<BooMushroom>(card => BooMushroom.BooMushroomCard = card); // DONE

            // RISK OF RAIN 2
            CustomCard.BuildCard<BeadsOfFealty>(); // DONE
            CustomCard.BuildCard<GestureOfTheDrowned>(); // DONE
            CustomCard.BuildCard<ShapedGlass>(); // DONE
            CustomCard.BuildCard<StoneFluxPauldron>(); //
            CustomCard.BuildCard<GlowingMeteorite>(); // DONE
            //CustomCard.BuildCard<Egocentrism>(card => Egocentrism.egocentrismCard = card);

            // DOKI DOKI LITERATURE CLUB
            CustomCard.BuildCard<PortraitOfMarkov>(); // NEED REWORK
            CustomCard.BuildCard<OpenYourThirdEye>(); // NEED REWORK + NEED ART
            CustomCard.BuildCard<TurningABlindEye>(); // NEED REWORK + NEED ART
            CustomCard.BuildCard<Trauma>(); // NEED REWORK + NEED ART
            CustomCard.BuildCard<Madness>(); // NEED REWORK + NEED ART
            CustomCard.BuildCard<Unimpressed>(); // NEED REWORK + NEED ART

            // Welcome To the Gungeon
            // THE stanley parable

            //HOOKS !
            GameModeManager.AddHook(GameModeHooks.HookGameStart, GameStart);
            GameModeManager.AddHook(GameModeHooks.HookPlayerPickEnd, gm => PortraitOfMarkov.MarkovPick());
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, gm => SpawnBulletsEffect.DestroyOnRoundEnd());


            SetupMultiplayer();

            Instance.ExecuteAfterSeconds(1, () =>
            {
                //all cards that are not "lunar" or "markov" or ... are now "normal"
                foreach (var card in UnboundLib.Utils.CardManager.cards.Values.Where(card =>
                             !card.cardInfo.categories.Contains(Markov) &&
                             !card.cardInfo.categories.Contains(Lunar) &&
                             !card.cardInfo.categories.Contains(MarioPowerUps)))
                {
                    card.cardInfo.categories = card.cardInfo.categories.AddToArray(Normal);
                }
            });
        }

        private static IEnumerator GameStart(IGameModeHandler gm)
        {
            //these categories are now blacklisted (not in common pool)
            foreach (var player in PlayerManager.instance.players)
            {
                var characterData =
                    ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats);
                if (!characterData.blacklistedCategories.Contains(Markov))
                    characterData.blacklistedCategories.Add(Markov);
                if (!characterData.blacklistedCategories.Contains(Lunar))
                    characterData.blacklistedCategories.Add(Lunar);
                if (!characterData.blacklistedCategories.Contains(MarioPowerUps))
                    characterData.blacklistedCategories.Add(MarioPowerUps);
            }

            yield break;
        }

        private static void SetupMultiplayer()
        {
            GameObject blasterPrefab = Assets.GasterBlasterSprite;
            blasterPrefab.AddComponent<PhotonView>();
            blasterPrefab.AddComponent<GasterBlasterMono>();
            blasterPrefab.AddComponent<AudioSource>();

            PhotonNetwork.PrefabPool.RegisterPrefab(blasterPrefab.name, blasterPrefab);
        }
    }
}