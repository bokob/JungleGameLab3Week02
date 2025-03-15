using System;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 손에 넣을 것들 관리
public class PlayerHandHold : MonoBehaviour
{
    [Header("감지")]
    PlayerCheckHandHold _playerCheckHandHold;
    PlayerCheckEnvironment _playerCheckEnvironment;

    [Header("물건 관리")]
    PlayerTool _playerTool;
    PlayerStack _playerStack;

    [Header("상호작용")]
    [SerializeField] Transform _nearHandHoldTransform;
    Transform _currentHandHoldTransform;                // 현재 손에 들고 있는 것의 Transform
    IHandHold _nearHandHold;
    IHandHold _currentHandHold;
    public IHandHold CurrentHandHold { get { return _currentHandHold; } }

    [Header("손에 들고 있는 것")]
    Ingredient _currentIngredient;  // 현재 손에 들고 있는 재료
    public Ingredient CurrentIngredient { get { return _currentIngredient; } }

    void Awake()
    {
        init();
    }

    void init()
    {
        _playerCheckHandHold = GetComponent<PlayerCheckHandHold>();
        _playerCheckEnvironment = GetComponent<PlayerCheckEnvironment>();
        _playerStack = GetComponent<PlayerStack>();

        _playerTool = GetComponent<PlayerTool>();

        Managers.Input.OnInteractEvent += InteractHandHold; // 이벤트 등록
    }

    // 들 수 있는 물건과 상호작용
    public void InteractHandHold()
    {
        _nearHandHoldTransform = _playerCheckHandHold.NearHandHoldTransform;
        if (_nearHandHoldTransform != null) // 주변에 들 수 있는 것이 있는 경우
        {
            if (_playerStack.IsEmpty && !_playerTool.IsHoldTool)    // 빈 손인 경우
            {
                GetHandHold();     // 새로운 물건 들기
            }
            else if (_playerTool.IsHoldTool) // 도구 든 경우
            {
                PutHandHold();     // 도구 내려놓기
                GetHandHold();     // 새로운 물건 들기

                Debug.Log("여긴가?");
            }
            else if (!_playerStack.IsEmpty) // 스택에 무언가 있는 경우 
            {
                PutHandHold();     // 땅에 전부 내려두기
                GetHandHold();     // 새로운 물건 들기
            }
        }
        else if (_currentHandHoldTransform != null && !_playerCheckEnvironment.IsNearEnvironment && !_playerCheckHandHold.IsNearHandHold)
        {// 주변 들 것 X, 손에 무언가 O, 주변 환경 X 
            PutHandHold(); // 땅에 두기
        }
    }

    // 손에 들 수 있는 것 줍기
    public void GetHandHold()
    {
        // 근처 손에 들 수 있는 것의 인터페이스 가져오기
        _nearHandHold = _nearHandHoldTransform.GetComponent<IHandHold>();
        if (_nearHandHold.HandHoldType == Define.HandHold.Tool)           // 도구인 경우
        {
            _playerTool.Get(_nearHandHoldTransform); // 도구 들기
        }
        else if (_nearHandHold.HandHoldType == Define.HandHold.Ingredient) // 재료인 경우
        {
            _playerStack.Push(_nearHandHoldTransform.gameObject);
        }
        else if(_nearHandHold.HandHoldType == Define.HandHold.Rail)       // 레일인 경우
        {
            _playerStack.Push(_nearHandHoldTransform.gameObject);
        }

        _currentHandHoldTransform = _nearHandHoldTransform;
        _nearHandHoldTransform = null;
    }

    public void PutHandHold()
    {
        if (_playerTool.IsHoldTool) // 도구 들고 있는 경우
        {
            _playerTool.Put(); // 도구 내려놓기
        }
        else if (!_playerStack.IsEmpty) // 스택에 무언가 있는 경우
        {
            /*
             TODO
            스택에 있는 것을 바닥에 내려놓기
             */

            _playerStack.Put();
        }
        // 현재 손에 들고 있는 정보 지우기
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