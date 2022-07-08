using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] private int maxMoves;
    
    private int currentMoves;
    private int currentPosition = 0;

    private List<Node> currentRoad = new List<Node>();
    private Vector3 currentTarget;
    private bool breakMoving = false;
    private bool coroutineIsRun = false;
    private void Awake()
    {
        currentMoves = maxMoves;
    }

    private void OnDrawGizmos()
    {
        Vector2 position = transform.position;
        // for (int i = currentPosition; i < currentRoad.Count; i++)
        // {
        //     Gizmos.DrawLine(position, currentRoad[i]);
        //     position = currentRoad[i];
        // }
    }

    public void NewTour()
    {
        currentMoves = maxMoves;
    }

    public void SetCurrentTarget(Vector3 position)
    {
        if (coroutineIsRun)
        {
            breakMoving = true;
            currentTarget = position;
            return;
        }
        
        if (currentRoad.Count == 0 || currentRoad[currentRoad.Count - 1].worldPosition != position)
        {
            
            currentTarget = position;
            Pathfinding.Instance.FindPath(transform.position, currentTarget);
            currentRoad = Grid.Instance.path;

        }
        else
        {
            Debug.Log("XD");
            StartCoroutine(StartMove());
        }
    }

    public void RemoveCurrentTarget()
    {
        breakMoving = true;
        currentRoad.Clear();
    }

    // public Vector2[] GetRoad()
    // {
    //     return currentRoad.ToArray();
    // }

    // private void CalculateRoad()
    // {
    //     ClearRoad();
    //     Vector2Int currentPosition = new Vector2Int((int)transform.position.x, (int)transform.position.y)-new Vector2Int(1, 0);
    //
    //     while (currentPosition!= currentTarget)
    //     {
    //         Vector2Int dir = (currentTarget - currentPosition);
    //         dir = new Vector2Int(Mathf.Clamp(dir.x, -1, 1), Mathf.Clamp(dir.y, -1, 1));
    //         currentPosition += dir;
    //         currentRoad.Add(currentPosition);
    //     }
    // }

    private void ClearRoad()
    {
        currentRoad.Clear();
        if(!coroutineIsRun)
            currentPosition = 0;
    }

    private IEnumerator StartMove()
    {
        if (coroutineIsRun)
        {
            breakMoving = true;
            yield break;
        }

        coroutineIsRun = true;
        int moveIndex = 0;
        List<Node> currentRoad = this.currentRoad;
        while (currentMoves>0 && moveIndex<currentRoad.Count)
        {
            yield return new WaitForSeconds(0.3f);
            yield return StartCoroutine(DoMove(currentRoad[moveIndex].worldPosition));
            moveIndex++;
            currentPosition = moveIndex;
            currentMoves--;
            if (breakMoving)
                break;
        }

        if (moveIndex >= currentRoad.Count)
        {
            ClearRoad();
        }

        if (breakMoving)
        {
            Pathfinding.Instance.FindPath(transform.position, currentTarget);
        }

        breakMoving = false;
        coroutineIsRun = false;
        currentPosition = 0;
    }

    private IEnumerator DoMove(Vector3 moveToPosition)
    {
        float delta = 0;
        Vector3 startPosition = transform.position;
        while (delta<=1)
        {
            transform.position = Vector3.Lerp(startPosition, moveToPosition, delta);
            delta += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
    }
}
