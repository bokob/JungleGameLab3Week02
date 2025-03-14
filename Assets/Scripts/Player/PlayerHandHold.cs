using System;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 손에 넣을 것들 관리
public class PlayerHandHold : MonoBehaviour
{
    PlayerGrid _playerCheckInteraction;
    PlayerCheckHandHold _playerCheckHandHold;

    [Header("손")]
    Transform _handToolTransform;                   // 도구 손 위치
    Transform _handResourceTransform;               // 자원 손 위치

    [Header("상호작용")]
    [SerializeField] Transform _nearHandHoldTransform;
    IHandHold _nearHandHold;
    IHandHold _currentHandHold;
    UnityEngine.Object _currentHandHoldObject;          // 현재 손에 들고 있는 것

    void Awake()
    {
        init();
    }

    public void  init()
    {
        _playerCheckInteraction = GetComponent<PlayerGrid>();
        _playerCheckHandHold = GetComponent<PlayerCheckHandHold>();

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
            currentHandHoldTransform.SetParent(null);
            currentHandHoldTransform.position = _nearHandHoldTransform.position;
            currentHandHoldTransform.rotation = _nearHandHoldTransform.rotation;   
        }

        // 근처 손에 들 수 있는 것의 인터페이스 가져오기
        _nearHandHold = _nearHandHoldTransform.GetComponent<IHandHold>();
        if (_nearHandHold.HandHoldType == Define.HandHold.Tool)           // 도구인 경우
        {
            // 새로운 물건 손에 들기
            _nearHandHoldTransform.SetParent(_handToolTransform);
            _nearHandHoldTransform.localPosition = Vector3.zero;
            _nearHandHoldTransform.localRotation = Quaternion.identity;

            // 새로운 물건을 현재 손에 들고 있는 것으로 변경
            _currentHandHoldObject = _nearHandHoldTransform;
            _currentHandHold = GetComponent<IHandHold>();
            _nearHandHoldTransform = null;
        }
        else if (_nearHandHold.HandHoldType == Define.HandHold.Resource)  // 자원인 경우
        {

        }
    }

    public void PutHandHold()
    {
        Debug.Log("버려버려");
        Transform currentHandHoldTransform = (Transform)_currentHandHoldObject;
        currentHandHoldTransform.SetParent(null);
        currentHandHoldTransform.position = _playerCheckInteraction.GridCenterPos;
        currentHandHoldTransform.rotation = Quaternion.identity;

        _currentHandHoldObject = null;  // 비우기
    }
}