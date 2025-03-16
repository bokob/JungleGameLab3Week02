using UnityEngine;
using System.Collections.Generic;

public class Iron : Ingredient
{
    [SerializeField] GameObject _prefab;

    void Awake()
    {
        Init();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    Push();
        //}
        //else if(Input.GetKeyDown(KeyCode.Y))
        //{
        //    Pop();
        //    Debug.Log("Pop");
        //}
    }

    protected override void Init()
    {
        HandHoldType = Define.HandHold.TwoHand;
        StackType = Define.Stack.Iron;
        topPointer = transform.Find("TopPointer");
        prefab = _prefab;
        Push();
    }
}