using System.Collections;
using System.Collections.Generic;
using Classes;
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
            {
                FightController.Instance.DeselectCard();
                return;
            }

            if (who.Team == FightController.Instance.Tour)
            {
                FightController.Instance.DeselectCard();
                return;
            }

            who.UnitCard.Statistics.heal -= (damage - who.UnitCard.Statistics.def);
            if (who.UnitCard.Statistics.heal <= 0)
            {
                who.RemoveUnitCard();
            }
            FightController.Instance.CurrentMoving.RemoveCard(this);
            FightController.Instance.UsedCard();
        }

        public override Statistics GetStatistics()
        {
            var s = new Statistics();
            s.damage = damage;
            return s;
        }
    }
}