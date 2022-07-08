using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemplateInput : MonoBehaviour
{
    [SerializeField] private EntityController entityController;

    private Vector2Int lastTarget = Vector2Int.zero;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    private Vector2Int CalculateTarget()
    {
        Vector2 worldPosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector2.one/2;
        Vector2Int target = new Vector2Int((int)worldPosition.x, (int)worldPosition.y);
        lastTarget = target;
        return target;
    }
}
