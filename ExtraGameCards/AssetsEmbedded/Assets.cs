using UnityEngine;

namespace EGC.AssetsEmbedded
{
    internal class Assets
    {
        //Mario
        private static readonly AssetBundle MarioArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("block", typeof(ExtraGameCards).Assembly);
        public static GameObject MarioArt = MarioArtBundle.LoadAsset<GameObject>("C_Block");

        private static readonly AssetBundle SuperMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("supermush", typeof(ExtraGameCards).Assembly);
        public static GameObject SuperMushArt = SuperMushArtBundle.LoadAsset<GameObject>("C_SuperMush");

        private static readonly AssetBundle MiniMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("minimush", typeof(ExtraGameCards).Assembly);
        public static GameObject MiniMushArt = MiniMushArtBundle.LoadAsset<GameObject>("C_MiniMush");

        private static readonly AssetBundle BooMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("boomush", typeof(ExtraGameCards).Assembly);
        public static GameObject BooMushArt = BooMushArtBundle.LoadAsset<GameObject>("C_BooMush");

        private static readonly AssetBundle OneUpMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("oneupmush", typeof(ExtraGameCards).Assembly);
        public static GameObject OneUpMushArt = OneUpMushArtBundle.LoadAsset<GameObject>("C_OneUpMush");

        private static readonly AssetBundle PoisonMushArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("poisonmush", typeof(ExtraGameCards).Assembly);
        public static GameObject PoisonMushArt = PoisonMushArtBundle.LoadAsset<GameObject>("C_PoisonMush");


        //ROR2
        private static readonly AssetBundle EgocentrismArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("egocentrism", typeof(ExtraGameCards).Assembly);
        public static GameObject EgocentrismArt = EgocentrismArtBundle.LoadAsset<GameObject>("C_Egocentrism");

        private static readonly AssetBundle BeadsArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("beads", typeof(ExtraGameCards).Assembly);
        public static GameObject BeadsArt = BeadsArtBundle.LoadAsset<GameObject>("C_BeadsOfFealty");

        private static readonly AssetBundle GlowingArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("glowing", typeof(ExtraGameCards).Assembly);
        public static GameObject GlowingArt = GlowingArtBundle.LoadAsset<GameObject>("C_GlowingMeteorite");

        private static readonly AssetBundle ShapedGlassArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("shapedglass", typeof(ExtraGameCards).Assembly);
        public static GameObject ShapedGlassArt = ShapedGlassArtBundle.LoadAsset<GameObject>("C_ShapedGlass");

        private static readonly AssetBundle GestureArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gesture", typeof(ExtraGameCards).Assembly);
        public static GameObject GestureArt = GestureArtBundle.LoadAsset<GameObject>("C_GestureOfTheDrowned");

        private static readonly AssetBundle StoneFluxPauldronArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("stoneflux", typeof(ExtraGameCards).Assembly);
        public static GameObject StoneFluxPauldronArt = StoneFluxPauldronArtBundle.LoadAsset<GameObject>("C_StoneFluxPauldron");


        //DDLC
        private static readonly AssetBundle MarkovArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("portraitofmarkov", typeof(ExtraGameCards).Assembly);
        public static GameObject PortraitOfMarkovArt = MarkovArtBundle.LoadAsset<GameObject>("C_PortraitOfMarkov");


        //TBOI
        private static readonly AssetBundle TwentyArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("twenty", typeof(ExtraGameCards).Assembly);
        public static GameObject TwentyArt = TwentyArtBundle.LoadAsset<GameObject>("C_Twenty");


        //OMORI
        private static readonly AssetBundle SomethingSoundBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("something_noise", typeof(ExtraGameCards).Assembly);
        public static AudioClip SomethingNoise = SomethingSoundBundle.LoadAsset<AudioClip>("A_Something_Noise");

        private static readonly AssetBundle SomethingArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("something_art", typeof(ExtraGameCards).Assembly);
        public static GameObject SomethingArt = SomethingArtBundle.LoadAsset<GameObject>("C_Something");


        //Undertale
        private static readonly AssetBundle GasterBlasterSoundBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_noise", typeof(ExtraGameCards).Assembly);
        public static AudioClip GasterBlasterNoise = GasterBlasterSoundBundle.LoadAsset<AudioClip>("A_GatserBlaster_Noise");

        private static readonly AssetBundle GasterBlasterArtBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_art", typeof(ExtraGameCards).Assembly);
        public static GameObject GasterBlasterArt = GasterBlasterArtBundle.LoadAsset<GameObject>("C_GasterBlaster");

        private static readonly AssetBundle GasterBlasterBundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_sprite", typeof(ExtraGameCards).Assembly);
        public static GameObject GasterBlasterSprite = GasterBlasterBundle.LoadAsset<GameObject>("S_GasterBlaster");

    }
}
