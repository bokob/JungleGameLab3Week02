using UnityEngine;

public class PlayerGrid : MonoBehaviour
{
    [Header("그리드 시스템")]
    [SerializeField] Grid _grid;
    [SerializeField] GameObject _cellIndicator;
    Vector3 _checkPosition;                             // 그리드를 구하고 싶은 바닥 좌표를 구하기 위한 레이캐스트 발사 지점
    Vector3 _offset = new Vector3(0.5f, 0f, 0.5f);      // 그리드 -> 월드 시, 좌하단 좌표를 반환하므로 offset 더해야 그리드 중심이 됨
    public Vector3 GridCenterPos { get; private set; }  // 그리드 중심 좌표

    void Start()
    {
        Init();
    }

    void Init()
    {
        GridSystem gridSystem = FindAnyObjectByType<GridSystem>();
        _grid = gridSystem.GetComponentInChildren<Grid>();
        _cellIndicator = gridSystem.GetComponentInChildren<CellIndicator>().gameObject;
    }

    void Update()
    {
        _checkPosition = transform.position + transform.forward * 0.6f;
        ShowGridIndicator();
        GridCenterPos = _checkPosition + _offset;
    }

    // 플레이어가 검사할 앞 바닥 좌표
    public Vector3 GetSelectedMapPosition()
    {
        Debug.DrawRay(_checkPosition, Vector3.down, Color.red);

        RaycastHit hit;
        Vector3 position = Vector3.zero;
        if (Physics.Raycast(_checkPosition, Vector3.down, out hit, 1.2f, 1))
        {
            position = hit.point;
        }
        return position;
    }

    // 그리드 위치(중심)에 인디케이터 표시
    public void ShowGridIndicator()
    {
        // 검사하고 싶은 바닥 좌표 -> 해당하는 그리드 좌표 -> 월드 좌표
        Vector3 playerFrontPosition = GetSelectedMapPosition();             // 검사하고 싶은 바닥 좌표 구하기
        Vector3Int gridPosition = _grid.WorldToCell(playerFrontPosition);   // 바닥 좌표에 매칭되는 그리드 좌표
        _checkPosition = _grid.CellToWorld(gridPosition);                   // 그리드 좌표 -> 월드 좌표 (좌하단 모서리 좌표 반환)
        _cellIndicator.transform.position = _checkPosition;                 // 인디케이터는 이미 x, z가 0.5씩 더해져 있음
    }
}