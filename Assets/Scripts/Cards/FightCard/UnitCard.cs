using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using Classes;
using UnityEngine;

namespace Cards.Fighting
{
    [CreateAssetMenu(fileName = "Attack Card" , menuName = "Cards/Fighting/Unit Card")]
    public class UnitCard : BasicCard
    {
        public Statistics Statistics;
        public override void SelectCard()
        {
            throw new System.NotImplementedException();
        }

        public override void CastCard(Transform who)
        {
            throw new System.NotImplementedException();
        }
    }
}