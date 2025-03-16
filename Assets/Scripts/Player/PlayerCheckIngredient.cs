using UnityEngine;

public class PlayerCheckIngredient : MonoBehaviour
{
    [Header("상호작용")]
    float _checkRange = 1.5f;
    LayerMask _ingredientLayerMask;
    LayerMask _twoHandLayerMask = 1 << 8;

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
        _ingredientLayerMask = _twoHandLayerMask;
        _playerGrid = GetComponent<PlayerGrid>();
    }

    void Update()
    {
        CheckClosestHandHold();
    }

    // 플레이어를 감싸는 8칸의 재료들이 레일을 만들만큼 존재하는지 확인
    public void CheckClosestHandHold()
    {
        Collider[] colliders = Physics.OverlapSphere(_playerGrid.GridCenterPos, _checkRange, _ingredientLayerMask);

        int wood = 0, iron = 0;
        foreach (Collider collider in colliders)
        {
            Define.Stack stackType = collider.GetComponent<Ingredient>().StackType;
            if (stackType == Define.Stack.Wood)
                wood++;
            else if (stackType == Define.Stack.Iron)
                iron++;
        }
    }

    void OnDrawGizmos()
    {
        if (_playerGrid != null)
        {
            // 플레이어 주위 8칸을 감싸는 구체
            Gizmos.color = new Color(0, 1, 1, 0.1f);
            Gizmos.DrawSphere(_playerGrid.GridCenterPos - transform.forward, _checkRange);
            //Gizmos.DrawCube(_playerGrid.GridCenterPos, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}
