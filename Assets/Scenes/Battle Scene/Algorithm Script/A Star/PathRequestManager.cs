using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PathRequestManager : MonoBehaviour
{
    Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
    PathRequest currPathRequest;

    static PathRequestManager instance;
    Pathfinding pathfinding;
    bool isProcessingPath;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }
    public static void RequestPath(Vector3 pathStart, Vector3 pathEnd, Action<Vector3[], bool> callback)
    {
        PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
        instance.pathRequestQueue.Enqueue(newRequest);
        instance.tryProcessNext();
    }

    void tryProcessNext()
    {
        if(!isProcessingPath && pathRequestQueue.Count > 0)
        {
            currPathRequest = pathRequestQueue.Dequeue();
            isProcessingPath = true;
            pathfinding.startFindPath(currPathRequest.pathStart, currPathRequest.pathEnd);
        }
    }

    public void finishedProcessingPath(Vector3[] path, bool success)
    {
        currPathRequest.callback(path, success);
        isProcessingPath = false;
        tryProcessNext();
    }

    struct PathRequest
    {
        public Vector3 pathStart;
        public Vector3 pathEnd;
        public Action<Vector3[], bool> callback;

        public PathRequest(Vector3 Start, Vector3 End, Action<Vector3[], bool> Callback)
        {
            pathStart = Start;
            pathEnd = End;
            callback = Callback;
        }
    }
}
