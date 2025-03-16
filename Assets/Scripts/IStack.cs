using UnityEngine;

public interface IStack
{
    public Define.Stack StackType { get; }

    void Push();
    GameObject Pop();

    GameObject Top();
}