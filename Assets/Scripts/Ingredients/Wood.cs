using UnityEngine;

public class Wood : Ingredient
{
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        HandHoldType = Define.HandHold.Ingredient;
        IngredientType = Define.Ingredient.Wood;
    }
}