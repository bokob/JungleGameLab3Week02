using UnityEngine;

public class WorkTable : Tool
{
    [SerializeField] bool _isHold = false;

    [Header("그리드 시스템")]
    [SerializeField] Grid _grid;
    Vector3 _checkPosition;                             // 그리드를 구하고 싶은 바닥 좌표를 구하기 위한 레이캐스트 발사 지점
    Vector3 _offset = new Vector3(0.5f, 0f, 0.5f);      // 그리드 -> 월드 시, 좌하단 좌표를 반환하므로 offset 더해야 그리드 중심이 됨
    public Vector3 GridCenterPos { get; private set; }  // 그리드 중심 좌표

    [Header("레이어 마스크")]
    LayerMask _environmentLayerMask = 1 << 7;   // 환경
    LayerMask _oneHandLayerMask = 1 << 6;       // 도구
    LayerMask _twoHandLayerMask = 1 << 8;       // 자원
    LayerMask _railLayerMask = 1 << 9;          // 레일

    LayerMask _frontObstacleLayerMask;
    LayerMask _frontLayerMask;

    [SerializeField] bool _isFindRight = false;
    [SerializeField] bool _isFindLeft = false;
    [SerializeField] bool _isFindSide = false;

    [SerializeField] bool _isCanSide = false;
    [SerializeField] bool _isCanFront = false;
    [SerializeField] bool _isCanMake = false;

    [Header("재료")]
    [SerializeField] int _currentWoodCount = 0;
    [SerializeField] int _currentIronCount = 0;

    [Header("레일")]
    [SerializeField] GameObject _railPrefab;


    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        HandHoldType = Define.HandHold.OneHand;
        ToolType = Define.Tool.WorkTable;

        GridSystem gridSystem = FindAnyObjectByType<GridSystem>();
        _grid = gridSystem.GetComponentInChildren<Grid>();

        _frontObstacleLayerMask = _environmentLayerMask | _oneHandLayerMask | _twoHandLayerMask | _railLayerMask;
    }

    void Update()
    {
        _checkPosition = transform.position + transform.forward;
        UpdateCheckPosition();
        GridCenterPos = _checkPosition + _offset;

        if (!_isHold)
        {
            CheckCanMake();
        }
    }

    #region 그리드 관련
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

    void CheckCanMake()
    {
        // 앞에 아무것도 없거나 레일이 있는 경우 가능
        RaycastHit hit;
        Debug.DrawRay(GridCenterPos + transform.up * 0.5f, Vector3.down * 2f, Color.red);   // 앞쪽
        _isCanFront = !Physics.Raycast(GridCenterPos + transform.up * 0.5f, Vector3.down, out hit, 2f, _frontObstacleLayerMask);
        if (_isCanFront)
        {
            Debug.Log("작업대 앞에 아무것도 없음");
        }

        // 양 옆에 돌과 나무 개수 세기
        RaycastHit leftHit, rightHit;
        Debug.DrawRay(GridCenterPos - transform.forward + transform.right + transform.up * 0.5f, Vector3.down * 2f, Color.red); // 우측
        Debug.DrawRay(GridCenterPos - transform.forward - transform.right + transform.up * 0.5f, Vector3.down * 2f, Color.red); // 좌측
        _isFindRight = Physics.Raycast(GridCenterPos - transform.forward + transform.right + transform.up * 0.5f, Vector3.down, out leftHit, 2f, _twoHandLayerMask);
        _isFindLeft = Physics.Raycast(GridCenterPos - transform.forward - transform.right + transform.up * 0.5f, Vector3.down, out rightHit, 2f, _twoHandLayerMask);

        _isFindSide = _isFindRight && _isFindLeft;
        if (_isFindSide)    // 양 옆에 재료 존재
        {
            IStack leftStackObject = null, rightStackObject = null;
            if (leftHit.collider.TryGetComponent<IStack>(out leftStackObject) && rightHit.collider.TryGetComponent<IStack>(out rightStackObject))
            {
                if (leftStackObject.StackType == Define.Stack.Wood)
                    _currentWoodCount = leftStackObject.Count;
                else if (leftStackObject.StackType == Define.Stack.Iron)
                    _currentIronCount = leftStackObject.Count;

                if (rightStackObject.StackType == Define.Stack.Wood)
                    _currentWoodCount = rightStackObject.Count;
                else if (rightStackObject.StackType == Define.Stack.Iron)
                    _currentIronCount = rightStackObject.Count;
            }

            if (_currentWoodCount >= 1 && _currentIronCount >= 1)
                _isCanSide = true;
            else
                _isCanSide = false;

            _isCanMake = _isCanFront && _isCanSide;

            if (_isCanMake)
            {
                leftStackObject.Pop();
                rightStackObject.Pop();
                Instantiate(_railPrefab, GridCenterPos, Quaternion.identity);
            }
        }
    }

    void AutoMakeRail()
    {
        // 이미 생성된 레일이 있으면 Push

        // 레일이 없으면 생성하고 Push
    }
}