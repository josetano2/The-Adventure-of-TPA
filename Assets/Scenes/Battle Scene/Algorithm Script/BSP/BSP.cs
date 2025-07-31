using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSP : MonoBehaviour
{
    private List<BoundsInt> rooms;
    public List<BoundsInt> Rooms
    {
        get { return rooms; }
        set { rooms = value; }
    }

    void Awake()
    {
        BoundsInt terrainBounds = new BoundsInt(0, 0, 0, 111, 0, 111);
        rooms = binarySpacePartitioning(terrainBounds, 10, 10);
    }
    public List<BoundsInt> binarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.x >= minWidth && room.size.z >= minHeight)
            {
                if (UnityEngine.Random.value > 0.5f)
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        splitHorizontally(roomsQueue, room);
                    }
                    else if (room.size.z >= minHeight * 2)
                    {
                        splitVertically(roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    } 
                }
                else
                {
                    if (room.size.z >= minHeight * 2)
                    {
                        splitVertically(roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        splitHorizontally(roomsQueue, room);
                    }
                    else
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    public void splitHorizontally(Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = UnityEngine.Random.Range(1, room.size.x);
        BoundsInt roomLeft = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt roomRight = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z), new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(roomLeft);
        roomsQueue.Enqueue(roomRight);
    }

    public void splitVertically(Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var zSplit = UnityEngine.Random.Range(1, room.size.z);
        BoundsInt roomTop = new BoundsInt(room.min, new Vector3Int(room.size.x, room.size.y, zSplit));
        BoundsInt roomBottom = new BoundsInt(new Vector3Int(room.min.x, room.min.y, room.min.z + zSplit), new Vector3Int(room.size.x, room.size.y, room.size.z - zSplit));
        roomsQueue.Enqueue(roomTop);
        roomsQueue.Enqueue(roomBottom);
    }

    void OnDrawGizmos()
    {
        if (rooms != null)
        {
            foreach (var room in rooms)
            {
                Gizmos.color = Color.magenta;
                float heightOffset = 11f;
                Vector3 position = new Vector3(room.center.x, heightOffset, room.center.z);
                Vector3 size = new Vector3(room.size.x, 1, room.size.z);
                Gizmos.DrawCube(position, size);
            }
        }
    }
}