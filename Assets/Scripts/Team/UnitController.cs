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
        [SerializeField] private MeshRenderer higlight;
        public UnityEvent OnLoseFight = new UnityEvent();
        public TeamController TeamParrent { get; set; }
        public List<BasicCard> Cards
        {
            get => cards;
        }
        
        private List<BasicCard> cards = new List<BasicCard>();

        private void Start()
        {
            TeamParrent.OnTeamChangedCard.AddListener(OnCardUpdate);
            higlight.material.color = Color.yellow;
        }

        public void AddCard(BasicCard basicCard)
        {
            cards.Add(Instantiate(basicCard));
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
            
        }

        public void RespawnUnit()
        {
            
        }
        
    }
}