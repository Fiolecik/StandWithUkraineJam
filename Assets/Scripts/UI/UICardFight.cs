using System;
using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using Fighting;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICardFight : MonoBehaviour
{
        public bool IsPosibleToSelect { get; set; }
        public Vector3 DefaultPosition { get; set; }
        
        [SerializeField] public Image cardIcon;
        [SerializeField] public TMP_Text count;
        [SerializeField] public TMP_Text description;
        [SerializeField] public TMP_Text name;

        private BasicCard card;
        private bool followCard = false;
        private bool lerping = false;

        private void Update()
        {
            if (FightController.Instance.SelectedCard == this)
            {
                transform.position = DefaultPosition - Vector3.right * 300;
            }
            else
            {
                transform.position = DefaultPosition;
            }
        }

        public void SetCard(BasicCard basicCard)
        {
            card = basicCard;
            cardIcon.sprite = basicCard.sprite;
            name.text = basicCard.name;
            count.text = FightController.Instance.CurrentMoving.Cards.Find(ctg => ctg.basicCard == basicCard).countOfCards.ToString();
            description.text = FightController.Instance.CurrentMoving.Cards.Find(ctg => ctg.basicCard == basicCard).basicCard.description;
        }

        public void SelectCard()
        {
            if (FightController.Instance.CurrentMoving.TeamParrent.CPU)
                return;
            // StartCoroutine(LerpToPosition(DefaultPosition - Vector3.right * 300));
            FightController.Instance.SelectCard(card);
        }

        public void LerpToBasicPosition()
        {
            transform.position = DefaultPosition;
            // StartCoroutine(LerpToPosition(DefaultPosition));
        }
}
