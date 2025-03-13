using System;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 손에 넣을 것들 관리
public class PlayerHandHold : MonoBehaviour
{
    [Header("현재 들고 있는 것")]
    public Define.HandHold CurrentHandHoldType { get; protected set; }  // 현재 손에 들고 있는 것의 종류
    IHandHold _currentHandHold;                 // 현재 손에 들고 있는 것
    
    [Header("상호작용")]
    bool _isPickup = false;                     // 손에 들고 있는지 여부
    bool IsPickup { get { return _isPickup; } }
    LayerMask _interactionLayerMask;            // 상호작용 가능한 레이어 마스크(도구, 환경, 자원)
    LayerMask _toolLayerMask = 1 << 6;
    LayerMask _environmentLayerMask = 1 << 7;
    LayerMask _resourceLayerMask = 1 << 8;
    float _range = 0.6f;         // 탐색 범위
    
    [Header("채집")]
    Tool _currentTool;                        // 현재 손에 들고 있는 도구
    bool isNearHandHold = false;              // 주변에 손에 들 수 있는 것이 있는지 여부
    bool isNearEnvironment = false;           // 주변에 채집 가능한 환경이 있는지 여부

    [Header("디버깅용")]
    bool isFind = false;

    void Awake()
    {
        init();
    }

    public void  init()
    {
        _isPickup = false;
        _interactionLayerMask = _toolLayerMask | _environmentLayerMask | _resourceLayerMask;
    }

    void Update()
    {
        CheckFrontOfFeet();
        if (isNearHandHold && !_isPickup && Managers.Input.IsInteractPressed) // 도구, 자원 줍기
        {
            Debug.Log("도구 or 자원 줍기");
        }
        else if(isNearEnvironment && _currentTool != null) // 채집
        {
            Debug.Log("자동 채집");
        }
        else if(_isPickup && Managers.Input.IsInteractPressed) // 손에 들고 있는 것 놓기
        {
            Debug.Log("손에 든거 놓기");
        }
    }

    // 발 앞에 있는 것 체크
    void CheckFrontOfFeet()
    {
        Debug.DrawRay(transform.position - transform.up * 0.95f, transform.forward * _range, Color.blue);
        Collider[] colliders = Physics.OverlapSphere(transform.position - transform.up * 0.95f, _range, _interactionLayerMask);

        if (colliders.Length > 0)
        {
            isFind = true;

            for(int i=0; i < colliders.Length; i++)
            {
                LayerMask layerMask = colliders[i].gameObject.layer;
                if (layerMask == _toolLayerMask || layerMask == _resourceLayerMask)
                {
                    isNearHandHold = true;
                }
                else if (layerMask == _environmentLayerMask)
                {
                    isNearEnvironment = true;
                }
            }
        }
        else
        {
            isFind = false;

            isNearHandHold = false;
            isNearEnvironment = false;
        }
    }

    public void GetObject(IHandHold handHold)
    {
        if (_currentHandHold == null && _isPickup == false)
        {
            switch (handHold.HandHoldType)
            {
                case Define.HandHold.Tool:
                    {
                        if (_currentTool == null)
                        {
                            _currentHandHold = handHold;
                            _currentTool = (Tool)handHold;
                            _currentTool.Use();
                        }
                    }
                    break;
                case Define.HandHold.Resource:
                    {

                    }
                    break;
                default:
                    Debug.Log("들 수 없는것");
                    break;
            }
        }
    }

    void PutObject()
    {

    }

    public void UseTool()
    {
        if (IsPickup && CurrentHandHoldType == Define.HandHold.Tool && isNearEnvironment)
        {
            Debug.Log("Use Tool");
            _currentTool.Use();
        }
    }

    void OnDrawGizmos()
    {
        // 플레이어 발 앞
        Gizmos.color = (isFind) ? new Color(0,1,0) : new Color(1, 0, 0);
        Gizmos.DrawSphere(transform.position - transform.up * 0.95f, _range);
    }
}