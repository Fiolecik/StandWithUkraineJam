using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using UnityEngine;

namespace Team
{
    public class UnitController : MonoBehaviour
    {
        public List<BasicCard> Cards
        {
            get => cards;
        }
        
        private List<BasicCard> cards = new List<BasicCard>();
        public void AddCard(BasicCard basicCard)
        {
            cards.Add(Instantiate(basicCard));
        }
        
    }
}