using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cards.Fighting
{
    public enum ActiveTypeCard
    {
        map,
        friendUnit,
        enemyUnit
    }

    [CreateAssetMenu(fileName = "Fight Card", menuName = "Cards/Fight Card")]
    public class BasicCard : ScriptableObject
    {
        public string name;
        public string description;
        
        public virtual void SelectCArd()
        {
            
        }

        public virtual void CastCard(Transform who)
        {
            
        }
    }
}