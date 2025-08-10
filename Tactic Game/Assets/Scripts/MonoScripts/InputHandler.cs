using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Private
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private ObstacleInfo obstacleInfo;
    private Animator animator;

    [SerializeField] private GameObject hoverPlanePrefab;
    private GameObject mousehoverPlane;
    [SerializeField] private GameObject selectedPlane;
    private float offsetY = 0.6f;
    [SerializeField] private TextMeshProUGUI posX_txt;
    [SerializeField] private TextMeshProUGUI posY_txt;

    [SerializeField] private PlayerMovement playerMovement;
    private Transform playerPos;
    private Node startNode;
    private Node endNode;
    private List<Node> playerPath;

    private void Start()
    {
        mousehoverPlane = Instantiate(hoverPlanePrefab);
        mousehoverPlane.SetActive(false);
        selectedPlane.SetActive(false);
        Invoke("Gettinganimator", 0.5f);


    }

    // Update is called once per frame
    void Update()
    {
        MouseHoverGrid();
    }

    private void Gettinganimator()
    {
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }


    private void MouseHoverGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.tag == "Gridtile")
        {
            GridInfo gridInfo = hit.collider.gameObject.GetComponent<GridInfo>();
            
            if (gridInfo != null)
            {
                posX_txt.text = "Pos X: " + gridInfo.PosX.ToString();
                posY_txt.text = "Pos Y: " + gridInfo.PosY.ToString();
            }
            
            if(!playerMovement.isMoving && !obstacleInfo.obstacleTiles[(int)gridInfo.PosX * 10 + (int)gridInfo.PosY])
            {
                mousehoverPlane.SetActive(true);
                Vector3 hitGridPos = hit.collider.gameObject.transform.position;
                mousehoverPlane.transform.position = new Vector3(hitGridPos.x, offsetY, hitGridPos.z);
     

                if (Input.GetMouseButtonDown(0))
                {
                    animator.SetBool("move", true);
                    selectedPlane.SetActive(true);
                    Vector3 selectedGridPos = hit.collider.gameObject.transform.position;
                    selectedPlane.transform.position = new Vector3(selectedGridPos.x, offsetY, selectedGridPos.z);
                    endNode = gridGenerator.grids[(int)gridInfo.PosX, (int)gridInfo.PosY];
                    playerPos = GameObject.FindGameObjectWithTag("Player").transform;
                    startNode = GetNodeFromPos(playerPos.position);
                    Debug.Log("Mouse Left Button");
                    MouseClickGrid();
                }
            }


        }
        else
        {
            mousehoverPlane.SetActive(false);

            posX_txt.text = "Pos X: ";
            posY_txt.text = "Pos Y: ";
        }
    }

    private void MouseClickGrid()
    {
        if (startNode != null && endNode != null)
        {
            Debug.Log("Calling PathfindingScript");
            playerPath = PathfindingScript.instance.AStarGeneratePath(startNode, endNode);

            if (playerPath != null)
            {
                Debug.Log("Calling playerMovementScript");
                playerMovement.PathMovement(playerPath);
            }
        }
    }


    public Node GetNodeFromPos(Vector3 pos)
    {
        Ray ray = new Ray(pos, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GridInfo gridInfo = hit.collider.GetComponent<GridInfo>();

            return gridGenerator.grids[(int)gridInfo.PosX, (int)gridInfo.PosY]; 
        }
        return null;
    }

    
}
