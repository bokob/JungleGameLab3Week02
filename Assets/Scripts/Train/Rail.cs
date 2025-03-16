using UnityEngine;

public class Rail : Ingredient
{
    [SerializeField] GameObject _prefab;
    [SerializeField] float _offset;

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
        StackType = Define.Stack.Rail;
        topPointer = transform.Find("TopPointer");
        prefab = _prefab;
        offset = _offset;
        Push();
    }
}