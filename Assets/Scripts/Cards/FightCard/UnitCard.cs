using System;
using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using Classes;
using Fighting;
using UI;
using UnityEngine;

namespace Cards.Fighting
{
    [CreateAssetMenu(fileName = "Attack Card" , menuName = "Cards/Fighting/Unit Card")]
    public class UnitCard : BasicCard
    {
        public bool ranged=false;
        public Statistics Statistics;

        private void Awake()
        {
            ActiveTypeCard = ActiveTypeCard.map;
        }

        public override void SelectCard()
        {
            FightController.Instance.SelectCard(this);
        }

        public override void CastCard(UICardSlot who)
        {
            if (who.IsPossibleSetUnitCard())
            {
                FightController.Instance.CurrentMoving.RemoveCard(this);
                who.SetUnitCard(this);
                FightController.Instance.UsedCard();
            }
            else
            {
                FightController.Instance.DeselectCard();
            }
        }

        public void Attack(UICardSlot who)
        {
            if (who.UnitCard == null)
            {
                FightController.Instance.DeselectCard();
                return;
            }
                who.UnitCard.Statistics.heal -= (Statistics.damage - who.UnitCard.Statistics.def);
            if (who.UnitCard.Statistics.heal <= 0)
            {
                who.RemoveUnitCard();
            }
            FightController.Instance.UsedCard();
        }
        
        public override Statistics GetStatistics()
        {
            var s = new Statistics();
            s = Statistics;
            return s;
        }
    }
}