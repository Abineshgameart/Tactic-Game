using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    // Private
    [SerializeField] ObstacleInfo obstacleInfo;  // Getting ScriptableObject obstacle Info
    [SerializeField] GridGenerator gridGenerator;  // Getting GridGnenerator Script
    [SerializeField] private GameObject playerPrefab;  // Getting Player Prefab
    private bool playerGenerated = false;  // bool to represent the player is generated or not
    private float offsetY = 1f;  // y axis offset value

    // Public
    public int playerPosX = 0;  // Player X position
    public int playerPosZ = 0;  // Player Z position

    // Start is called before the first frame update
    void Start()
    {
        // Invoking GeneratePlayer with Delay
        Invoke("GeneratePlayer", 0.5f);
    }

    // Generate Palyer Methos to generate in Random Grid
    private void GeneratePlayer()
    {
        
        // Player not Genreated is the condition
        while (!playerGenerated)
        {
            // If it is not a obstacle position, the proceed with that position
            if(!obstacleInfo.obstacleTiles[playerPosX * 10 + playerPosZ])
            {
                // new Player Position
                Vector3 newPlayerPosition = new Vector3(playerPosX * gridGenerator.gridSpace, offsetY, playerPosZ * gridGenerator.gridSpace);

                GameObject newPlayer = Instantiate(playerPrefab, newPlayerPosition, Quaternion.identity); // Instatiating the Player on the Position

                newPlayer.name = "Player";  // assigning the name for the player Instance
                return;
            }

            playerPosX = Random.Range(0, 10); // Random X Position
            playerPosZ = Random.Range(0, 10); // Random Y Position

        }
    }
}
