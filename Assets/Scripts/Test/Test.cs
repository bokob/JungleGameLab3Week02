using UnityEngine;

public class Test : MonoBehaviour, ITest
{
    ITest _test;
    [SerializeField] Test me;

    void Start()
    {
        _test = GetComponent<ITest>();
        me = (Test)_test;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            _test.TestDebug();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            me.OwnTest();
        }
    }

    public void TestDebug()
    {
        Debug.Log("Test");
    }

    public void OwnTest()
    {
        Debug.Log("OwnTest");
    }
}
