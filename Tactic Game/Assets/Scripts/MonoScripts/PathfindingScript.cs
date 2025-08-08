using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingScript : MonoBehaviour
{
    // Private
    [SerializeField] private List<Vector3> gridToSearch;
    [SerializeField] private List<Vector3> searchedGrids;
    [SerializeField] private List<Vector3> finalPath;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FindPath(Vector3 startPos, Vector3 endPos)
    {
        searchedGrids = new List<Vector3>();
        gridToSearch = new List<Vector3> { startPos };
        finalPath = new List<Vector3>();

        Grid startGrid = grid[startPos];
        startGrid.gCost = 0;
        startGrid.hCost = GetDistance(startPos, endPos);
        startGrid.fCost = GetDistance(startPos, endPos);

    }

    private int GetDistance(Vector3 pos1, Vector3 pos2)
    {
        Vector3Int dist = new Vector3Int(Mathf.Abs((int)pos1.x - (int)pos2.x), Mathf.Abs((int)pos1.y - (int)pos2.y), Mathf.Abs((int)pos1.z - (int)pos2.z));
        int lowest = Mathf.Min(dist.x, dist.z);
        int highest = Mathf.Max(dist.x, dist.z);

        int horizontalMovesRequired = highest - lowest;

        return lowest * 10 + horizontalMovesRequired * 10;

    }

    private class Grid
    {
        public Vector3 position;
        public int fCost = int.MaxValue;
        public int gCost = int.MaxValue;
        public int hCost = int.MaxValue;
        public Vector3 connection;
        public bool isobstacle;

        public Grid(Vector3 pos)
        {
            position = pos;
        }
    }
}

