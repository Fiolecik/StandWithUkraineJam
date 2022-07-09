using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Map Card", menuName = "Cards/Map card")]
    public class Card : ScriptableObject
    {
        public string name;
        public string description;
        public int luckyCost;
        public int[] resources;
        public Sprite icon;
        public BasicCard basicCard;
    }
}