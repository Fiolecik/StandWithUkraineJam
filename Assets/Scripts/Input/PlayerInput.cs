using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using Team;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameInput
{
    public class PlayerInput : MonoBehaviour
    {

        private Vector2Int lastTarget = Vector2Int.zero;

        void Update()
        {
            if (EventSystem.current.currentSelectedGameObject != null)
                return;
            if (TourController.Instance.CPU)
                return;
            if (TourController.Instance.CurrentMoving.SelectedCard)
            {
                // highlighting unit and base
            }
            else
            {
                
            }
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;

            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(r, out rh))
                {
                    Normal(rh);
                }
            }
        }

        private void SelectedCard(RaycastHit rh)
        {
            
        }

        private void Normal(RaycastHit rh)
        {
            TourController.Instance.CurrentMoving.EntityController.SetCurrentTarget(Grid.Instance
                .NodeFromWorldPoint(rh.point).worldPosition);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawSphere(new Vector3(lastTarget.x, lastTarget.y, 0), 0.1f);
        }
    }
}