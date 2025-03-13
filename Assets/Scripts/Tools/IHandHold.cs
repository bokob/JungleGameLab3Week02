using UnityEngine;

// 손에 붙들 수 있는 인터페이스
public interface IHandHold
{
    public Define.HandHold HandHoldType { get;}

    /// <summary>
    /// 플레이어가 갖기
    /// </summary>
    public void Get() { }

    /// <summary>
    /// 플레이어가 놓기
    /// </summary>
    public void Put() { }
}