using UnityEngine;

/// <summary>
/// 플레이어 채집
/// </summary>
public class PlayerGather : MonoBehaviour
{
    PlayerHandHold _playerHandHold;
    PlayerCheckEnvironment _playerCheckEnvironment;

    [Header("상호작용")]
    [SerializeField] Transform _nearGatherTransform;
    bool isHaveTool = false;

    Environment _nearEnvironment;
    Tool _currentTool;

    [Header("애니메이션")]
    Animator _anim;
    int _gatherHash;

    void Awake()
    {
        _playerHandHold = GetComponent<PlayerHandHold>();
        _playerCheckEnvironment = GetComponent<PlayerCheckEnvironment>();

        _anim = GetComponent<Animator>();
        _gatherHash = Animator.StringToHash("isGather");
    }

    void Update()
    {
        if (_playerCheckEnvironment.NearEnvironmentTransform != null && _playerHandHold.CurrentTool != null) // 도구, 환경 O
        {
            Debug.Log("실행");
            InteractEnvironment();
        }
        else if (_playerCheckEnvironment.NearEnvironmentTransform == null)
            _anim.SetBool(_gatherHash, false);
    }

    void InteractEnvironment()
    {
        isHaveTool = Define.HandHold.Tool == _playerHandHold.CurrentHandHold.HandHoldType;
        _nearGatherTransform = _playerCheckEnvironment.NearEnvironmentTransform;
        if (isHaveTool)
        {
            _nearEnvironment = _nearGatherTransform.GetComponent<Environment>();
            _currentTool = _playerHandHold.CurrentTool;
            if ((_currentTool.ToolType == Define.Tool.Axe && _nearEnvironment.EnvironmentType == Define.Environment.Tree) ||
                (_currentTool.ToolType == Define.Tool.Pickaxe && _nearEnvironment.EnvironmentType == Define.Environment.Rock))
            {
                _anim.SetBool(_gatherHash, true);
            }
        }
        else
        {
            _currentTool = null;
        }
    }

    // 채집 애니메이션 이벤트
    public void OnGatherAnimationEvent()
    {
        if (_nearGatherTransform != null)
        {
            Environment environment = null;
            if (_nearGatherTransform.TryGetComponent<Environment>(out environment))
            {
                environment.Deplete(_playerHandHold.CurrentTool.ToolType);
            }
        }
        else
        {
            Debug.Log("주변 채집 환경 X");
        }
    }
}