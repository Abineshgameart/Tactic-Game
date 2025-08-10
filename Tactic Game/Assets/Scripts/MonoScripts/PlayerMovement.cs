using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Private
    [SerializeField] EnemyAI enemyAI;  
    [SerializeField] private GameObject selectedPlane;
    private GameObject player;  

    private float moveSpeed = 3f;  // Movement Speed
    private float gridspace = 1f;  // Grid Tiles Gap

    private Animator animator;  // Player animator
    
    // Public
    public bool isMoving = false;  // status of player moving or not
    
    // Starting Movemnt through path
    public void PathMovement(List<Node> playerPath)
    {
        // If not player moving then start moving
        if(!isMoving)
        {
            StartCoroutine(MoveWithinPath(playerPath)); // Starting Courouting for movenment
        }
    }

    // IEnumerator to Move within path
    private IEnumerator MoveWithinPath(List<Node> path)
    {
        isMoving = true;  // Representing player is Moving
        

        player = GameObject.FindGameObjectWithTag("Player"); // Getting player gamobject by Tag
        
        // Getting Node from the path to make movements
        foreach (Node node in path) 
        {
            Vector3 nextPos = new Vector3(node.gridX * gridspace, 1f, node.gridY * gridspace); // Next Position of the player

            // Checking the distance is 0.05f as a while condition
            while (Vector3.Distance(player.transform.position, nextPos) > 0.05f)
            {
                // Moving the player to the next position
                player.transform.position = Vector3.MoveTowards(player.transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        isMoving = false;  // Representing Player is not Moving
        animator = player.GetComponent<Animator>();  // getting player animator
        animator.SetBool("move", false); // stopping the animation
        selectedPlane.SetActive(false);  // detaivating the select active plane

        enemyAI.startLocatingPlayer(); // calling Enemy AI to start Tracking Player
    }
    
}
