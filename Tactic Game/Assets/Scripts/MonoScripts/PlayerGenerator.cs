using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    // Private
    [SerializeField] ObstacleInfo obstacleInfo;
    [SerializeField] GridGenerator gridGenerator;
    [SerializeField] private GameObject playerPrefab;
    private bool playerGenerated = false;
    private float offsetY = 1f;

    // Public
    public int playerPosX = 0;
    public int playerPosZ = 0;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GeneratePlayer", 0.5f);
    }

    private void GeneratePlayer()
    {
        

        while (!playerGenerated)
        {
            
            if(!obstacleInfo.obstacleTiles[playerPosX * 10 + playerPosZ])
            {
                Vector3 newPlayerPosition = new Vector3(playerPosX * gridGenerator.gridSpace, offsetY, playerPosZ * gridGenerator.gridSpace);

                GameObject newPlayer = Instantiate(playerPrefab, newPlayerPosition, Quaternion.identity);

                newPlayer.name = "Player";
                return;
            }

            playerPosX = Random.Range(0, 10);
            playerPosZ = Random.Range(0, 10);

        }
    }
}
