using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using SimplyCard.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using RarityLib;

namespace ExtraGameCards
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]

    public class EGC : BaseUnityPlugin
    {
        private const string ModId = "com.cryoxe.rounds.ExtraGameCards";
        private const string ModName = "ExtraGameCards";
        public const string Version = "1.0.0";
        public const string ModInitials = "EGC";
        public static EGC Instance { get; private set; }

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            //RarityLib.Utils.RarityUtils.AddRarity("Lunar", 0.4f, new UnityEngine.Color(102 / 255, 204 / 255, 255 / 255), new UnityEngine.Color(0, 170/255, 1));
        }
        void Start()
        {
            Instance = this;
            CustomCard.BuildCard<BoneLord>();
            CustomCard.BuildCard<Twenty>();
            CustomCard.BuildCard<Jar>();
        }
    }
}
