using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.Fighting
{
    [CreateAssetMenu(fileName = "Attack Card" , menuName = "Cards/Fighting/Attack Card")]
    public class AttackCard : BasicCard
    {
        public int damage;
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