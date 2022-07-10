using System;
using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using Game;
using UnityEngine;
using UnityEngine.Events;

namespace Team
{
    
    public class UnitController : MonoBehaviour
    {
        public class CardHolder
        {
            public int countOfCards;
            public BasicCard basicCard;

            public CardHolder(int i, BasicCard c)
            {
                countOfCards = i;
                basicCard = c;
            }
        }
        [SerializeField] private MeshRenderer higlight;
        public UnityEvent OnLoseFight = new UnityEvent();
        public TeamController TeamParrent { get; set; }
        public List<CardHolder> Cards
        {
            get => cards;
        }
        
        private List<CardHolder> cards = new List<CardHolder>();

        private void Start()
        {
            TeamParrent.OnTeamChangedCard.AddListener(OnCardUpdate);
            higlight.material.color = Color.yellow;
        }

        public void AddCard(BasicCard basicCard)
        {
            int id = cards.FindIndex(ctg => ctg.basicCard == basicCard);
            if (id == -1)
            {
                cards.Add(new CardHolder(0, basicCard));
                id = cards.Count - 1;
            }

            cards[id].countOfCards++;
        }

        public void RemoveCard(BasicCard basicCard)
        {
            int id = cards.FindIndex(ctg => ctg.basicCard == basicCard);
            if (id == -1)
                return;
            cards[id].countOfCards--;
            if (cards[id].countOfCards <= 0)
            {
                cards.RemoveAt(id);
            }
        }

        public void LostFight()
        {
            OnLoseFight.Invoke();
        }

        public void OnMouseEnter()
        {
            if (TourController.Instance.CPU)
                return;
            higlight.material.color = Color.green;
        }

        public void OnMouseExit()
        {
            if (TourController.Instance.CPU)
                return;
            higlight.material.color = Color.yellow;
        }

        private void OnMouseDown()
        {
            if (TourController.Instance.CPU)
                return;
            TeamParrent.CastCard(transform);
        }

        private void OnCardUpdate()
        {
            if (TourController.Instance.CPU)
            {
                higlight.gameObject.SetActive(false);
                return;
            }
            higlight.gameObject.SetActive(TeamParrent.SelectedCard!=null);
        }

        public void DestroyBase()
        {
            Destroy(gameObject);
            Destroy(TeamParrent.SpawnedUnit.gameObject);
        }

        public void RespawnUnit()
        {
            transform.position = TeamParrent.Spawn.position;
        }
        
    }
}