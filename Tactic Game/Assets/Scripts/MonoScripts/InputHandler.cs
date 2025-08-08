using TMPro;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI posX_txt;
    [SerializeField] private TextMeshProUGUI posY_txt;

    // Update is called once per frame
    void Update()
    {
        MouseHoverGrid();
    }

    private void MouseHoverGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
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
    }
}
