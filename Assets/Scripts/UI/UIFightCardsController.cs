using System.Collections;
using System.Collections.Generic;
using Fighting;
using Team;
using UnityEngine;

namespace UI
{


    public class UIFightCardsController : MonoBehaviour
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private float distanceBeetwenCards;

        private List<UICardFight> cardMaps = new List<UICardFight>();

        private UnitController currentTour;


        // Update is called once per frame
        void Update()
        {
            CheckIfChanged();
        }

        private void CheckIfChanged()
        {
            if (currentTour != FightController.Instance.CurrentMoving)
            {
                currentTour = FightController.Instance.CurrentMoving;
                UpdateCards();
            }
        }

        private void UpdateCards()
        {
            int cardsLenght = cardMaps.Count;
            
            for (int i = 0; i < cardsLenght - currentTour.Cards.Count; i++)
            {
                Destroy(cardMaps[0].gameObject);
                cardMaps.RemoveAt(0);
            }
            
            for (int i = 0; i < currentTour.Cards.Count - cardsLenght; i++)
            {
                UICardFight g = Instantiate(cardPrefab, transform).GetComponent<UICardFight>();
                cardMaps.Add(g);
            }


            for (int i = 0; i < cardMaps.Count; i++)
            {
                cardMaps[i].SetCard(currentTour.Cards[i].basicCard);
                cardMaps[i].DefaultPosition = transform.position + Vector3.up * distanceBeetwenCards * i;
                cardMaps[i].StopAllCoroutines();
                cardMaps[i].LerpToBasicPosition();
            }
            Debug.Log(cardMaps.Count);
            Debug.Log(currentTour.Cards.Count);
        }
    }
}