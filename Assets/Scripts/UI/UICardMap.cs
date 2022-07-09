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
        public bool IsPosibleToSelect { get; set; }
        public Vector3 DefaultPosition { get; set; }
        [SerializeField] public Image cardIcon;
        [SerializeField] public TMP_Text luckyCost;
        [SerializeField] public TMP_Text[] resourcesCost;
        [SerializeField] public TMP_Text description;
        [SerializeField] public TMP_Text name;

        private Card card;
        private bool followCard = false;
        private bool lerping = false;

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
            if (TourController.Instance.CPU && !IsPosibleToSelect && lerping)
                return;

            if (TourController.Instance.CurrentMoving.SelectedCard == card)
            {
                LerpToBasicPosition();
                followCard=false;
                TourController.Instance.CurrentMoving.DeselectCard();
                return;
            }
            TourController.Instance.CurrentMoving.SelectCard(card);
            if (TourController.Instance.CurrentMoving.SelectedCard == card)
            {
                followCard = true;
                StartCoroutine(LerpToPosition(DefaultPosition+Vector3.up*150));
                StartCoroutine(FollowCard());
            }
        }

        public void LerpToBasicPosition()
        {
            StartCoroutine(LerpToPosition(DefaultPosition));
        }

        private IEnumerator LerpToPosition(Vector3 position)
        {
            if (lerping)
                yield break;
            lerping = true;
            float delta = 0;
            Vector3 startPosition = transform.position;
            while (delta < 1)
            {
                yield return null;
                delta += Time.deltaTime;
                transform.position = Vector3.Lerp(startPosition, position, delta);
            }

            lerping = false;
        }
        
        private IEnumerator FollowCard()
        {
            while (followCard)
            {
                yield return null;
                if (TourController.Instance.CurrentMoving.SelectedCard != card)
                {
                    lerping=false;
                    StopAllCoroutines();
                    LerpToBasicPosition();
                    followCard = false;
                }
            }
        }
    }
}