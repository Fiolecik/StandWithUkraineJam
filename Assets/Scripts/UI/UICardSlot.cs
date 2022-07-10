using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Cards.Fighting;
using Fighting;
using TMPro;
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
        [SerializeField] private Color possible = Color.green;
        [SerializeField] private Color normal = Color.grey;
        [SerializeField] private Color cant = Color.red;
        [SerializeField] private Color selected = Color.yellow;
        [SerializeField] private TMP_Text hp;
        [SerializeField] private TMP_Text def;
        [SerializeField] private TMP_Text attack;
        private Image feedback;

        private UnitCard unitCard;

        private void Awake()
        {
            feedback = GetComponent<Image>();
        }

        private void Update()
        {
            ColorSlot();
            ColorSlotFromUnit();
            if (unitCard!=null)
            {
                hp.text = unitCard.Statistics.heal.ToString();
                attack.text = unitCard.Statistics.damage.ToString();
                def.text = unitCard.Statistics.def.ToString();
            }
        }

        private void ColorSlot()
        {
            feedback.color = normal;
            cardIcon.color = normal;
            if (FightController.Instance.SelectedCard == null)
                return;
            Image target = feedback;
            if (cardIcon.gameObject.activeInHierarchy)
            {
                target = cardIcon;
            }
            if (FightController.Instance.Tour != team)
            {
                target.color = cant;
                return;
            }
            
            // karta atak i wrog team
            // karta atak i swoj team
            // karta buff i soj team
            // karta buff i wrog team
            // karta jednostka i swoj team pust epole
            // karta jednostka i wrog team
            // karta jednostka i swoj team pelne pole
            

            var currentCard = FightController.Instance.SelectedCard;
            if (currentCard.ActiveTypeCard != ActiveTypeCard.map && unitCard==null)
            {
                return;
            }
            
            if (currentCard.ActiveTypeCard == ActiveTypeCard.enemyUnit && team != FightController.Instance.Tour && unitCard!=null)
            {
                target.color = possible;
                return;
            }
            if (currentCard.ActiveTypeCard == ActiveTypeCard.enemyUnit && team == FightController.Instance.Tour)
            {
                target.color = cant;
                return;
            }
            
            if (currentCard.ActiveTypeCard == ActiveTypeCard.friendUnit && team != FightController.Instance.Tour)
            {
                target.color = cant;
                return;
            }
            if (currentCard.ActiveTypeCard == ActiveTypeCard.friendUnit && team == FightController.Instance.Tour && unitCard!=null)
            {
                target.color = possible;
                return;
            }

            if (currentCard.ActiveTypeCard == ActiveTypeCard.map && team == FightController.Instance.Tour)
            {
                target.color = unitCard == null ? possible : cant;
            }
        }

        private void ColorSlotFromUnit()
        {
            cardIcon.color = normal;
            if (FightController.Instance.SelectedUnit == null)
                return;
            if (FightController.Instance.SelectedUnit == this)
            {
                cardIcon.color = selected;
                return;
            }
            
            if (!cardIcon.gameObject.activeSelf)
            {
                return;
            }
            if (FightController.Instance.Tour != team)
            {
                cardIcon.color = possible;
                return;
            }
            
            cardIcon.color = cant;
        }

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
            if (FightController.Instance.SelectedCard != null)
            {
                FightController.Instance.SelectedCard.CastCard(this);
            }
            else
            {
                if (unitCard != null)
                {
                    FightController.Instance.SelectUnit(team,this);
                }
            }
        }
    }
}