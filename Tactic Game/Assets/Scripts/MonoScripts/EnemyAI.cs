using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IAI
{
    // Private
    [SerializeField] private PathfindingScript PathfindingScript;
    [SerializeField] private InputHandler inputHandler;
    private Animator animator;  // Enemy Animator
    private Vector3 playerPos;  // Player Position
    private Vector3 enemyPos;  // Enenmy Position
    private Node playerNode;  // Player Position Node
    private Node enemyNode;  // Enemy position Node
    Node playerNearestNode;  // 
    private List<Node> enemyPath;  // List Node for Enemy Path
    private GameObject enemy;  // Enemy GameObject
    private float moveSpeed = 3f;  // Movement Speed 
    private float gridspace = 1f;  // Grid Space
    
    // Public
    public bool isenemyMoving = false;

    // Method to start the Enemy Path finder with delay
    public void startLocatingPlayer()
    {
        Invoke("EnemyPathFinder", 0.5f); // Invoking Method with delay
    }

    public void EnemyPathFinder()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;  // Assinging player position
        enemyPos = GameObject.FindGameObjectWithTag("Enemy").transform.position;  // Assigning enemy position

        playerNode = inputHandler.GetNodeFromPos(playerPos); // Getting player node by Position
        enemyNode = inputHandler.GetNodeFromPos(enemyPos);  // Getting enemy node by position

        enemyNode.walkable = true; // setting walkable true before moving

        List<Node> playerNeighbors = new List<Node>();  // Lsit for Player Neighbors
        playerNeighbors = PathfindingScript.GetNeighbors(playerNode);  // Getting player Neighbors
        playerNearestNode = null;  // assigning nearestNode null for now

        // Comparing player neighbor postion with the enemy node pos and chooosing one.
        for(int i = 0; i < playerNeighbors.Count - 2; i++)
        { 
            //  distance between enemy node and neightbours
            int dist1 = PathfindingScript.GetDistance(enemyNode, playerNeighbors[i]);
            int dist2 = PathfindingScript.GetDistance(enemyNode, playerNeighbors[i + 1]); 

            if(dist1 < dist2)
            {
                playerNearestNode = playerNeighbors[i];  // if i is Less assign it to nearest
            } 
            else
            {
                playerNearestNode = playerNeighbors[i + 1];  // or this one
            }
        }

        //  Finding the shortest path by A * Algorithm from enemy node to nearest node
        enemyPath = PathfindingScript.AStarGeneratePath(enemyNode, playerNearestNode);
        
        // Is Enemy path is not null start moving
        if (enemyPath != null)  
        {

            // Calling movement Function
            EnemyMovement();
        }
    }

    //  Method for enemy movement
    public void EnemyMovement()
    {
        animator = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>(); // Getting enemy animator
        animator.SetBool("enemyMove", true);  // Activating enemy animator
        StartCoroutine(MoveWithinEnemyPath(enemyPath)); // starting coroutine for the smooth movements
        
    }

    // IEnumerator for path movement
    private IEnumerator MoveWithinEnemyPath(List<Node> path)
    {
        isenemyMoving = true;

        enemy = GameObject.FindGameObjectWithTag("Enemy"); // Getting enemy gameobject by Tag
        
        // Taking Node itn the path
        foreach (Node node in path)
        {
            Vector3 nextPos = new Vector3(node.gridX * gridspace, 1f, node.gridY * gridspace);  // next position for the enemy

            //  checkin the distance with the condition, which is greater thea 0.05f 
            while (Vector3.Distance(enemy.transform.position, nextPos) > 0.05f) 
            {
                // Moving the enemy towards next position
                enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;  // returning null
            }
        }

        animator.SetBool("enemyMove", false); // Deactivating the animation.
        playerNearestNode.walkable = false;
        isenemyMoving = false;
    }
}


