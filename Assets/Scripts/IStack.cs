using UnityEngine;

public interface IStack
{
    public Define.Stack StackType { get; }
    public int Count { get; }

    void Push();
    GameObject Pop();

    GameObject Top();
}