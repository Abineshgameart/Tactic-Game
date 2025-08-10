using System.Collections.Generic;
using UnityEngine;

public class PathfindingScript : MonoBehaviour
{
    // Public
    public static PathfindingScript instance;  // MAking a Static Instance

    //Private
    [SerializeField] private GridGenerator gridGenerator;  // Grid Generator Script

    private void Awake()
    {
        instance = this;
    }

    
    // A* Algorithm for Pathfinding
    public List<Node> AStarGeneratePath(Node start, Node end)
    {
        List<Node> openList = new List<Node>();  // Created open List
        HashSet<Node> closeList = new HashSet<Node>();  // Created CloseList
        
        openList.Add(start);   // Add the start on the OpenList

        // Conditioning by openList Count
        while(openList.Count > 0)
        {
            Node current = openList[0];

            // Finding Lowest fCost
            for(int i = 1; i < openList.Count; i++)
            {
                // And assigning it to current
                if (openList[i].fCost < current.fCost || (openList[i].fCost == current.fCost && openList[i].hCost < current.hCost)) {
                    current = openList[i];
                }
            }

            openList.Remove(current);  // Remove cuurent from openLsit 
            closeList.Add(current);  // Add current to the closeList

            // If cuurrent and end are same the start the RetracePath
            if(current == end)
            {
                return RetracePath(start, end);
            }
            
            // Calculatin and finding path on the neighbours
            foreach(Node neighbor in GetNeighbors(current))
            {
                // If the neighbor is not walkable or close list contains the neighbour, the omit current process and continue
                if(!neighbor.walkable || closeList.Contains(neighbor))
                {
                    continue;
                }

                // calculating cost to neighbors
                int newCostToNeighbor = current.gCost + GetDistance(current, neighbor);

                // Calculaing cost and assigning Parents
                if(newCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor; // assigning gCost
                    neighbor.hCost = GetDistance(neighbor, end);  // assigning hCost
                    neighbor.parent = current; // assigning Parent

                    // add neighbor to openlist if it doesn't have
                    if(!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;  // returning null
    }


    // Method for RetracePath
    List<Node> RetracePath(Node start, Node end)
    {
        // Creating empty path
        List<Node> path = new List<Node>();

        Node currentNode = end; // Assigning current Node to end

        // getting on reverse through the start
        while (currentNode != start)
        {
            path.Add(currentNode);  // Adding currentNode to path
            currentNode = currentNode.parent;  // current Node to curren Node Parent
        }

        path.Reverse(); // now reversing the Path to get correct forward path
        return path; // returning path
    }

    // Methos to Getneighbors 
    public List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>(); // creating empaty Node

        // Row
        for (int x = -1; x <= 1; x++)
        {
            // Column
            for (int y = -1; y <= 1; y++)
            {
                // Skip the current node itself
                if (x == 0 && y == 0)
                    continue;

                // Allow only 4 direction (no diagnals)
                if (Mathf.Abs(x) + Mathf.Abs(y) == 2)
                    continue;

                // assigning next node
                int checkX = node.gridX + x; 
                int checkY = node.gridY + y;

                // Checking the grid with X and Y lent and adding it to te neighbors list
                if(checkX >= 0 && checkX < gridGenerator.gridHeight && checkY >= 0 && checkY < gridGenerator.gridWidth)
                {
                    neighbors.Add(gridGenerator.grids[checkX, checkY]); // Adding to neighbor
                }
            }
        }
        return neighbors;  // Returning neighbor
    }

    // Method to get distance by Cost
    public int GetDistance(Node a, Node b)
    {
        int distanceX = Mathf.Abs(a.gridX - b.gridX); // Getting Asolute value for X
        int distanceY = Mathf.Abs(a.gridY - b.gridY); // Getting Asolute value for Y

        // If the distanceX is bigger the staement will be excecuted or the outer return will be returned
        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}

