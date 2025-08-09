using System.Collections.Generic;
using UnityEngine;

public class Node
{
    // Public
    public bool walkable;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public int fCost
    {
        get { return gCost + hCost; }
    }

    public Node(bool _wakable, int _gridX, int _gridY)
    {
        walkable = _wakable;
        gridX = _gridX;
        gridY = _gridY;
    }


}
