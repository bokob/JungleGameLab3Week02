using UnityEngine;

public interface IStack
{
    void Push();
    GameObject Pop();

    GameObject Top();
}