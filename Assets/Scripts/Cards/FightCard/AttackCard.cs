using System.Collections;
using System.Collections.Generic;
using Fighting;
using UI;
using UnityEngine;

namespace Cards.Fighting
{
    [CreateAssetMenu(fileName = "Attack Card" , menuName = "Cards/Fighting/Attack Card")]
    public class AttackCard : BasicCard
    {
        public int damage;
        
        private void Awake()
        {
            ActiveTypeCard = ActiveTypeCard.enemyUnit;
        }
        public override void SelectCard()
        {
            FightController.Instance.SelectCard(this);
        }

        public override void CastCard(UICardSlot who)
        {
            if (who.UnitCard == null)
                return;

            who.UnitCard.Statistics.heal -= (damage - who.UnitCard.Statistics.def);
            if (who.UnitCard.Statistics.heal <= 0)
            {
                who.RemoveUnitCard();
            }
            FightController.Instance.UsedCard();
        }
    }
}