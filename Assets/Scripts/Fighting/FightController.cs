using System;
using System.Collections;
using System.Collections.Generic;
using Cards.Fighting;
using Patterns;
using Team;
using TMPro.EditorUtilities;
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

        public UICardSlot SelectedUnit
        {
            get => selectedUnit;
        }
        
        public UnitController[] Fighters = new UnitController[2];
        [SerializeField] private UICardSlot[] topSide;
        [SerializeField] private UICardSlot[] downSide;
        [SerializeField] private GameObject battleMap;

        private int tour = 0;
        private BasicCard selectedCard;
        private UICardSlot selectedUnit;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                UnitController[] uc = FindObjectsOfType<UnitController>();
                StartFight(uc[0],uc[1]);
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
                selectedUnit = unitCard;
                selectedCard = null;
            }
            else
            {
                if (selectedUnit != null)
                {
                    selectedUnit.UnitCard.Attack(unitCard);
                }
            }
        }
        
        public void SelectCard(BasicCard basicCard)
        {
            selectedCard = basicCard;
            selectedUnit = null;
        }

        public void DeselectCard()
        {
            selectedCard = null;
            selectedUnit = null;
        }
        
        public void UsedCard()
        {
            DeselectCard();
            NextTour();
        }

        public void NextTour()
        {
            CheckCards();
            selectedCard = null;
            tour++;
            if (tour >= Fighters.Length)
                tour = 0;
        }

        private void CheckCards()
        {
            int topSideArmy = 0;
            for (int i = 0; i < topSide.Length; i++)
            {
                if (topSide[i].UnitCard != null)
                {
                    topSideArmy++;
                }
            }

            for (int i = 0; i < Fighters[1].Cards.Count; i++)
            {
                if (Fighters[1].Cards[i].basicCard.ActiveTypeCard == ActiveTypeCard.map)
                {
                    topSideArmy++;
                }
            }
            
            int downSideArmy = 0;
            for (int i = 0; i < downSide.Length; i++)
            {
                if (downSide[i].UnitCard != null)
                {
                    downSideArmy++;
                }
            }

            for (int i = 0; i < Fighters[0].Cards.Count; i++)
            {
                if (Fighters[0].Cards[i].basicCard.ActiveTypeCard == ActiveTypeCard.map)
                {
                    downSideArmy++;
                }
            }
            
            if (topSideArmy <= 0)
            {
                Fighters[1].LostFight();
                FightEnd();
            }

            if (downSideArmy <= 0)
            {
                Fighters[0].LostFight();
                FightEnd();
            }
        }

        private void FightEnd()
        {
            foreach (var VARIABLE in topSide)
            {
                VARIABLE.RemoveUnitCard();
            }
            foreach (var VARIABLE in downSide)
            {
                VARIABLE.RemoveUnitCard();
            }

            tour = 0;
            selectedCard = null;
            selectedUnit = null;
            battleMap.SetActive(false);
        }
    }
}