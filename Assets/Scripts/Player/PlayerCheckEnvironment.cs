using UnityEngine;

public class PlayerCheckEnvironment : MonoBehaviour
{
    [Header("상호작용")]
    PlayerGrid _playerCheckInteraction;

    Transform _nearEnvironmentTransform;
    [field: SerializeField] public Transform NearEnvironmentTransform { get { return _nearEnvironmentTransform; } }

    LayerMask _environmentLayerMask = 1 << 7;

    bool _isNearEnvironment = false;     // 주변에 채집 가능한 환경이 있는지 여부
    public bool IsNearEnvironment { get { return _isNearEnvironment; } }

    float _checkRange = 0.3f;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _playerCheckInteraction = GetComponent<PlayerGrid>();
    }

    void Update()
    {
        CheckClosestHandHold();
    }

    // 해당 그리드에 손으로 들 수 있는 것 찾기
    public void CheckClosestHandHold()
    {
        Collider[] colliders = Physics.OverlapSphere(_playerCheckInteraction.GridCenterPos + transform.up * 0.5f, _checkRange, _environmentLayerMask);

        if (colliders.Length >= 1)  // 가장 가까운 것 찾기
        {
            _isNearEnvironment = true;

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
            _nearEnvironmentTransform = target.transform; // 근처에 있는 채집 가능한 환경 할당
        }
        else
        {
            _isNearEnvironment = false;
            _nearEnvironmentTransform = null;
        }
    }

    void OnDrawGizmos()
    {
        if(_playerCheckInteraction != null)
        {
            // 플레이어 앞
            Gizmos.color = (_isNearEnvironment) ? new Color(1, 1, 0) : new Color(0, 0, 1);
            Gizmos.DrawSphere(_playerCheckInteraction.GridCenterPos + transform.up * 0.5f, _checkRange);
        }
    }
}
