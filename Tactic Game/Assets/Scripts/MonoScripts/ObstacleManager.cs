using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    // Private
    [SerializeField] ObstacleInfo obstacleInfo;
    private int obstacleLength = 10;
    private float obstacleSpace = 1f;
    private float offsetY = 0.8f;

    [SerializeField] private GameObject obstaclePrefab;

    // Start is called before the first frame update
    void Start()
    {
        GenerateObstacle();
    }

    private void GenerateObstacle()
    {
        for (int i = 0; i < obstacleLength; i++)
        {
            for(int j = 0; j < obstacleLength; j++)
            {
                int obstacleIndex = i * obstacleLength + j;
                if (obstacleInfo.obstacleTiles[obstacleIndex])
                {
                    Vector3 newObstaclePosition = new Vector3(i * obstacleSpace, offsetY, j * obstacleSpace);
                    GameObject newObstacle = Instantiate(obstaclePrefab, newObstaclePosition, Quaternion.identity);

                    newObstacle.transform.parent = transform;

                    newObstacle.name = $"Obstacle({i},{j})";
                }
            }
        }
    }
}
