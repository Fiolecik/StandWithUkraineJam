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
            transform.position = DefaultPosition - Vector3.right * 300;
            // StartCoroutine(LerpToPosition(DefaultPosition - Vector3.right * 300));
            StartCoroutine(FollowCard());
            FightController.Instance.SelectCard(card);
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
                if (FightController.Instance.SelectedCard != card)
                {
                    lerping = false;
                    StopAllCoroutines();
                    LerpToBasicPosition();
                    followCard = false;
                    count.text = FightController.Instance.CurrentMoving.Cards.Find(ctg => ctg.basicCard == card).countOfCards.ToString();
                }
            }
        }
}
