using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BSPNode
{
    private List<BSPNode> childrenNodeList;
    public List<BSPNode> ChildrenNodeList
    {
        get { return childrenNodeList; }
    }

    public bool visited;
    public Vector2Int bottomLeftAreaCorner;
    public Vector2Int bottomRightAreaCorner;
    public Vector2Int topRightAreaCorner;
    public Vector2Int topLeftAreaCorner;
    public BSPNode parent;
    public int treeLayerIndex;

    public BSPNode(BSPNode parentNode)
    {
        childrenNodeList = new List<BSPNode>();
        parent = parentNode;
        if(parentNode != null)
        {
            parentNode.AddChild(this);
        }
    }
    public void AddChild(BSPNode node)
    {
        childrenNodeList.Add(node);
    }

    public void RemoveChild(BSPNode node)
    {
        childrenNodeList.Remove(node);
    }

}
