using System;
using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Map Card", menuName = "Cards/Map card")]
    public class Card : ScriptableObject
    {
        private void OnEnable()
        {
            if (basicCard != null)
            {
                basicCard.description = description;
                basicCard.name = name;
                basicCard.sprite = icon;
            }
        }

        public string name;
        public string description;
        public int luckyCost;
        public int[] resources = new int[4];
        public Sprite icon;
        public BasicCard basicCard;
    }
}