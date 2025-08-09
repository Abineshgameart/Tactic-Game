using System.Collections.Generic;
using UnityEngine;

public class PathfindingScript : MonoBehaviour
{
    // Public
    public static PathfindingScript instance;

    //Private
    [SerializeField] private GridGenerator gridGenerator;

    private void Awake()
    {
        instance = this;
    }

    

    public List<Node> AStarGeneratePath(Node start, Node end)
    {
        List<Node> openList = new List<Node>();
        HashSet<Node> closeList = new HashSet<Node>();
        
        openList.Add(start);

        while(openList.Count > 0)
        {
            Node current = openList[0];

            for(int i = 1; i < openList.Count; i++)
            {
                if (openList[i].fCost < current.fCost || (openList[i].fCost == current.fCost && openList[i].hCost < current.hCost)) {
                    current = openList[i];
                }
            }

            openList.Remove(current);
            closeList.Add(current); 

            if(current == end)
            {
                return RetracePath(start, end);
            }
            
            foreach(Node neighbor in GetNeighbors(current))
            {
                if(!neighbor.walkable || closeList.Contains(neighbor))
                {
                    continue;
                }

                int newCostToNeighbor = current.gCost + GetDistance(current, neighbor);

                if(newCostToNeighbor < neighbor.gCost || !openList.Contains(neighbor))
                {
                    neighbor.gCost = newCostToNeighbor;
                    neighbor.hCost = GetDistance(neighbor, end);
                    neighbor.parent = current;

                    if(!openList.Contains(neighbor))
                    {
                        openList.Add(neighbor);
                    }
                }
            }
        }

        return null;
    }

    List<Node> RetracePath(Node start, Node end)
    {
        List<Node> path = new List<Node>();

        Node currentNode = end;

        while (currentNode != start)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;  
        }

        path.Reverse();
        return path;
    }

    List<Node> GetNeighbors(Node node)
    {
        List<Node> neighbors = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // Skip the current node itself
                if (x == 0 && y == 0)
                    continue;

                // Allow only 4 direction (no diagnals)
                if (Mathf.Abs(x) + Mathf.Abs(y) == 2)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if(checkX >= 0 && checkX < gridGenerator.gridHeight && checkY >= 0 && checkY < gridGenerator.gridWidth)
                {
                    neighbors.Add(gridGenerator.grids[checkX, checkY]);
                }
            }
        }
        return neighbors;
    }

    private int GetDistance(Node a, Node b)
    {
        int distanceX = Mathf.Abs(a.gridX - b.gridX);
        int distanceY = Mathf.Abs(a.gridY - b.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}

