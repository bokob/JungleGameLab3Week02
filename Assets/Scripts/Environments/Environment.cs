using UnityEngine;

public abstract class Environment : MonoBehaviour
{
    protected int capacity = 3;
    [SerializeField] protected GameObject _resource;
    public Define.Environment EnvironmentType { get; protected set; }

    protected virtual void Init() { }

    public abstract void Deplete(Define.Tool toolType);
}
