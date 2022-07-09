using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TemplateInput : MonoBehaviour
{
    [SerializeField] private EntityController entityController;

    private Vector2Int lastTarget = Vector2Int.zero;

    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rh;
            if (Physics.Raycast(r, out rh))
            {
                entityController.SetCurrentTarget(Grid.Instance
                    .NodeFromWorldPoint(rh.point).worldPosition);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(new Vector3(lastTarget.x,lastTarget.y,0), 0.1f);
    }
}
