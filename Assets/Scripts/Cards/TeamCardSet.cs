using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;

namespace Cards
{
    [CreateAssetMenu(fileName = "Card Set", menuName = "Cards/Team Card Set")]
    public class TeamCardSet : ScriptableObject
    {
        public Card[] cards;
    }
}