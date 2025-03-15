using UnityEngine;

public class Rock : Environment
{
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        EnvironmentType = Define.Environment.Rock;
    }

    public override void Deplete(Define.Tool toolType)
    {
        if(toolType == Define.Tool.Pickaxe)
        {
            Debug.Log("채광 채광");

            transform.localScale = transform.localScale - Vector3.one * 0.2f;
            capacity--;

            if (capacity == 0)
            {
                Debug.Log("암석 파괴");
                Instantiate(_resource, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
