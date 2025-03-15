using UnityEngine;

public class Ingredient : MonoBehaviour, IHandHold
{
    public Define.HandHold HandHoldType { get; protected set; }
    public Define.Ingredient IngredientType { get; protected set; }

    protected virtual void Init() { }
}