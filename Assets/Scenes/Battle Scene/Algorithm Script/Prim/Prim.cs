using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prim : MonoBehaviour
{
    private List<Vector2Int> maze;
    private int[,] visited;
    private char[,] map;
    public char[,] Map
    {
        get { return map; }
        set { map = value; }
    }
    [SerializeField] private int height;
    [SerializeField] private int width;
    [SerializeField] private int wallThickness;
    [SerializeField] private float cellSize;
    private int[] xDirection = { 0, 2, 0, -2 };
    private int[] yDirection = { -2, 0, 2, 0 };

    void Awake()
    {
        maze = new List<Vector2Int>();
        visited = new int[width, height];
        map = new char[width, height];

        generateMazePrim();
    }

    public void generateMazePrim()
    {
        Vector2Int startPos = new Vector2Int(0, 0);
        maze.Add(startPos);

        int first = 1;
        int count = 0;

        while (maze.Count > 0)
        {
            Vector2Int currPos = maze[0];
            maze.RemoveAt(0);

            if (visited[currPos.x, currPos.y] == 1)
                continue;

            visited[currPos.x, currPos.y] = 1;
            map[currPos.x, currPos.y] = ' ';

            int randomNeighbor;
            bool valid = true;

            if (first == 1)
            {
                first = 0;
            }
            else
            {
                do
                {
                    randomNeighbor = Random.Range(0, 4);
                    valid = currPos.x - xDirection[randomNeighbor] >= 0 && currPos.x - xDirection[randomNeighbor] < width && currPos.y - yDirection[randomNeighbor] >= 0 && currPos.y - yDirection[randomNeighbor] < height && map[currPos.x - xDirection[randomNeighbor], currPos.y - yDirection[randomNeighbor]] == ' ';
                } while (!valid);

                map[currPos.x - (xDirection[randomNeighbor] / 2), currPos.y - (yDirection[randomNeighbor] / 2)] = ' ';
            }

            for (int i = 0; i < 4; i++)
            {
                if (currPos.x - xDirection[i] >= 0 && currPos.x - xDirection[i] < width && currPos.y - yDirection[i] >= 0 && currPos.y - yDirection[i] < height)
                {
                    Vector2Int neighbor = new Vector2Int(currPos.x - xDirection[i], currPos.y - yDirection[i]);
                    maze.Add(neighbor);
                    count++;
                }
            }
        }

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                {
                    map[i, j] = '#';
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        if (map != null)
        { 
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    if (map[i, j] == '#')
                    {
                        Gizmos.color = Color.black;
                        Gizmos.DrawCube(new Vector3(i * cellSize, 10, j * cellSize), new Vector3(cellSize, wallThickness, cellSize));
                    }
                    else if (map[i, j] == ' ')
                    {
                        Gizmos.color = Color.white;
                        Gizmos.DrawCube(new Vector3(i * cellSize, 10, j * cellSize), new Vector3(cellSize, wallThickness, cellSize));
                    }
                }
            }
        }
    }
}