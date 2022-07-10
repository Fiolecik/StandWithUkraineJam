using System;
using System.Collections;
using System.Collections.Generic;
using Cards;
using Game;
using Interfaces;
using Other;
using Unit;
using UnityEngine;
using UnityEngine.Events;

namespace Team
{
    public class TeamController : MonoBehaviour
    {
        public UnityEvent OnTeamChangedCard = new UnityEvent();
        public string Name { get; set; }
        public int TeamId { get; set; }
        public TeamCardSet CardSet
        {
            get => cardSet;
        }

        public EntityActions SpawnedUnit
        {
            get => spawnedUnit;
        }

        public EntityController EntityController
        {
            get => entityController;
        }

        private EntityController entityController;
        private EntityActions spawnedUnit;
        public ResourcesController ResourcesController { get => resourcesController; }

        public Card SelectedCard
        {
            get => selectedCard;
        }

        public Transform Spawn
        {
            get => spawn;
        }

        private Card selectedCard;
        public bool CPU { get=>cpu; set => cpu=value; }
        private bool cpu;

        private ResourcesController resourcesController;
        [SerializeField] private TeamCardSet cardSet;
        [SerializeField] private GameObject player;
        [SerializeField] private Transform spawn;
        
        private void Awake()
        {
            resourcesController = GetComponent<ResourcesController>();
            TourController.Instance.AddTeam(this);
            GameObject g = Instantiate(player);
            g.transform.position = spawn.transform.position;
            spawnedUnit = g.GetComponent<EntityActions>();
            entityController = g.GetComponent<EntityController>();
            g.GetComponent<UnitController>().TeamParrent = this;
            GetComponent<UnitController>().TeamParrent = this;
        }

        public void SelectCard(Card card)
        {
            if (!resourcesController.CanRemoveLucky(card.luckyCost))
            {
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                if(!resourcesController.HasResource((PlayerResource)i, card.resources[i] ))
                {
                    return;
                }
            }
            selectedCard = card;
            OnTeamChangedCard.Invoke();
        }

        public void DeselectCard()
        {
            selectedCard = null;
            OnTeamChangedCard.Invoke();
        }

        public void CastCard(Transform who)
        {
            if (!resourcesController.CanRemoveLucky(selectedCard.luckyCost))
                return;
            for (int i = 0; i < 4; i++)
            {
                if(!resourcesController.HasResource((PlayerResource)i, selectedCard.resources[i] ))
                {
                    return;
                }
            }

            resourcesController.RemoveLucky(selectedCard.luckyCost);
            var basicCard = selectedCard.basicCard;
            basicCard.name = selectedCard.name;
            basicCard.description = selectedCard.description;
            basicCard.sprite = selectedCard.icon;
            who.GetComponent<UnitController>().AddCard(selectedCard.basicCard);
            selectedCard = null;
            OnTeamChangedCard.Invoke();
        }
    }
}