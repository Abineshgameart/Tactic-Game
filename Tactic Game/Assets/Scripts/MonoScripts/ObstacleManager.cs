using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // Private
    [SerializeField] ObstacleInfo obstacleInfo; // Getting ScriptableObject obstacle Info
    private int obstacleLength = 10; // Obstacle Length
    private float obstacleSpace = 1f; // Obstacle Space 
    private float offsetY = 0.8f; // Obstacle Y offset Val

    [SerializeField] private GameObject obstaclePrefab;  // Obstacle Prefab

    // Start is called before the first frame update
    void Start()
    {
        GenerateObstacle(); // Generating Player at Start
    }

    // Method to Generate Obstacles
    private void GenerateObstacle()
    {
        // Row
        for (int i = 0; i < obstacleLength; i++)
        {
            // Column
            for(int j = 0; j < obstacleLength; j++)
            {
                int obstacleIndex = i * obstacleLength + j; // Getting obstacle indexby
                
                // Checking for obstacle bool true in the Scriptable Object
                if (obstacleInfo.obstacleTiles[obstacleIndex])
                {
                    // New Obstacle Position
                    Vector3 newObstaclePosition = new Vector3(i * obstacleSpace, offsetY, j * obstacleSpace);

                    // Instantiating the New Obstacle
                    GameObject newObstacle = Instantiate(obstaclePrefab, newObstaclePosition, Quaternion.identity);  

                    newObstacle.transform.parent = transform; // Paranting it under the Manager

                    newObstacle.name = $"Obstacle({i},{j})";  // New obstacle name
                }
            }
        }
    }
}
