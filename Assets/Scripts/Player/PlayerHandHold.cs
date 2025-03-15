using System;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 손에 넣을 것들 관리
public class PlayerHandHold : MonoBehaviour
{
    [Header("감지")]
    PlayerGrid _playerCheckInteraction;
    PlayerCheckHandHold _playerCheckHandHold;
    PlayerCheckEnvironment _playerCheckEnvironment;

    [Header("손")]
    Transform _handToolTransform;                   // 도구 손 위치
    Transform _handResourceTransform;               // 자원 손 위치

    [Header("상호작용")]
    [SerializeField] Transform _nearHandHoldTransform;
    IHandHold _nearHandHold;
    IHandHold _currentHandHold;
    UnityEngine.Object _currentHandHoldObject;          // 현재 손에 들고 있는 것
    public IHandHold CurrentHandHold { get { return _currentHandHold; } }

    [Header("손에 들고 있는 것")]
    Tool _currentTool;              // 현재 손에 들고 있는 도구
    Ingredient _currentIngredient;  // 현재 손에 들고 있는 재료
    public Tool CurrentTool { get { return _currentTool; } }
    public Ingredient CurrentIngredient { get { return _currentIngredient; } }

    void Awake()
    {
        init();
    }

    public void  init()
    {
        _playerCheckInteraction = GetComponent<PlayerGrid>();
        _playerCheckHandHold = GetComponent<PlayerCheckHandHold>();
        _playerCheckEnvironment = GetComponent<PlayerCheckEnvironment>();

        _handToolTransform = transform.Find("HandTool");
        _handResourceTransform = transform.Find("HandResource");

        Managers.Input.OnInteractEvent += InteractHandHold;
    }

    // 들 수 있는 물건과 상호작용
    public void InteractHandHold()
    {
        _nearHandHoldTransform = _playerCheckHandHold.NearHandHoldTransform;

        if (_nearHandHoldTransform != null) // 주변에 손에 들 수 있는 것이 있는 경우
            GetHandHold();
        else if(_nearHandHoldTransform == null && _currentHandHoldObject != null) // 주변 X, 손에 들고 있는 경우
            PutHandHold(); // 땅에 두기
    }

    // 손에 들 수 있는 것 줍기
    public void GetHandHold()
    {
        if (_currentHandHoldObject != null) // 이미 손에 무언가 들고 있는 경우
        {
            // 들고 있던 물건 내려놓기
            Transform currentHandHoldTransform = (Transform)_currentHandHoldObject;
            SetTransformHandHold(currentHandHoldTransform, null, _nearHandHoldTransform.position);
        }

        // 근처 손에 들 수 있는 것의 인터페이스 가져오기
        _nearHandHold = _nearHandHoldTransform.GetComponent<IHandHold>();
        if (_nearHandHold.HandHoldType == Define.HandHold.Tool)           // 도구인 경우
        {
            // 새로운 물건 손에 들기
            SetTransformHandHold(_nearHandHoldTransform, _handToolTransform, Vector3.zero);

            // 새로운 물건을 현재 손에 들고 있는 것으로 변경
            _currentHandHoldObject = _nearHandHoldTransform;
            _currentHandHold = _nearHandHoldTransform.GetComponent<IHandHold>();
            _nearHandHoldTransform = null;

            _currentTool = (Tool)_currentHandHold;
        }
        else if(_nearHandHold.HandHoldType == Define.HandHold.Ingredient)  // 재료인 경우
        {
            // 새로운 물건 손에 들기
            _nearHandHoldTransform.SetParent(_handResourceTransform);
            _nearHandHoldTransform.localPosition = Vector3.zero;
            _nearHandHoldTransform.localRotation = Quaternion.identity;

            // 새로운 물건을 현재 손에 들고 있는 것으로 변경
            _currentHandHoldObject = _nearHandHoldTransform;
            _currentHandHold = GetComponent<IHandHold>();
            _nearHandHoldTransform = null;
        }
    }
    public void PutHandHold()
    {
        if (!_playerCheckEnvironment.IsNearEnvironment && !_playerCheckHandHold.IsNearHandHold)
        {
            Debug.Log("버려버려");
            Transform currentHandHoldTransform = (Transform)_currentHandHoldObject;
            SetTransformHandHold(currentHandHoldTransform, null, _playerCheckInteraction.GridCenterPos);

            // 현재 손에 들고 있는 정보 지우기
            _currentHandHoldObject = null;  // 비우기
            _currentTool = null;
        }
    }

    // 손에 들 수 있는 물건 Transform 조정
    public static void SetTransformHandHold(Transform target, Transform parent, Vector3 position)
    {
        target.SetParent(parent);
        target.localPosition = position;
        target.localRotation = Quaternion.identity;
    }
}