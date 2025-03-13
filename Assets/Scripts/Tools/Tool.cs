using UnityEngine;

public abstract class Tool : MonoBehaviour, IHandHold
{
    public Define.HandHold HandHoldType { get; protected set; }
    // set이 가능한 이유는 인터페이스의 규칙은 그대로 구현(여기서는 get을 그대로 구현함)
    // set을 추가한 것이므로 인터페이스 규칙에 위배되지 않아서 가능함 

    public Define.Tool ToolType { get; protected set; }

    protected static PlayerController _playerController;

    protected bool isUsing = false; // 도구 사용중인지 여부

    protected virtual void Init(){}

    public abstract void Use();

    public float GetDistancePlayer()  // 플레이어와의 거리 계산
    {
        return Vector3.Distance(transform.position, _playerController.transform.position);
    }
}