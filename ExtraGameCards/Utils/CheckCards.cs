using System.Collections.Generic;

namespace EGC.Utils
{
    internal class CheckCards
    {
        public static int Amount(Player player, string cardName)
        {
            List<CardInfo> currentCards = player.data.currentCards;
            int cardAmount = 0;
            for (int i = currentCards.Count - 1; i >= 0; i--)
            {
                if (currentCards[i].cardName == cardName)
                {
                    cardAmount++;
                }
            }

            return cardAmount;
        }
    }
}