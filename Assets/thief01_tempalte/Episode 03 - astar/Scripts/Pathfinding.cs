using System;
using System.Collections.Generic;
using Patterns;
using UnityEngine;

public class Pathfinding : SingletonMonoBehaviour<Pathfinding>
{
    private Grid grid;

    public Transform t1, t2;

    private void Awake()
    {
        grid = GetComponent<Grid>();
    }

    // private void Update()
    // {
    //     FindPath(t1.position, t2.position);
    // }

    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {
        var startNode = grid.NodeFromWorldPoint(startPos);
        var targetNode = grid.NodeFromWorldPoint(targetPos);

        var openSet = new List<Node>();
        var closedSet = new HashSet<Node>();
        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            var node = openSet[0];
            for (var i = 1; i < openSet.Count; i++)
                if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
                    if (openSet[i].hCost < node.hCost)
                        node = openSet[i];

            openSet.Remove(node);
            closedSet.Add(node);

            if (node == targetNode) RetracePath(startNode, targetNode);

            foreach (var neighbour in grid.GetNeighbours(node))
            {
                if (!neighbour.walkable || closedSet.Contains(neighbour)) continue;

                var newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
                if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = node;

                    if (!openSet.Contains(neighbour))
                        openSet.Add(neighbour);
                }
            }
        }
    }


    public void RetracePath(Node startNode, Node endNode)
    {
        var path = new List<Node>();
        var currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        grid.path = path;
    }

    private int GetDistance(Node nodeA, Node nodeB)
    {
        var dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        var dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}