using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

public class GridObstacle : MonoBehaviour
{
    [SerializeField] private Vector2Int midPoint;
    [SerializeField] private Vector2Int size;
    private void Awake()
    {
        GridController.Instance.AddObstacle(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(midPoint.x, midPoint.y, 0), new Vector3(size.x+1, size.y+1));
    }

    public Vector2Int[] GetPoints()
    {
        List<Vector2Int> points = new List<Vector2Int>();
        for (int i = -size.x; i < size.x; i++)
        {
            for (int j = -size.y; j < size.y; j++)
            {
                points.Add(new Vector2Int(i,j));
            }
        }

        return points.ToArray();
    }
}
