using UnityEngine;

public class PlayerCheckHandHold : MonoBehaviour
{
    [Header("상호작용")]
    float _checkRange = 0.15f;
    LayerMask _handHoldLayerMask;
    LayerMask _oneHandLayerMask = 1 << 6;
    LayerMask _twoHandLayerMask = 1 << 8;
    LayerMask _railLayerMask = 1 << 9;

    PlayerGrid _playerGrid;
    [SerializeField] Transform _nearHandHoldTransform;
    [field: SerializeField] public Transform NearHandHoldTransform { get { return _nearHandHoldTransform; } }

    bool _isNearHandHold = false;                                    // 주변에 손에 들 수 있는 것이 있는지 여부
    public bool IsNearHandHold { get { return _isNearHandHold; } set { } }


    void Start()
    {
        Init();
    }

    void Init()
    {
        _handHoldLayerMask = _oneHandLayerMask | _twoHandLayerMask | _railLayerMask;
        _playerGrid = GetComponent<PlayerGrid>();
    }

    void Update()
    {
        CheckClosestHandHold();
    }

    // 해당 그리드에 손으로 들 수 있는 것 찾기
    public void CheckClosestHandHold()
    {
        Collider[] colliders = Physics.OverlapSphere(_playerGrid.GridCenterPos, _checkRange, _handHoldLayerMask);


        if (colliders.Length >= 1)  // 가장 가까운 것 찾기
        {
            _isNearHandHold = true;

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

            _nearHandHoldTransform = target.transform; // 근처에 있는 손에 들 수 있는 것 할당
        }
        else
        {
            _isNearHandHold = false;
            _nearHandHoldTransform = null;
        }
    }

    void OnDrawGizmos()
    {
        if (_playerGrid != null)
        {
            // 플레이어 발 앞
            Gizmos.color = (_isNearHandHold) ? new Color(0, 1, 0) : new Color(1, 0, 0);
            Gizmos.DrawSphere(_playerGrid.GridCenterPos, _checkRange);
            //Gizmos.DrawCube(_playerGrid.GridCenterPos, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}