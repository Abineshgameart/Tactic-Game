using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    // Private
    private int gridHeight = 10;
    private int gridWidth = 10;
    private float gridSpace = 1f;

    [SerializeField] private GameObject gridPrefab;

    // Public
    

    
    // Start is called before the first frame update
    void Start()
    {
       GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid()
    {
        if (gridPrefab == null)
        {
            Debug.Log("Grid prefab not assigned.");
            return;
        }

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
            }
        }
    }
}
