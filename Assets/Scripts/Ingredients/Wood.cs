using System.Collections.Generic;
using UnityEngine;

public class Wood : Ingredient
{
    [SerializeField] GameObject _prefab;

    void Awake()
    {
        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Push();
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            Pop();
            Debug.Log("Pop");
        }
    }

    protected override void Init()
    {
        HandHoldType = Define.HandHold.TwoHand;
        IngredientType = Define.Ingredient.Wood;
        topPointer = transform.Find("TopPointer");
        prefab = _prefab;
        Push();
    }
}