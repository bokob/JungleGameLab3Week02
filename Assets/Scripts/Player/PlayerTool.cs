using UnityEngine;

public class PlayerTool : MonoBehaviour
{
    [Header("감지")]
    PlayerGrid _playerGrid;

    [Header("손")]
    Transform _handToolTransform;                   // 도구 손 위치

    [Header("현재 도구")]
    Tool _currentTool;              // 현재 손에 들고 있는 도구
    public Tool CurrentTool { get { return _currentTool; } }
    bool isHoldTool = false;
    public bool IsHoldTool { get { return isHoldTool; } }

    void Start()
    {
        Init();
    }

    void Init()
    {
        _playerGrid = GetComponent<PlayerGrid>();

        _handToolTransform = transform.Find("HandTool");
    }

    // 도구 줍기
    public void Get(Transform toolTransform)
    {
        if (toolTransform == null)
            return;

        if (toolTransform.TryGetComponent<Tool>(out Tool tool))
        {
            isHoldTool = true;
            _currentTool = tool;
            PlayerHandHold.SetTransformHandHold(toolTransform, _handToolTransform, Vector3.zero);
        }
    }

    // 그리드 중심에 도구 놓기
    public void Put()
    {
        PlayerHandHold.SetTransformHandHold(_currentTool.transform, null, _playerGrid.GridCenterPos);
        isHoldTool = false;
        _currentTool = null;
    }
}