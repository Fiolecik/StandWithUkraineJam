using System.Collections;
using System.Collections.Generic;
using Game;
using Patterns;
using Team;
using UnityEngine;

namespace UI.Map
{
    public class UICardsController : SingletonMonoBehaviour<UICardsController>
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private float distanceBeetwenCards;

        private List<UICardMap> cardMaps = new List<UICardMap>();

        private TeamController currentTour;
        

        // Update is called once per frame
        void Update()
        {
            CheckIfChanged();
        }

        private void CheckIfChanged()
        {
            if (currentTour != TourController.Instance.CurrentMoving)
            {
                currentTour = TourController.Instance.CurrentMoving;
                UpdateCards();
            }
        }

        private void UpdateCards()
        {
            int cardsLenght = cardMaps.Count;
            Debug.Log("XD");
            if (cardsLenght> currentTour.CardSet.cards.Length)
            {
                for (int i = 0; i < cardsLenght - currentTour.CardSet.cards.Length; i++)
                {
                    Destroy(cardMaps[i]);
                }
            }
            Debug.Log(currentTour.CardSet.cards.Length);
            if (cardsLenght < currentTour.CardSet.cards.Length)
            {
                for (int i = 0; i < currentTour.CardSet.cards.Length-cardsLenght; i++)
                {
                    UICardMap g = Instantiate(cardPrefab, transform).GetComponent<UICardMap>();
                    cardMaps.Add(g);
                }
            }

            for (int i = 0; i < cardMaps.Count; i++)
            {
                cardMaps[i].SetCard(currentTour.CardSet.cards[i]);
            }

            StartCoroutine(LerpPositions());
        }

        private IEnumerator LerpPositions()
        {
            float delta = 0;
            List<Vector3> positions = new List<Vector3>();
            for (int i = 0; i < cardMaps.Count; i++)
            {
                positions.Add(cardMaps[i].transform.position);
            }
            while (delta<1)
            {
                yield return null;
                delta += Time.deltaTime;
                for (int i = 0; i < cardMaps.Count; i++)
                {
                    cardMaps[i].transform.position = Vector3.Lerp(positions[i], transform.position+new Vector3(distanceBeetwenCards*i,0,0), delta);
                }
            }
        }
    }
}