using System.Collections;
using System.Collections.Generic;
using Fighting;
using Game;
using Other;
using Unit;
using UnityEngine;

namespace UI
{
    public class UIActionsController : MonoBehaviour
    {
        [SerializeField] private GameObject changeResourceButton;

        [SerializeField] private GameObject attackButton;

        [SerializeField] private GameObject takeOverControllButton;


        private EntityActions last;
        // Update is called once per frame
        void Update()
        {
            if (TourController.Instance.CurrentMoving == null)
                return;
            last = TourController.Instance.CurrentMoving.SpawnedUnit
                .GetComponent<EntityActions>();
            attackButton.SetActive(last.IsPossibleAttack);
            takeOverControllButton.SetActive(last.IsPossibleTakeOverStruct);
            changeResourceButton.SetActive(last.IsPossibleChangeResource);
        }

        public void Attack()
        {
            last.Attack();
        }

        public void TakeOverControll()
        {
            last.TakeOverControll();
        }

        public void ChangeResource(int i)
        {
            last.SetResourceAtStruct((PlayerResource)i+2);
        }
    }
}