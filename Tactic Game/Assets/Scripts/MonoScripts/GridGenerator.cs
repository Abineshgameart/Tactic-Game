using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Private

    [SerializeField] private GameObject gridPrefab;
    [SerializeField] ObstacleInfo obstacleInfo;
    private bool walkable = true;


    // Public
    public int gridHeight = 10;
    public int gridWidth = 10;
    public float gridSpace = 1f;
    public Node[,] grids;

    
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (gridPrefab == null)
        {
            Debug.Log("Grid prefab not assigned.");
            return;
        }

        grids = new Node[gridHeight, gridWidth];

        for (int i = 0; i < gridHeight; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                Vector3 newGridPosition = new Vector3(i * gridSpace, 0, j * gridSpace);

                GameObject newGrid = Instantiate(gridPrefab, newGridPosition, Quaternion.identity);

                newGrid.transform.parent = transform;

                newGrid.name = $"Tile({i},{j})";

                GridInfo gridInfo = newGrid.GetComponent<GridInfo>();
                gridInfo.PosX = newGridPosition.x;
                gridInfo.PosY = newGridPosition.z;

                if(obstacleInfo.obstacleTiles[i * gridWidth + j])
                {
                    walkable = false;
                } 
                else
                {
                    walkable = true;
                }

                grids[i, j] = new Node(walkable, i, j);

            }
        }
    }
}
