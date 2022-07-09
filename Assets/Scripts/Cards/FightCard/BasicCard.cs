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