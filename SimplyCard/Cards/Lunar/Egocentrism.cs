using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ExtraGameCards.Utils;
using ExtraGameCards.MonoBehaviours;

namespace ExtraGameCards.Cards
{
    class Egocentrism : CustomCard
    {
        public static CardInfo StaticCardEgo;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = true;

            cardInfo.categories = new CardCategory[]
            {
                EGC.Lunar
            };

            gun.projectileColor = Color.cyan;
            gun.ammo = 1;
            gun.attackSpeed = 0.9f;
            gun.reloadTimeAdd = 1.15f;
            gun.damage = 1.15f;

            //var explosiveBullet = (GameObject)Resources.Load("0 cards/Explosive bullet");
            //var a_Explosion = explosiveBullet.GetComponent<Gun>().objectsToSpawn[0].effect;
            //var explo = Instantiate(a_Explosion);
            //
            //explo.transform.position = new Vector3(1000, 0, 0);
            //explo.hideFlags = HideFlags.HideAndDontSave;
            //explo.name = "EgocentrismExplo";
            //
            //DestroyImmediate(explo.GetComponent<RemoveAfterSeconds>());
            //var explodsion = explo.GetComponent<Explosion>();
            //explodsion.force = -10000;
            //
            //gun.objectsToSpawn = new[]
            //{
            //    new ObjectsToSpawn
            //    {
            //        effect = explo,
            //        normalOffset = 0.1f,
            //        numberOfSpawns = 1,
            //        scaleFromDamage = 0.5f,
            //        scaleStackM = 0.7f,
            //        scaleStacks = true,
            //    }
            //};
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            var mb = player.transform.gameObject.GetOrAddComponent<EgocentrismMono>();
            mb.player = player;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //UnityEngine.Debug.Log($"[{ExtraCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");

            var mb = player.transform.gameObject.GetComponent<EgocentrismMono>();
            if (mb != null && CheckCards.Amount(player, "Egocentrism") <= 1)
            {
                UnityEngine.Debug.Log("EgoRemoved");
                Destroy(mb);
            }
        }

        protected override string GetTitle()
        {
            return "Egocentrism";
        }
        protected override string GetDescription()
        {
            return "You are stronger <color=#c61a09> But every round, a random card is converted into this card</color>";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Splash DMG",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullet",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload time",
                    amount = "+0.15s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public override string GetModName()
        {
            return EGC.ModInitials;
        }
    }
}