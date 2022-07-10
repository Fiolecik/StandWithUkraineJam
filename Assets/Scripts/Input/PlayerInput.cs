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
        [SerializeField] private GameObject target;

        private Vector2Int lastTarget = Vector2Int.zero;

        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (TourController.Instance.CPU)
                return;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;

            if (Physics.Raycast(r, out rh))
            {
                if (target != null)
                    target.transform.position = Grid.Instance
                        .NodeFromWorldPoint(rh.point).worldPosition;
            }


            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(r, out rh))
                {
                    Normal(rh);
                }
            }
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