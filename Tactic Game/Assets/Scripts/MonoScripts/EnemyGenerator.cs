using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Private
    [SerializeField] private ObstacleInfo obstacleInfo; // Getting ScriptableObject obstacle Info
    [SerializeField] private GridGenerator gridGenerator; // Getting Grid Genenrator Script
    [SerializeField] private PlayerGenerator playerGenerator;   // Getting player Genenrator Script
    [SerializeField] private PlayerMovement playerMovement;  // Getting player Movement Script
    [SerializeField] private InputHandler inputHandler;
    [SerializeField] private GameObject enemyPrefab;  // Getting enemy Prefab
    Node enemyGeneratedGridNode;
    private bool enemyGenerated = false;  // to represent the status, that the enemy is generated or not
    private float offsetY = 1f;  // Y unit offset

    // Start is called before the first frame update
    void Start()
    {
        // Invoking function on the start with the delay
        Invoke("GenerateEnemy", 1f);
    }

    // Method to Generate Enemy
    private void GenerateEnemy()
    {
        int posX = 0;
        int posZ = 0;

        while (!enemyGenerated)
        {

            // Postion pos of X and Zare same then return
            if (posX == playerGenerator.playerPosX && posZ == playerGenerator.playerPosZ)
            {
                return;
            }

            // If it is not an obstackle then,
            if (!obstacleInfo.obstacleTiles[posX * 10 + posZ])
            {
                // assigning new position
                Vector3 newEnemyPosition = new Vector3(posX * gridGenerator.gridSpace, offsetY, posZ * gridGenerator.gridSpace);

                // Instantiaiying Enemy
                GameObject newEnemy = Instantiate(enemyPrefab, newEnemyPosition, Quaternion.identity);

                // Assigning name as Enemy
                newEnemy.name = "Enemy";
                // To represent enemy generated
                enemyGenerated = true;

                enemyGeneratedGridNode = inputHandler.GetNodeFromPos(newEnemyPosition);
                enemyGeneratedGridNode.walkable = false;

                return;
            }

            posX = Random.Range(0, 10);  //  Ranzom number of X
            posZ = Random.Range(0, 10);  // Random number o Y

            

        }
    }
}
