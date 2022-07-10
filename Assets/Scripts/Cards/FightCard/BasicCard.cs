using System.Collections;
using System.Collections.Generic;
using Classes;
using UI;
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
        public string name { get; set; }
        public string description { get; set; }
        public Sprite sprite { get; set; }

        public ActiveTypeCard ActiveTypeCard { get; set; }

        public abstract void SelectCard();

        public abstract void CastCard(UICardSlot who);

        public abstract Statistics GetStatistics();
    }
}