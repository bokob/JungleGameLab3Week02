using System;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 손에 넣을 것들 관리
public class PlayerHandHold : MonoBehaviour
{
    [Header("감지")]
    PlayerGrid _playerGrid;
    PlayerCheckHandHold _playerCheckHandHold;
    PlayerCheckEnvironment _playerCheckEnvironment;

    [Header("손")]
    Transform _handToolTransform;                   // 도구 손 위치
    Transform _handResourceTransform;               // 스택 손 위치

    [Header("상호작용")]
    [SerializeField] Transform _nearHandHoldTransform;
    Transform _currentHandHoldTransform;                // 현재 손에 들고 있는 것의 Transform
    IHandHold _nearHandHold;
    IHandHold _currentHandHold;
    public IHandHold CurrentHandHold { get { return _currentHandHold; } }

    [Header("손에 들고 있는 것")]
    Ingredient _currentIngredient;  // 현재 손에 들고 있는 재료
    public Ingredient CurrentIngredient { get { return _currentIngredient; } }

    bool _isHoldOneHand = false;
    bool _isHoldTwoHand = false;
    public bool IsHoldOneHand { get { return _isHoldOneHand; } }
    public bool IsHoldTwoHand { get { return _isHoldTwoHand; } }


    [Header("도구")]
    Tool _currentTool;  // 현재 손에 들고 있는 도구
    public Tool CurrentTool { get { return _currentTool; } }



    void Awake()
    {
        init();
    }

    void init()
    {
        _playerGrid = GetComponent<PlayerGrid>();
        _playerCheckHandHold = GetComponent<PlayerCheckHandHold>();
        _playerCheckEnvironment = GetComponent<PlayerCheckEnvironment>();

        _handToolTransform = transform.Find("HandTool");
        _handResourceTransform = transform.Find("HandResource");

        Managers.Input.OnInteractEvent += InteractHandHold; // 이벤트 등록
    }

    // 들 수 있는 물건과 상호작용
    public void InteractHandHold()
    {
        _nearHandHoldTransform = _playerCheckHandHold.NearHandHoldTransform;
        if (_nearHandHoldTransform != null) // 주변에 들 수 있는 것이 있는 경우
        {
            // 근처 손에 들 수 있는 것의 인터페이스 가져오기
            _nearHandHold = _nearHandHoldTransform.GetComponent<IHandHold>();
            if(_currentHandHoldTransform == null)   // 빈손
            {
                Get();
            }
            else // 빈손 X
            {
                Put();
                Get();
            }
        }
        else if (_currentHandHoldTransform != null && !_playerCheckEnvironment.IsNearEnvironment && !_playerCheckHandHold.IsNearHandHold)
        {// 주변 들 것 X, 손에 무언가 O, 주변 환경 X 
            Put();
            Debug.Log("땅에 두기요~");
        }
    }

    public void Get()
    {
        Transform parent = null;
        IHandHold handHold = _nearHandHoldTransform.GetComponent<IHandHold>();
        if (handHold.HandHoldType == Define.HandHold.OneHand) // 도구
        {
            parent = _handToolTransform;
            _isHoldOneHand = true;

            _currentTool = _nearHandHoldTransform.GetComponent<Tool>();
        }
        else if (handHold.HandHoldType == Define.HandHold.TwoHand) // 재료 OR 레일
        {
            parent = _handResourceTransform;
            _isHoldTwoHand = true;
        }
        _currentHandHoldTransform = _nearHandHoldTransform;
        _nearHandHoldTransform = null;
        SetTransformHandHold(_currentHandHoldTransform, parent, Vector3.zero);
    }

    public void Put()
    {
        SetTransformHandHold(_currentHandHoldTransform, null, _playerGrid.GridCenterPos);
        _currentTool = null;
        _isHoldOneHand = false;
        _isHoldTwoHand = false;
        _currentHandHoldTransform = null;
    }

    // 손에 들 수 있는 물건 Transform 조정
    public static void SetTransformHandHold(Transform target, Transform parent, Vector3 position)
    {
        target.SetParent(parent);
        target.localPosition = position;
        target.localRotation = Quaternion.identity;
    }
}