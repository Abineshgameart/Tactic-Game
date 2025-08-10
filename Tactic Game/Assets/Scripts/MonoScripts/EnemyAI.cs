using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    [SerializeField] private PathfindingScript PathfindingScript;
    [SerializeField] private InputHandler inputHandler;
    private Animator animator;
    private Vector3 playerPos;
    private Vector3 enemyPos;
    private Node playerNode;
    private Node enemyNode;
    private List<Node> enemyPath;
    private GameObject enemy;
    private float moveSpeed = 3f;
    private float gridspace = 1f;

    public void startLocatingPlayer()
    {
        Invoke("EnemyPathFinder", 0.5f);
    }

    public void EnemyPathFinder()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        enemyPos = GameObject.FindGameObjectWithTag("Enemy").transform.position;

        playerNode = inputHandler.GetNodeFromPos(playerPos);
        enemyNode = inputHandler.GetNodeFromPos(enemyPos);

        List<Node> playerNeighbors = new List<Node>();
        playerNeighbors = PathfindingScript.GetNeighbors(playerNode);
        Node nearestNode = null;
        for(int i = 0; i < playerNeighbors.Count - 2; i++)
        {
            int dist1 = PathfindingScript.GetDistance(enemyNode, playerNeighbors[i]);
            int dist2 = PathfindingScript.GetDistance(enemyNode, playerNeighbors[i + 1]);

            if(dist1 < dist2)
            {
                nearestNode = playerNeighbors[i];
            } 
            else
            {
                nearestNode = playerNeighbors[i + 1];
            }
        }
        enemyPath = PathfindingScript.AStarGeneratePath(enemyNode, nearestNode);
        if (enemyPath != null)
        {
            EnemyMovement();
        }
    }

    public void EnemyMovement()
    {
        animator = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
        animator.SetBool("enemyMove", true);
        StartCoroutine(MoveWithinEnemyPath(enemyPath));
        
    }

    private IEnumerator MoveWithinEnemyPath(List<Node> path)
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        foreach (Node node in path)
        {
            Vector3 nextPos = new Vector3(node.gridX * gridspace, 1f, node.gridY * gridspace);
            Debug.Log(nextPos);

            while (Vector3.Distance(enemy.transform.position, nextPos) > 0.05f)
            {
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        animator.SetBool("enemyMove", false);
    }
}
