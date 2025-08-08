using TMPro;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private GameObject hoverPlanePrefab;
    private GameObject mousehoverPlane;
    private float offsetY = 0.6f;
    [SerializeField] private TextMeshProUGUI posX_txt;
    [SerializeField] private TextMeshProUGUI posY_txt;

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
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.tag == "Gridtile")
        {
            mousehoverPlane.SetActive(true);
            Vector3 hitGridPos = hit.collider.gameObject.transform.position;
            mousehoverPlane.transform.position = new Vector3(hitGridPos.x, offsetY, hitGridPos.z);

            GridInfo gridInfo = hit.collider.gameObject.GetComponent<GridInfo>();

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
}
