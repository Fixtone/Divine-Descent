﻿using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PathRequest
{
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public Action<Vector3[], bool> callback;

    public PathRequest(Vector3 start, Vector3 end, Action<Vector3[], bool> _callback)
    {
        pathStart = start;
        pathEnd = end;
        callback = _callback;
    }
}

public struct PathResult
{
    public Vector3[] path;
    public bool success;
    public Action<Vector3[], bool> callback;

    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }
}

public class PathRequestManager : MonoBehaviour
{
    Queue<PathResult> results = new Queue<PathResult>();
    static PathRequestManager instance;
    Pathfinding pathFinding;

    void Awake()
    {
        instance = this;
        pathFinding = GetComponent<Pathfinding>();
    }

    void Update()
    {
        if(results.Count > 0)
        {
            int itemsInQueue = results.Count;

            lock(results)
            {
                for(int i=0; i< itemsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.success);
                }
            }
        }
    }

    /// <summary>
    /// Starts a thread to request a path
    /// </summary>
    /// <param name="request"></param>
    public static void RequestPath(PathRequest request)
    {
        ThreadStart threadStart = delegate
        {
            instance.pathFinding.FindPath(request, instance.FinishedProcessingPath);
        };

        threadStart.Invoke();
    }

    /// <summary>
    /// Called when a path has finished being processed
    /// </summary>
    /// <param name="result"></param>
    public void FinishedProcessingPath(PathResult result)
    {
        lock (results)
        {
            results.Enqueue(result);
        }
    }

}