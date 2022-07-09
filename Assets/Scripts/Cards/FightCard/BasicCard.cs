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
    
    public abstract class BasicCard : ScriptableObject
    {
        public string name;
        public string description;

        public ActiveTypeCard ActiveTypeCard { get; set; }

        public abstract void SelectCard();

        public abstract void CastCard(Transform who);
    }
}