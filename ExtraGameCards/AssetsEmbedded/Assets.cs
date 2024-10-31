using UnityEngine;

namespace EGC.AssetsEmbedded
{
    internal static class Assets
    {
        //Mario
        private static readonly AssetBundle MarioArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("block", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject MarioArt = MarioArtBundle.LoadAsset<GameObject>("C_Block");

        private static readonly AssetBundle SuperMushArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("supermush", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject SuperMushArt = SuperMushArtBundle.LoadAsset<GameObject>("C_SuperMush");

        private static readonly AssetBundle MiniMushArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("minimush", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject MiniMushArt = MiniMushArtBundle.LoadAsset<GameObject>("C_MiniMush");

        private static readonly AssetBundle BooMushArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("boomush", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject BooMushArt = BooMushArtBundle.LoadAsset<GameObject>("C_BooMush");

        private static readonly AssetBundle OneUpMushArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("oneupmush", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject OneUpMushArt = OneUpMushArtBundle.LoadAsset<GameObject>("C_OneUpMush");

        private static readonly AssetBundle PoisonMushArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("poisonmush", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject PoisonMushArt = PoisonMushArtBundle.LoadAsset<GameObject>("C_PoisonMush");


        //ROR2
        private static readonly AssetBundle EgocentrismArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("egocentrism", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject EgocentrismArt = EgocentrismArtBundle.LoadAsset<GameObject>("C_Egocentrism");

        private static readonly AssetBundle BeadsArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("beads", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject BeadsArt = BeadsArtBundle.LoadAsset<GameObject>("C_BeadsOfFealty");

        private static readonly AssetBundle GlowingArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("glowing", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject GlowingArt = GlowingArtBundle.LoadAsset<GameObject>("C_GlowingMeteorite");

        private static readonly AssetBundle ShapedGlassArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("shapedglass", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject ShapedGlassArt = ShapedGlassArtBundle.LoadAsset<GameObject>("C_ShapedGlass");

        private static readonly AssetBundle GestureArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gesture", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject GestureArt = GestureArtBundle.LoadAsset<GameObject>("C_GestureOfTheDrowned");

        private static readonly AssetBundle StoneFluxPauldronArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("stoneflux", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject StoneFluxPauldronArt =
            StoneFluxPauldronArtBundle.LoadAsset<GameObject>("C_StoneFluxPauldron");


        //DDLC
        private static readonly AssetBundle MarkovArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("portraitofmarkov", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject PortraitOfMarkovArt =
            MarkovArtBundle.LoadAsset<GameObject>("C_PortraitOfMarkov");

        private static readonly AssetBundle MarkovBlindEyeArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("eyeblind", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject MarkovBlindEyeArt =
            MarkovBlindEyeArtBundle.LoadAsset<GameObject>("C_EyeBlind");

        private static readonly AssetBundle MarkovEyeMadnessArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("eyemadness", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject MarkovEyeMadnessArt =
            MarkovEyeMadnessArtBundle.LoadAsset<GameObject>("C_EyeMadness");

        private static readonly AssetBundle MarkovEyeThirdArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("eyethirdeye", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject MarkovEyeThirdArt =
            MarkovEyeThirdArtBundle.LoadAsset<GameObject>("C_ThirdEye");

        private static readonly AssetBundle MarkovEyeTraumaArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("eyetrauma", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject MarkovEyeTraumaArt =
            MarkovEyeTraumaArtBundle.LoadAsset<GameObject>("C_EyeTrauma");

        private static readonly AssetBundle MarkovEyeUnimpressedArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("eyeunimpressed", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject MarkovEyeUnimpressedArt =
            MarkovEyeUnimpressedArtBundle.LoadAsset<GameObject>("C_Unimpressed");



        //TBOI
        private static readonly AssetBundle TwentyArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("twenty", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject TwentyArt = TwentyArtBundle.LoadAsset<GameObject>("C_Twenty");


        //OMORI
        private static readonly AssetBundle SomethingSoundBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("something_noise", typeof(ExtraGameCards).Assembly);
        public static readonly AudioClip
            SomethingNoise = SomethingSoundBundle.LoadAsset<AudioClip>("A_Something_Noise");

        private static readonly AssetBundle SomethingArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("something_art", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject SomethingArt = SomethingArtBundle.LoadAsset<GameObject>("C_Something");


        //Undertale
        private static readonly AssetBundle GasterBlasterSoundBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_noise",
                typeof(ExtraGameCards).Assembly);
        public static readonly AudioClip GasterBlasterNoise =
            GasterBlasterSoundBundle.LoadAsset<AudioClip>("A_GatserBlaster_Noise");

        private static readonly AssetBundle GasterBlasterArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_art", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject GasterBlasterArt =
            GasterBlasterArtBundle.LoadAsset<GameObject>("C_GasterBlaster");

        private static readonly AssetBundle GasterBlasterBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gasterblaster_sprite",
                typeof(ExtraGameCards).Assembly);
        public static readonly GameObject GasterBlasterSprite =
            GasterBlasterBundle.LoadAsset<GameObject>("S_GasterBlaster");

        //Enter the Gungeon
        private static readonly AssetBundle BulletBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("bullet_sprite",
                typeof(ExtraGameCards).Assembly);
        public static readonly GameObject BulletSprite =
            BulletBundle.LoadAsset<GameObject>("S_Bullet");

        private static readonly AssetBundle GunBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("gun_sprites",
                typeof(ExtraGameCards).Assembly);
        public static readonly GameObject GunSprite =
            GunBundle.LoadAsset<GameObject>("S_Gun");

        private static readonly AssetBundle BulletArtBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("bullet_art",
                typeof(ExtraGameCards).Assembly);
        public static readonly GameObject BulletArt =
            BulletArtBundle.LoadAsset<GameObject>("C_Bullet");

        // Stanley
        private static readonly AssetBundle BucketSoundBundle =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("bucket_noise", typeof(ExtraGameCards).Assembly);
        public static readonly AudioClip BucketNoise = BucketSoundBundle.LoadAsset<AudioClip>("A_Bucket");

        private static readonly AssetBundle BucketBundleSprite =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("bucket_sprite", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject BucketSprite = BucketBundleSprite.LoadAsset<GameObject>("S_Bucket");

        private static readonly AssetBundle BucketBundleArt =
            Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("bucket_art", typeof(ExtraGameCards).Assembly);
        public static readonly GameObject BucketArt = BucketBundleArt.LoadAsset<GameObject>("C_Bucket");
    }
}