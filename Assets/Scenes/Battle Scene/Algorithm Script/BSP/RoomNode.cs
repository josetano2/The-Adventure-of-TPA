using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomNode : BSPNode
{
    public RoomNode(Vector2Int bottomLeftAreaCorner, Vector2Int topRightAreaCorner, BSPNode parentNode, int index) : base(parentNode)
    {
        this.bottomLeftAreaCorner = bottomLeftAreaCorner;
        this.topRightAreaCorner = topRightAreaCorner;
        this.bottomRightAreaCorner = new Vector2Int(topRightAreaCorner.x, bottomLeftAreaCorner.y);
        this.topLeftAreaCorner = new Vector2Int(bottomLeftAreaCorner.x, topRightAreaCorner.y);
        this.treeLayerIndex = index;
    }

    public int Width
    {
        get => (int)(topRightAreaCorner.x - bottomLeftAreaCorner.x);
    }

    public int Length
    {
        get => (int)(topRightAreaCorner.y - bottomLeftAreaCorner.y);
    }
}
