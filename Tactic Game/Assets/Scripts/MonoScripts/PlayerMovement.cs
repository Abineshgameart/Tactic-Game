using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Private
    private GameObject player;
    private float moveSpeed = 3f;

    private float gridspace = 1f;
    
    // Public
    public bool isMoving = false;
    
    public void PathMovement(List<Node> playerPath)
    {
        Debug.Log("Movement Called");
        
        if(!isMoving)
        {
            StartCoroutine(MoveWithinPath(playerPath));
        }
    }

    private IEnumerator MoveWithinPath(List<Node> path)
    {
        isMoving = true;

        player = GameObject.FindGameObjectWithTag("Player");
        foreach (Node node in path)
        {
            Vector3 nextPos = new Vector3(node.gridX * gridspace, 1f, node.gridY * gridspace);
            Debug.Log(nextPos);

            while (Vector3.Distance(player.transform.position, nextPos) > 0.05f)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position, nextPos, moveSpeed * Time.deltaTime);
                yield return null;
            }
        }

        isMoving = false;
    }
    
}
