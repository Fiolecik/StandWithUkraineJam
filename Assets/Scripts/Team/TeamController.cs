using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using Game;
using Interfaces;
using Other;
using UnityEngine;

namespace Team
{
    public class TeamController : MonoBehaviour
    {
        public string Name { get; set; }
        public TeamCardSet CardSet
        {
            get => cardSet;
        }
        public ResourcesController ResourcesController { get => resourcesController; }

        public Card SelectedCard
        {
            get => selectedCard;
        }

        private Card selectedCard;
        public bool CPU { get=>cpu; set => cpu=value; }
        private bool cpu;

        private ResourcesController resourcesController;
        [SerializeField] private TeamCardSet cardSet;
        
        private void Awake()
        {
            resourcesController = GetComponent<ResourcesController>();
            TourController.Instance.AddTeam(this);
        }

        public void SelectCard(Card card)
        {
            if (!resourcesController.CanRemoveLucky(card.luckyCost))
                return;
            for (int i = 0; i < 4; i++)
            {
                if(!resourcesController.HasResource((PlayerResource)i+2, card.resources[i] ))
                {
                    return;
                }
            }
            selectedCard = card;
        }

        public void DeselectCard()
        {
            selectedCard = null;
        }

        public void CastCard(Transform who)
        {
            if (!resourcesController.CanRemoveLucky(selectedCard.luckyCost))
                return;
            for (int i = 0; i < 4; i++)
            {
                if(!resourcesController.HasResource((PlayerResource)i+2, selectedCard.resources[i] ))
                {
                    return;
                }
            }

            resourcesController.RemoveLucky(selectedCard.luckyCost);
            who.GetComponent<UnitController>().AddCard(selectedCard.basicCard);
        }
    }
}