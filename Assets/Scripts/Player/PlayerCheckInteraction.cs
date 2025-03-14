using UnityEngine;

public class PlayerCheckInteraction : MonoBehaviour
{
    [Header("상호작용")]
    [SerializeField] Transform _nearHandHoldTransform;
    [field: SerializeField] public Transform NearHandHoldTransform { get { return _nearHandHoldTransform; } }
    LayerMask _handHoldLayerMask;
    LayerMask _toolLayerMask = 1 << 6;
    LayerMask _environmentLayerMask = 1 << 7;
    LayerMask _resourceLayerMask = 1 << 8;

    [Header("채집")]
    bool isNearHandHold = false;        // 주변에 손에 들 수 있는 것이 있는지 여부
    bool isNearEnvironment = false;     // 주변에 채집 가능한 환경이 있는지 여부
    public bool IsNearHandHold { get { return isNearHandHold; } }
    public bool IsNearEnvironment { get { return isNearHandHold; } }

    [Header("시각화 및 탐색")]
    float _range = 0.15f;                // 탐색 범위
    [SerializeField] Grid _grid;
    [SerializeField] GameObject _cellIndicator;
    Vector3 _checkPosition;                             // 그리드를 구하고 싶은 바닥 좌표를 구하기 위한 레이캐스트 발사 지점
    Vector3 _offset = new Vector3(0.5f, 0f, 0.5f);      // 그리드 -> 월드 시, 좌하단 좌표를 반환하므로 offset 더해야 그리드 중심이 됨
    public Vector3 GridCenterPos { get; private set; }  // 그리드 중심 좌표

    [Header("디버깅용")]
    bool isFind = false;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _handHoldLayerMask = _toolLayerMask | _resourceLayerMask;
        GridSystem gridSystem = FindAnyObjectByType<GridSystem>();
        _grid = gridSystem.GetComponentInChildren<Grid>();
        _cellIndicator = gridSystem.GetComponentInChildren<CellIndicator>().gameObject;
    }

    void Update()
    {
        _checkPosition = transform.position + transform.forward * 0.6f;
        ShowGridIndicator();
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
        //Debug.Log("position: " + position);
        return position;
    }

    // 그리드 위치(중심)에 인디케이터 표시
    public void ShowGridIndicator()
    {
        // 검사하고 싶은 바닥 좌표 -> 해당하는 그리드 좌표 -> 월드 좌표
        Vector3 playerFrontPosition = GetSelectedMapPosition();             // 검사하고 싶은 바닥 좌표 구하기
        Vector3Int gridPosition = _grid.WorldToCell(playerFrontPosition);   // 바닥 좌표에 매칭되는 그리드 좌표
        _checkPosition = _grid.CellToWorld(gridPosition);                   // 그리드 좌표 -> 월드 좌표 (좌하단 모서리 좌표 반환)

        //Debug.Log("Cell to world: " + _checkPosition);

        _cellIndicator.transform.position = _checkPosition; // 인디케이터는 이미 x, z가 0.5씩 더해져 있음

        // 해당 그리드에 주울 수 있는 것들이 있는지 확인
        CheckClosestHandHold(_checkPosition);
    }

    // 해당 그리드에 손으로 들 수 있는 것 찾기
    public void CheckClosestHandHold(Vector3 centerPos)
    {
        //Debug.Log("그리드 중심: " + (centerPos + _offset));

        GridCenterPos = _checkPosition + _offset;
        Collider[] colliders = Physics.OverlapSphere(GridCenterPos, _range, _handHoldLayerMask);

        Debug.Log(colliders.Length);

        //for(int i = 0; i < colliders.Length; i++)
        //{
        //    Debug.Log(colliders[i].name);
        //}

        if (colliders.Length >= 1)
        {
            isFind = true;

            isNearHandHold = true;

            Collider target = null;
            float minDist = float.MaxValue;
            foreach (Collider collider in colliders)
            {
                float dist = Vector3.Distance(transform.position, collider.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    target = collider;
                }
            }

            // 근처에 있는 손에 들 수 있는 것 할당
            _nearHandHoldTransform = target.transform;
        }
        else
        {
            isFind = false;

            isNearHandHold = false;
            _nearHandHoldTransform = null;
        }
    }

    void OnDrawGizmos()
    {
        // 플레이어 발 앞
        Gizmos.color = (isFind) ? new Color(0, 1, 0) : new Color(1, 0, 0);
        Gizmos.DrawSphere(_checkPosition + _offset, _range);
        //Gizmos.DrawCube(testVector, new Vector3(1f, 0.1f, 1f));

        //// 플레이어 바로 앞 바닥
        //Gizmos.color = new Color(0, 0, 1);
        //Gizmos.DrawRay(_checkPosition, Vector3.down);
    }
}