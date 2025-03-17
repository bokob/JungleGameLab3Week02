using UnityEngine;

public class TrainCheckRail : MonoBehaviour
{
    [Header("그리드 시스템")]
    [SerializeField] Grid _grid;
    Vector3 _checkPosition;                             // 그리드를 구하고 싶은 바닥 좌표를 구하기 위한 레이캐스트 발사 지점
    Vector3 _offset = new Vector3(0.5f, 0f, 0.5f);      // 그리드 -> 월드 시, 좌하단 좌표를 반환하므로 offset 더해야 그리드 중심이 됨
    public Vector3 GridCenterPos { get; private set; }  // 그리드 중심 좌표

    [Header("검사")]
    float _checkRange = 0.15f;
    LayerMask _railLayerMask = 1 << 9;

    [SerializeField] bool _isStop = false;
    [SerializeField] bool _isFindFront = false;
    [SerializeField] bool _isFindRight = false;
    [SerializeField] bool _isFindLeft = false;

    public bool IsStop { get { return _isStop; } }
    public bool IsFindFront { get { return _isFindFront; } }
    public bool IsFindRight { get { return _isFindRight; } }
    public bool IsFindLeft { get { return _isFindLeft; } }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _checkPosition = transform.position + transform.forward * 0.6f;
        UpdateCheckPosition();
        GridCenterPos = _checkPosition + _offset;

        CheckCurrent();
    }

    void Init()
    {
        GridSystem gridSystem = FindAnyObjectByType<GridSystem>();
        _grid = gridSystem.GetComponentInChildren<Grid>();
    }

    #region 그리드 관련
    // 기차가 검사할 앞 바닥 좌표
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
    public void UpdateCheckPosition()
    {
        // 검사하고 싶은 바닥 좌표 -> 해당하는 그리드 좌표 -> 월드 좌표
        Vector3 trainFrontPosition = GetSelectedMapPosition();             // 검사하고 싶은 바닥 좌표 구하기
        Vector3Int gridPosition = _grid.WorldToCell(trainFrontPosition);   // 바닥 좌표에 매칭되는 그리드 좌표
        _checkPosition = _grid.CellToWorld(gridPosition);                   // 그리드 좌표 -> 월드 좌표 (좌하단 모서리 좌표 반환)
    }
    #endregion


    public void CheckFront()
    {
        RaycastHit hit;

        // 앞쪽 검사
        Debug.DrawRay(GridCenterPos + transform.up * 0.5f, Vector3.down * 2f, Color.red);
        _isFindFront = Physics.Raycast(GridCenterPos + transform.up * 0.5f, Vector3.down, out hit, 2f, _railLayerMask);

        if (_isFindFront)
            Debug.Log(hit.collider.gameObject.name);
    }

    public void CheckSide()
    {
        RaycastHit hit;

        // 오른쪽 검사
        Debug.DrawRay(GridCenterPos - transform.forward + transform.right + transform.up * 0.5f, Vector3.down * 2f, Color.red);
        _isFindRight = Physics.Raycast(GridCenterPos - transform.forward + transform.right + transform.up * 0.5f, Vector3.down, out hit, 2f, _railLayerMask);

        if (_isFindRight)
            Debug.Log(hit.collider.gameObject.name);

        // 왼쪽 검사
        Debug.DrawRay(GridCenterPos - transform.forward - transform.right + transform.up * 0.5f, Vector3.down * 2f, Color.red);
        _isFindLeft = Physics.Raycast(GridCenterPos - transform.forward - transform.right + transform.up * 0.5f, Vector3.down, out hit, 2f, _railLayerMask);

        if (_isFindLeft)
            Debug.Log(hit.collider.gameObject.name);
    }

    public void CheckCurrent()
    {
        RaycastHit hit;
        Debug.DrawRay(GridCenterPos - transform.forward * 1 + transform.up * 0.5f, Vector3.down * 2f, Color.red);
        if(Physics.Raycast(GridCenterPos - transform.forward * 1 + transform.up * 0.5f, Vector3.down, out hit, 2f, _railLayerMask))
        {
            hit.collider.gameObject.layer = 0;
        }
    }

    void OnDrawGizmos()
    {
        if(_grid != null)
        {
            Gizmos.color = Color.red;

            Gizmos.color = (_isFindFront) ? Color.green : Color.red;
            Gizmos.DrawSphere(GridCenterPos, _checkRange);

            Gizmos.color = (_isFindRight) ? Color.green : Color.red;
            Gizmos.DrawSphere(GridCenterPos - transform.forward + transform.right, _checkRange);
            
            Gizmos.color = (_isFindLeft) ? Color.green : Color.red;
            Gizmos.DrawSphere(GridCenterPos - transform.forward - transform.right, _checkRange);
        }
    }
}
