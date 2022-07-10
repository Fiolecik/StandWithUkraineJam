using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Cards.Fighting;
using Fighting;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UICardSlot : MonoBehaviour
    {
        public bool DistanceUnit
        {
            get => distanceUnit;
        }

        public int Team
        {
            get => team;
        }

        public UnitCard UnitCard
        {
            get => unitCard;
        }
        [SerializeField] private bool distanceUnit;
        [SerializeField] private int team;
        [SerializeField] private Image cardIcon;

        private UnitCard unitCard;

        public void SetUnitCard(UnitCard unitCard)
        {
            if (!IsPossibleSetUnitCard())
                return;
            this.unitCard = Instantiate(unitCard);
            cardIcon.sprite = unitCard.sprite;
            cardIcon.gameObject.SetActive(true);
        }

        public void RemoveUnitCard()
        {
            unitCard = null;
            cardIcon.sprite = null;
            cardIcon.gameObject.SetActive(false);
        }

        public bool IsPossibleSetUnitCard()
        {
            return unitCard == null && team == FightController.Instance.Tour;
        }

        public void ClickedAtSlot()
        {
            if (unitCard == null)
            {
                if (FightController.Instance.SelectedCard != null)
                {
                    FightController.Instance.SelectedCard.CastCard(this);
                }
            }
            else
            {
                FightController.Instance.SelectUnit(team, this);
            }
        }
    }
}