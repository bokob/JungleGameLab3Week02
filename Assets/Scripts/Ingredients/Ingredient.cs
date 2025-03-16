using UnityEngine;
using System.Collections.Generic;

public class Ingredient : MonoBehaviour, IHandHold, IStack
{
    public Define.HandHold HandHoldType { get; protected set; }
    public Define.Ingredient IngredientType { get; protected set; }

    List<Transform> _stack = new List<Transform>();
    public int Capacity { get { return _stack.Count; } protected set { } }

    protected Transform topPointer;
    protected GameObject prefab;

    protected virtual void Init() { }

    public GameObject GetGameObject() => gameObject;

    public void Push()
    {
        GameObject ingredient = Instantiate(prefab, topPointer.position, Quaternion.identity, transform);
        _stack.Add(ingredient.transform);
        Debug.Log("스택크기: " + _stack.Count);
        topPointer.position += Vector3.up * 0.3f;
    }

    public GameObject Pop()
    {
        if (_stack.Count == 0)
            return null;
        GameObject ingredient = _stack[_stack.Count - 1].gameObject;
        _stack.RemoveAt(_stack.Count - 1);
        Destroy(ingredient);
        topPointer.position -= Vector3.up * 0.3f;
        return ingredient;
    }

    public GameObject Top()
    {
        if (_stack.Count == 0)
            return null;
        GameObject ingredient = _stack[_stack.Count - 1].gameObject;
        return ingredient;
    }
}