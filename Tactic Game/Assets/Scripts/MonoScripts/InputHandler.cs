using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Private
    [SerializeField] private AudioManager audioManager;  // Audio Manager scipt
    [SerializeField] private GridGenerator gridGenerator;  // gridGenerator Script
    [SerializeField] private ObstacleInfo obstacleInfo;  // Obstacle Info Script
    private Animator animator;  // For Player Animator

    [SerializeField] private GameObject hoverPlanePrefab; // hover Plane Prefab
    private GameObject mousehoverPlane; // GameObject var for mousehoverPlane
    [SerializeField] private GameObject selectedPlane;  // SelectedPlane
    private float offsetY = 0.6f;  // Y offsetVal;
    [SerializeField] private TextMeshProUGUI posX_txt;  // PosX text in Screen
    [SerializeField] private TextMeshProUGUI posY_txt;  // PosY text in Screen

    [SerializeField] private PlayerMovement playerMovement;  // playerMovement Script
    private Transform playerPos;  // Player Position
    private Node startNode;  // starting Node
    private Node endNode;  // Ending Node
    private List<Node> playerPath;  // Node List of Player Path

    private void Start()
    {
        mousehoverPlane = Instantiate(hoverPlanePrefab);  // Instantiating hoverPlanePrefab
        mousehoverPlane.SetActive(false);  // deactivate it
        selectedPlane.SetActive(false);  // deactivate selectedPlan
        Invoke("Gettinganimator", 1f);  // Invoke Gettin animation with 0.5f deral.


    }

    // Update is called once per frame
    void Update()
    {
        MouseHoverGrid();  // Calling it on every frame
    }

    // Method the get animator
    private void Gettinganimator()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>(); // getting player Animator
    }

    // Method for mouse Hover functions on Grids 
    private void MouseHoverGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  // Use Ray Cast for finding Elements
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.tag == "Gridtile") // If ray hit on the GridTile
        {
            GridInfo gridInfo = hit.collider.gameObject.GetComponent<GridInfo>();  // Getting GridInfo of the hit object
            
            // If gridinfo not NUll
            if (gridInfo != null)
            {
                posX_txt.text = "Pos X: " + gridInfo.PosX.ToString(); // Assingn unit for UI Screen
                posY_txt.text = "Pos Y: " + gridInfo.PosY.ToString();
            }
            
            // If player is not Moving | and not a obstacje | and also not a enenmy
            if(!playerMovement.isMoving && !obstacleInfo.obstacleTiles[(int)gridInfo.PosX * 10 + (int)gridInfo.PosY])
            {
                mousehoverPlane.SetActive(true);  // active mouseHoverPlane.
                Vector3 hitGridPos = hit.collider.gameObject.transform.position;  // Hitted Grid position
                mousehoverPlane.transform.position = new Vector3(hitGridPos.x, offsetY, hitGridPos.z);  //Moving Mouse hover plane to grid position
     
                // If Mouse Left Buttton Clicked
                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("move", true);  // activate the animation
                    selectedPlane.SetActive(true);  // active selectedPlane
                    Vector3 selectedGridPos = hit.collider.gameObject.transform.position;  // selected plane position to hit position
                    selectedPlane.transform.position = new Vector3(selectedGridPos.x, offsetY, selectedGridPos.z);  // Transform the posiiton
                    endNode = gridGenerator.grids[(int)gridInfo.PosX, (int)gridInfo.PosY];  // assigning end node
                    playerPos = GameObject.FindGameObjectWithTag("Player").transform;  // Assigning player position
                    startNode = GetNodeFromPos(playerPos.position); // Getting Start noe 
                    MouseClickGrid(); 
                }
            }

        }
        else
        {
            mousehoverPlane.SetActive(false);  // mouse hover plane is deactivating

            posX_txt.text = "Pos X: "; // Assingn unit for UI Screen
            posY_txt.text = "Pos Y: ";
        }
    }

    // Method  which calls the Pathfinding Algorithm
    private void MouseClickGrid()
    {
        // I start node and end nodeids not null
        if (startNode != null && endNode != null) 
        {
            playerPath = PathfindingScript.instance.AStarGeneratePath(startNode, endNode);  // Getting Player path from A* algorthim 

            //  If playerpath is not null
            if (playerPath != null)
            {
                playerMovement.PathMovement(playerPath); // PathMove with the playerpath
            }
        }
    }


    // Getting Nod form Pos
    public Node GetNodeFromPos(Vector3 pos)
    {
        Ray ray = new Ray(pos, Vector3.down); // Ray casting doen he Player pod
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GridInfo gridInfo = hit.collider.GetComponent<GridInfo>();  // Getting gridInfo from the hitted tile  

            return gridGenerator.grids[(int)gridInfo.PosX, (int)gridInfo.PosY]; //  return Grid Gnenerator with the Node
        }
        return null;  // retun null
    }

    
}
