using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] ObstacleInfo obstacleInfo;
    [SerializeField] GridGenerator gridGenerator;
    [SerializeField] private GameObject playerPrefab;
    private bool playerGenerated = false;
    private float offsetY = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        GeneratePlayer();
    }

    private void GeneratePlayer()
    {
        int posX = 0;
        int posZ = 0;

        while (!playerGenerated)
        {
            
            if(!obstacleInfo.obstacleTiles[posX * 10 + posZ])
            {
                Vector3 newPlayerPosition = new Vector3(posX * gridGenerator.gridSpace, offsetY, posZ * gridGenerator.gridSpace);

                GameObject newPlayer = Instantiate(playerPrefab, newPlayerPosition, Quaternion.identity);

                newPlayer.name = "Player";
                return;
            }

            posX = Random.Range(0, 10);
            posZ = Random.Range(0, 10);

        }
    }
}
