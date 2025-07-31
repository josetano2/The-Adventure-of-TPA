using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grids : MonoBehaviour
{
    public bool displayGridGizmoz;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public Transform player1;
    public Transform player2;
    public Transform player3;
    public float nodeRadius;
    Node[,] grid;

    // seberapa banyak grid yang muat
    float nodeDiameter;
    int gridSizeX;
    int gridSizeY;

    public bool onlyDisplayPathGizmoz;
    void Awake()
    {
        nodeDiameter = nodeRadius * 2;
        // return jumlah grid yang muat
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        Invoke("createGrid", 0.5f);
        //createGrid();
    }

    public int MaxSize
    {
        get { return gridSizeX * gridSizeY; }
    }

    void createGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for(int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public List<Node> getNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();
        
        for(int x = -1; x <= 1; x++)
        {
            for(int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    // buat biar klo ad yang g jelas, diroundoff
    public Node nodeFromWorldPoint(Vector3 worldPosition)
    {
        Vector3 localPosition = worldPosition - transform.position;
        float percentX = Mathf.Clamp01(localPosition.x / gridWorldSize.x + 0.5f);
        float percentY = Mathf.Clamp01(localPosition.z / gridWorldSize.y + 0.5f);

        int x = Mathf.FloorToInt(Mathf.Clamp01(percentX) * (gridSizeX - 1));
        int y = Mathf.FloorToInt(Mathf.Clamp01(percentY) * (gridSizeY - 1));
        return grid[x, y];
    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if(grid != null && displayGridGizmoz)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
