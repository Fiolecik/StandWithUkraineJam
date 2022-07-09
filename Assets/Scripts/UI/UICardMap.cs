using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UICardMap : MonoBehaviour
    {
        [SerializeField] public Image cardIcon;
        [SerializeField] public TMP_Text luckyCost;
        [SerializeField] public TMP_Text[] resourcesCost;
        [SerializeField] public TMP_Text description;
        [SerializeField] public TMP_Text name;

        private Card card;

        public void SetCard(Card card)
        {
            this.card = card;
            cardIcon.sprite = card.icon;
            luckyCost.text = card.luckyCost.ToString();
            for (int i = 0; i < 4; i++)
            {
                resourcesCost[i].text = card.resources[i].ToString();
            }

            description.text = card.description;
            name.text = card.name;
        }

        public void SelectCard()
        {
            if (!TourController.Instance.CPU)
            {
                TourController.Instance.CurrentMoving.SelectCard(card);
            }
        }
    }
}