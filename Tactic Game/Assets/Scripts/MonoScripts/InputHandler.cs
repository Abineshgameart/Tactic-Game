using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    // Private
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private ObstacleInfo obstacleInfo;

    [SerializeField] private GameObject hoverPlanePrefab;
    private GameObject mousehoverPlane;
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
    }

    // Update is called once per frame
    void Update()
    {
        MouseHoverGrid();
    }

    private void MouseHoverGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.tag == "Gridtile")
        {
            GridInfo gridInfo = hit.collider.gameObject.GetComponent<GridInfo>();
            
            
            if(!playerMovement.isMoving && !obstacleInfo.obstacleTiles[(int)gridInfo.PosX * 10 + (int)gridInfo.PosY])
            {
                mousehoverPlane.SetActive(true);
                Vector3 hitGridPos = hit.collider.gameObject.transform.position;
                mousehoverPlane.transform.position = new Vector3(hitGridPos.x, offsetY, hitGridPos.z);
            }

            

            if(Input.GetMouseButtonDown(0))
            {
                endNode = gridGenerator.grids[(int)gridInfo.PosX, (int)gridInfo.PosY];
                playerPos = GameObject.FindGameObjectWithTag("Player").transform;
                startNode = GetNodeFromPos(playerPos.position);
                Debug.Log("Mouse Left Button");
                MouseClickGrid();
            }


            if (gridInfo != null)
            {
                posX_txt.text = "Pos X: " + gridInfo.PosX.ToString();
                posY_txt.text = "Pos Y: " + gridInfo.PosY.ToString();
            }
            else
            {
                posX_txt.text = "Pos X: ";
                posY_txt.text = "Pos Y: ";
            }
        }
        else
        {
            mousehoverPlane.SetActive(false);
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


    private Node GetNodeFromPos(Vector3 pos)
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
