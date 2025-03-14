using UnityEngine;

public class TestPlayerPlacement : MonoBehaviour
{
    [SerializeField] GameObject mouseIndicator;
    [SerializeField] TestInputManager _inputManager;

    [SerializeField] GameObject cellIndicator;
    [SerializeField] Grid grid;

    void Update()
    {
        Vector3 mousePosition = _inputManager.GetSelectedMapPosition();
        mouseIndicator.transform.position = mousePosition;


        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        cellIndicator.transform.position = grid.CellToWorld(gridPosition);
    }
}
