using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGenerator
{
    
    List<RoomNode> allSpaceNodes = new List<RoomNode>();

    private int gridWidth;
    private int gridLength;

    public RoomGenerator(int GridWith, int GridLength)
    {
        gridWidth = GridWith;
        gridLength = GridLength;
    }

    public List<BSPNode> CalculateRooms(int maxIterations, int roomWidthMin, int roomLengthMin)
    {
        BinarySpacePartitioner bsp = new BinarySpacePartitioner(gridWidth, gridLength);
        allSpaceNodes = bsp.PrepareNodesCollection(maxIterations, roomWidthMin, roomLengthMin);
        return new List<BSPNode>(allSpaceNodes);
    }

}
