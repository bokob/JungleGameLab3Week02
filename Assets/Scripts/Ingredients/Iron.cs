using UnityEngine;

public class Iron : Ingredient
{
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        HandHoldType = Define.HandHold.Ingredient;
        IngredientType = Define.Ingredient.Iron;
    }
}