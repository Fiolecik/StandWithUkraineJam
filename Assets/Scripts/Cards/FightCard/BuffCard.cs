using System.Collections;
using System.Collections.Generic;
using Classes;
using UnityEngine;

namespace Cards.Fighting
{
    [CreateAssetMenu(fileName = "Attack Card" , menuName = "Cards/Fighting/Buff card")]
    public class BuffCard : BasicCard
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