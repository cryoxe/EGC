using System.Collections;
using System.Collections.ObjectModel;
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
using UnboundLib.Utils;
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

        internal static readonly CardCategory Normal = CustomCardCategories.instance.CardCategory("Normal");

        internal static readonly CardCategory CardManipulation =
            CustomCardCategories.instance.CardCategory("CardManipulation");

        internal static readonly CardCategory Markov = CustomCardCategories.instance.CardCategory("Markov");
        internal static readonly CardCategory Lunar = CustomCardCategories.instance.CardCategory("Lunar");

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
            CustomCard.BuildCard<BeadsOfFealty>();// DONE
            CustomCard.BuildCard<GestureOfTheDrowned>(card => GestureOfTheDrowned.GestureOfTheDrownedCard = card); // DONE
            CustomCard.BuildCard<ShapedGlass>(card => ShapedGlass.ShapedGlassCard = card); // DONE
            CustomCard.BuildCard<StoneFluxPauldron>(card => StoneFluxPauldron.StoneFluxPauldronCard = card); // DONE
            CustomCard.BuildCard<GlowingMeteorite>(card => GlowingMeteorite.GlowingMeteoriteCard = card); // DONE
            //CustomCard.BuildCard<Egocentrism>(card => Egocentrism.egocentrismCard = card);

            // DOKI DOKI LITERATURE CLUB
            CustomCard.BuildCard<PortraitOfMarkov>(); // NEED REWORK
            CustomCard.BuildCard<OpenYourThirdEye>(card => OpenYourThirdEye.OpenYourThirdEyeCard = card); // DONE
            CustomCard.BuildCard<TurningABlindEye>(card => TurningABlindEye.TurningABlindEyeCard = card); // DONE
            CustomCard.BuildCard<Trauma>(card => Trauma.TraumaCard = card); // DONE
            CustomCard.BuildCard<Madness>(card => Madness.MadnessCard = card); // DONE
            CustomCard.BuildCard<Unimpressed>(card => Unimpressed.UnimpressedCard = card); // DONE

            // Welcome To the Gungeon
            // THE stanley parable

            //HOOKS !
            GameModeManager.AddHook(GameModeHooks.HookGameStart, GameStart);
            GameModeManager.AddHook(GameModeHooks.HookPlayerPickEnd, gm => PortraitOfMarkov.MarkovPick());
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, gm => SpawnBulletsEffect.DestroyOnBattleEnd());


            SetupMultiplayer();

            Instance.ExecuteAfterSeconds(1, () =>
            {
                //all cards that are not "lunar" or "markov" or ... are now "normal"
                foreach (var card in CardManager.cards.Values.Where(card =>
                             !card.cardInfo.categories.Contains(Markov) &&
                             !card.cardInfo.categories.Contains(Lunar)))
                {
                    card.cardInfo.categories = card.cardInfo.categories.AddToArray(Normal);
                }

                AddRestrictedCard(ShapedGlass.ShapedGlassCard);
                AddRestrictedCard(GestureOfTheDrowned.GestureOfTheDrownedCard);
                AddRestrictedCard(StoneFluxPauldron.StoneFluxPauldronCard);
                AddRestrictedCard(GlowingMeteorite.GlowingMeteoriteCard);

                AddRestrictedCard(OpenYourThirdEye.OpenYourThirdEyeCard);
                AddRestrictedCard(TurningABlindEye.TurningABlindEyeCard);
                AddRestrictedCard(Trauma.TraumaCard);
                AddRestrictedCard(Madness.MadnessCard);
                AddRestrictedCard(Unimpressed.UnimpressedCard);

                AddHiddenCard(MiniMushroom.MiniMushroomCard);
                AddHiddenCard(SuperMushroom.SuperMushroomCard);
                AddHiddenCard(OneUpMushroom.OneUpMushroomCard);
                AddHiddenCard(PoisonousMushroom.PoisonousMushroomCard);
                AddHiddenCard(BooMushroom.BooMushroomCard);
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



        // Hidden cards are not shown in the main menu and will not be added to the card pool
        private static void AddHiddenCard(CardInfo card)
        {
            ModdingUtils.Utils.Cards.instance.AddHiddenCard(card);
        }

        // Restricted cards are not shown in the main menu but will be added to the card pool
        private static void AddRestrictedCard(CardInfo card)
        {
            ModdingUtils.Utils.Cards.instance.AddHiddenCard(card);

            Instance.ExecuteAfterFrames(15,
                () =>
                {
                    ((ObservableCollection<CardInfo>)typeof(CardManager).GetField("activeCards",
                            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)!
                        .GetValue(null)).Add(card);
                });
        }
    }
}