using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Private

    [SerializeField] private GameObject gridPrefab; // Grid Prefab
    [SerializeField] ObstacleInfo obstacleInfo;  // Obstacle Info
    private bool walkable = true;  // Walkable or Obstacle


    // Public
    public int gridHeight = 10;  // Grid Height
    public int gridWidth = 10;  // Grid Width
    public float gridSpace = 1f;  // Grid Space
    public Node[,] grids;  // two-dimentional grid array

    
    // Start is called before the first frame update
    void Start()
    {
        GenerateGrid(); // Generating Grid on the start
    }

    // Method for Generating grid
    private void GenerateGrid()
    {
        //  If the prefab is return without any errors
        if (gridPrefab == null)
        {
            return;
        }

        grids = new Node[gridHeight, gridWidth];  // Setting legth of the grids

        // Row
        for (int i = 0; i < gridHeight; i++)
        {
            // Column
            for (int j = 0; j < gridWidth; j++)
            {
                Vector3 newGridPosition = new Vector3(i * gridSpace, 0, j * gridSpace);  // New Grid Postion

                GameObject newGrid = Instantiate(gridPrefab, newGridPosition, Quaternion.identity); // Instantiating New Grid on the Position

                newGrid.transform.parent = transform;  // Parating it under the Grid Manager

                newGrid.name = $"Tile({i},{j})";  //  Assiging new Grid name

                GridInfo gridInfo = newGrid.GetComponent<GridInfo>();  // Getting GirdInfo Script
                gridInfo.PosX = newGridPosition.x;  // Initializing Grid info Position X
                gridInfo.PosY = newGridPosition.z;  // Initializing Grid info Position Y
                
                //  If it is obstacle then walkables is false
                if(obstacleInfo.obstacleTiles[i * gridWidth + j])
                {
                    walkable = false;  // Assigning wakables as False
                } 
                else
                {
                    walkable = true;  // Assigning wakables as True
                }

                grids[i, j] = new Node(walkable, i, j);  // Creating and assigning Node in list

            }
        }
    }
}
