using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Node
{
    // Public
    public bool walkable;  // To represent that the grid is walkable or not
    public int gridX; // grid X val
    public int gridY;  // grid Y Val

    public int gCost; // GCost
    public int hCost;  // hCost
    public Node parent;  // Parent Node

    public int fCost
    {
        // Get and return the f = g + h
        get { return gCost + hCost; }
    }

    // Creating constructor and intializing it.
    public Node(bool _wakable, int _gridX, int _gridY)
    {
        walkable = _wakable;
        gridX = _gridX;
        gridY = _gridY;
    }


}
