using System;
using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using Patterns;
using Team;
using UI;
using UnityEngine;

namespace Fighting
{
    public class FightController : SingletonMonoBehaviour<FightController>
    {
        public UnitController CurrentMoving
        {
            get => Fighters[tour];
        }

        public int Tour
        {
            get => tour;
        }

        public BasicCard SelectedCard
        {
            get=>selectedCard;
        }
        
        public UnitController[] Fighters = new UnitController[2];
        
        [SerializeField] private GameObject battleMap;
        [SerializeField] private UnitController oneplayer, twoplayer;
        
        private int tour = 0;
        private BasicCard selectedCard;
        private UnitCard selectedUnit;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                StartFight(oneplayer,twoplayer);
            }
        }

        public void StartFight(UnitController playerOne, UnitController playerTwo)
        {
            Fighters[0] = playerOne;
            Fighters[1] = playerTwo;
            battleMap.gameObject.SetActive(true);
        }

        public void SelectUnit(int team, UICardSlot unitCard)
        {
            if (team == tour)
            {
                selectedUnit = unitCard.UnitCard;
            }
            else
            {
                if (selectedUnit != null)
                {
                    selectedUnit.Attack(unitCard);
                }
            }
        }
        
        public void SelectCard(BasicCard basicCard)
        {
            selectedCard = basicCard;
        }

        public void UsedCard()
        {
            NextTour();
        }

        public void NextTour()
        {
            selectedCard = null;
            tour++;
            if (tour >= Fighters.Length)
                tour = 0;
        }

        private void CheckCards()
        {
            
        }
    }
}