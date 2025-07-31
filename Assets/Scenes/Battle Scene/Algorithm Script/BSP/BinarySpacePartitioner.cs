using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BinarySpacePartitioner : MonoBehaviour
{
    RoomNode rootNode;
    private int gridWidth;
    private int gridLength;

    public RoomNode RootNode
    {
        get => rootNode;
    }

    public BinarySpacePartitioner(int GridWidth, int GridLength)
    {
        rootNode = new RoomNode(new Vector2Int(0, 0), new Vector2Int(gridWidth, gridLength), null, 0);
    }

    public List<RoomNode> PrepareNodesCollection(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        Queue<RoomNode> graph = new Queue<RoomNode>();
        List<RoomNode> listToReturn = new List<RoomNode>();
        graph.Enqueue(rootNode);
        listToReturn.Add(RootNode);
        int iterations = 0;
        while(iterations < maxIterations && graph.Count > 0)
        {
            iterations++;
            RoomNode currNode = graph.Dequeue();
            if(currNode.Width >= roomWidthMin * 2 || currNode.Length >= roomLengthMin * 2)
            {
                SplitTheSpace(currNode, listToReturn, roomLengthMin, roomWidthMin, graph);
            }
        }
        return listToReturn;
    }

    public void SplitTheSpace(RoomNode currNode, List<RoomNode> listToReturn, int roomLengthMin, int roomWidthMin, Queue<RoomNode> graph)
    {
        Line line = GetLineDividingSpace(currNode.bottomLeftAreaCorner, currNode.topRightAreaCorner, roomWidthMin, roomLengthMin);
        RoomNode node1;
        RoomNode node2;
        if(line.Orientation == Orientation.horizontal)
        {
            node1 = new RoomNode(currNode.bottomLeftAreaCorner, new Vector2Int(currNode.topRightAreaCorner.x, line.Coordinates.y), currNode, currNode.treeLayerIndex + 1);
            node2 = new RoomNode(new Vector2Int(currNode.bottomLeftAreaCorner.x, line.Coordinates.y), currNode.topRightAreaCorner, currNode, currNode.treeLayerIndex + 1);
        }
        else
        {
            node1 = new RoomNode(currNode.bottomLeftAreaCorner, new Vector2Int(line.Coordinates.x, currNode.topRightAreaCorner.y), currNode, currNode.treeLayerIndex + 1);
            node2 = new RoomNode(new Vector2Int(line.Coordinates.x, currNode.bottomLeftAreaCorner.y), currNode.topRightAreaCorner, currNode, currNode.treeLayerIndex + 1);
        }
        AddNewNodeToCollections(listToReturn, graph, node1);
        AddNewNodeToCollections(listToReturn, graph, node2);
    }

    private void AddNewNodeToCollections(List<RoomNode> listToReturn, Queue<RoomNode> graph, RoomNode node)
    {
        listToReturn.Add(node);
        graph.Enqueue(node);
    }

    public Line GetLineDividingSpace(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        Orientation orientation;
        bool lengthStatus = (topRightAreaCorner.y - bottomLeftAreaCorner.y) >= 2 * roomWidthMin;
        bool widthStatus = (topRightAreaCorner.x - bottomLeftAreaCorner.x) >= 2 * roomLengthMin;
        if(lengthStatus && widthStatus)
        {
            orientation = (Orientation)(Random.Range(0, 2));
        }
        else if (widthStatus)
        {
            orientation = Orientation.vertical;
        }
        else
        {
            orientation = Orientation.horizontal;
        }
        return new Line(orientation, GetCoordinatesForOrientation(orientation, bottomLeftAreaCorner, topRightAreaCorner, roomWidthMin, roomLengthMin));
    }

    public Vector2Int GetCoordinatesForOrientation(Orientation orientation, Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, int roomWidthMin, int roomLengthMin)
    {
        Vector2Int coordinates = Vector2Int.zero;
        if(orientation == Orientation.horizontal)
        {
            coordinates = new Vector2Int(0, Random.Range((bottomLeftAreaCorner.y + roomLengthMin), (topRightAreaCorner.y - roomLengthMin)));

        }
        else
        {
            coordinates = new Vector2Int(Random.Range((bottomLeftAreaCorner.x + roomWidthMin), (topRightAreaCorner.x - roomWidthMin)), 0);
        }
        return coordinates;
    }

}
