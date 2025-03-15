using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.Port;

public class PlayerStack : MonoBehaviour
{
    int capacity = 3;

    [SerializeField] Transform _handResourceTransform;
    [SerializeField] Transform _topPointer;
    [SerializeField] List<Transform> _stack = new List<Transform>();
    float _offset = 0.5f;

    [SerializeField] GameObject testPrefab;

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameObject GO = Instantiate(testPrefab);
            Push(GO);
        }
        else if(Input.GetKeyDown(KeyCode.Y))
        {
            GameObject GO = Pop();
            Destroy(GO);
        }
    }

    void Init()
    {
        _handResourceTransform = transform.Find("HandResource");
        _topPointer = _handResourceTransform.Find("TopPointer");
    }

    #region 스택

    public void Push(GameObject GO)
    {
        if (_stack.Count < capacity)
        {
            _stack.Add(GO.transform);

            GO.transform.SetParent(_handResourceTransform);
            GO.transform.position = _topPointer.position;
            _topPointer.position += Vector3.up * _offset;
        }
    }

    public GameObject Top()
    {
        if (_stack.Count == 0)
            return null;
        return _stack[_stack.Count - 1].gameObject;
    }

    public GameObject Pop()
    {
        if (_stack.Count == 0)
            return null;
        GameObject GO = _stack[_stack.Count - 1].gameObject;
        _stack.RemoveAt(_stack.Count - 1);



        _topPointer.position -= Vector3.up * _offset;


        return GO;
    }
    #endregion
}
