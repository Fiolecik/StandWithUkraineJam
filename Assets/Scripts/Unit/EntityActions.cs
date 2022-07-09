using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Other;
using Team;
using UnityEngine;
using World;

namespace Unit
{
    public class EntityActions : MonoBehaviour
    {
        public bool IsPossibleAttack { get; private set; }
        public bool IsPossibleTakeOverStruct { get; private set; }
        public  bool IsPossibleChangeResource { get; private set; }
        
        [SerializeField] private LayerMask structMask;
        [SerializeField] private LayerMask entityMask;
        
        [SerializeField] private float attackRange = 1;

        [SerializeField] private float interactionWithStructRange = 5f;

        private UnitController unitController;

        private UnitController atackTarget;
        private ChapelController chapelController;

        private void Awake()
        {
            unitController = GetComponent<UnitController>();
        }
        
        void Update()
        {
            if (TourController.Instance.CurrentMoving == unitController.TeamParrent)
            {
                FindInteractions();
            }
            else
            {
                ResetPossibles();
            }
        }

        private void FindInteractions()
        {
            ResetPossibles();
            Collider[] c = Physics.OverlapSphere(transform.position, attackRange, entityMask);

            foreach (var VARIABLE in c)
            {
                var uc = VARIABLE.GetComponent<UnitController>();
                if (uc.TeamParrent != unitController.TeamParrent)
                {
                    atackTarget = uc;
                    IsPossibleAttack = true;
                    break;
                }
            }
            c = Physics.OverlapSphere(transform.position, interactionWithStructRange, structMask);

            if (c != null && c.Length>0)
            {
                float distance = Vector3.Distance(transform.position, c[0].transform.position);
                int id = 0;
                for (int i = 1; i < c.Length; i++)
                {
                    float tempDis = Vector3.Distance(c[i].transform.position, transform.position);
                    if (tempDis < distance)
                    {
                        distance = tempDis;
                        id = i;
                    }
                }

                ChapelController chapelController = c[id].GetComponent<ChapelController>();
                this.chapelController = chapelController;
                IsPossibleTakeOverStruct = chapelController.IsPossibleToTakeOverControll(unitController.TeamParrent);
                IsPossibleChangeResource = chapelController.IsPossibleToSetSpecialResource();

            }
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = IsPossibleAttack ? Color.green : Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
            Gizmos.color = IsPossibleTakeOverStruct ? Color.blue : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionWithStructRange);
        }
        
        private void ResetPossibles()
        {
            IsPossibleAttack = false;
            IsPossibleChangeResource = false;
            IsPossibleTakeOverStruct = false;
        }

        public void Attack()
        {
            
        }

        public void TakeOverControll()
        {
            if (IsPossibleTakeOverStruct && chapelController != null)
            {
                chapelController.TakeOverControll(unitController.TeamParrent);
            }
            
        }

        public void SetResourceAtStruct(PlayerResource playerResource)
        {
            if (IsPossibleChangeResource && chapelController != null)
            {
                chapelController.SetSpecialResource(playerResource);
            }
        }
    }
}