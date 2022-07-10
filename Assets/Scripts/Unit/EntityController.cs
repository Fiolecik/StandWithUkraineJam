using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Game;
using Interfaces;
using UnityEngine;

public class EntityController : MonoBehaviour, ITourListener
{
    [SerializeField] private int maxMoves;
    
    private int currentMoves;
    private int currentPosition = 0;

    private List<Node> currentRoad = new List<Node>();
    private Vector3 currentTarget;
    private bool breakMoving = false;
    private bool coroutineIsRun = false;
    private LineRenderer lineRenderer;
    private void Awake()
    {
        currentMoves = maxMoves;
        TourController.Instance.AddTourListener(this);
        lineRenderer = GetComponent<LineRenderer>();
    }
    
    public void SetCurrentTarget(Vector3 position)
    {
        if (coroutineIsRun)
        {
            breakMoving = true;
            currentTarget = position;
            return;
        }

        if (currentRoad == null)
        {
            currentRoad = new List<Node>();
        }
        
        if (currentRoad.Count == 0 || currentRoad[currentRoad.Count - 1].worldPosition != position)
        {
            currentTarget = position;
            Pathfinding.Instance.FindPath(transform.position, currentTarget);
            currentRoad = Grid.Instance.path;
            AddPointsToLine();
            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.red;
        }
        else
        {
            StartCoroutine(StartMove());
            lineRenderer.startColor = Color.green;
            lineRenderer.endColor = Color.green;
        }
    }

    public void RemoveCurrentTarget()
    {
        breakMoving = true;
        currentRoad.Clear();
    }

    public Node[] GetRoad()
    {
        return currentRoad.ToArray();
    }
    
    private void ClearRoad()
    {
        currentRoad.Clear();
        if(!coroutineIsRun)
            currentPosition = 0;
    }

    private void AddPointsToLine()
    {
        lineRenderer.positionCount = currentRoad.Count;
        for (int i = 0; i < currentRoad.Count; i++)
        {
            lineRenderer.SetPosition(i, currentRoad[i].worldPosition);
        }


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
            yield return new WaitForSeconds(0.1f);
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
            delta += Time.deltaTime*2;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
    }

    public void OnTourEnd()
    {
        currentMoves = maxMoves;
    }
}
