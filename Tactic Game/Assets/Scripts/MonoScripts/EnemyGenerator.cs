using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // Private
    [SerializeField] ObstacleInfo obstacleInfo;
    [SerializeField] GridGenerator gridGenerator;
    [SerializeField] PlayerGenerator playerGenerator;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private GameObject enemyPrefab;
    private bool enemyGenerated = false;
    private float offsetY = 1f;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("GenerateEnemy", 1f);
    }

    private void GenerateEnemy()
    {
        int posX = 0;
        int posZ = 0;

        while (!enemyGenerated)
        {
            if (posX == playerGenerator.playerPosX && posZ == playerGenerator.playerPosZ)
            {
                return;
            }

            if (!obstacleInfo.obstacleTiles[posX * 10 + posZ])
            {
                Vector3 newEnemyPosition = new Vector3(posX * gridGenerator.gridSpace, offsetY, posZ * gridGenerator.gridSpace);

                GameObject newEnemy = Instantiate(enemyPrefab, newEnemyPosition, Quaternion.identity);

                newEnemy.name = "Enemy";
                return;
            }

            posX = Random.Range(0, 10);
            posZ = Random.Range(0, 10);

        }
    }
}
