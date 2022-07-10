using System.Collections;
using System.Collections.Generic;
using Classes;
using Fighting;
using UI;
using UnityEngine;

namespace Cards.Fighting
{
    [CreateAssetMenu(fileName = "Attack Card" , menuName = "Cards/Fighting/Buff card")]
    public class BuffCard : BasicCard
    {
        public Statistics Statistics;
        
        private void Awake()
        {
            ActiveTypeCard = ActiveTypeCard.friendUnit;
        }
        public override void SelectCard()
        {
            FightController.Instance.SelectCard(this);
        }

        public override void CastCard(UICardSlot who)
        {
            if (who.UnitCard == null)
            {
                FightController.Instance.DeselectCard();
                return;
            }

            if (who.Team != FightController.Instance.Tour)
            {
                FightController.Instance.DeselectCard();
                return;
            }

            who.UnitCard.Statistics += Statistics;
            FightController.Instance.CurrentMoving.RemoveCard(this);
            FightController.Instance.UsedCard();
        }
    }
}