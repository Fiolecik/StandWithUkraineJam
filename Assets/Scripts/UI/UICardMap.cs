using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using Game;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UICardMap : MonoBehaviour
    { 
        public Vector3 DefaultPosition { get; set; }=Vector3.zero;
        [SerializeField] public Image cardIcon;
        [SerializeField] public TMP_Text luckyCost;
        [SerializeField] public TMP_Text[] resourcesCost;
        [SerializeField] public TMP_Text description;
        [SerializeField] public TMP_Text name;
        [SerializeField] public TMP_Text hp;
        [SerializeField] public TMP_Text def;
        [SerializeField] public TMP_Text attack;
        

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
            if (card != null)
            {
                var basicCard = card.basicCard;

                if (basicCard != null)
                {
                    hp.text = basicCard.GetStatistics().heal.ToString();
                    def.text = basicCard.GetStatistics().def.ToString();
                    attack.text = basicCard.GetStatistics().damage.ToString();
                }
            }

        }

        public void SelectCard()
        {
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
                transform.position = DefaultPosition + Vector3.up * 150;
                // StartCoroutine(LerpToPosition(DefaultPosition+Vector3.up*150));
                StartCoroutine(FollowCard());
            }
        }

        public void LerpToBasicPosition()
        {
            transform.position = DefaultPosition;
            // StartCoroutine(LerpToPosition(DefaultPosition));
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