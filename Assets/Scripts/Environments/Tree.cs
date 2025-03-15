using UnityEngine;

public class Tree : Environment
{
    void Start()
    {
        Init();
    }

    protected override void Init()
    {
        EnvironmentType = Define.Environment.Tree;
    }

    public override void Deplete(Define.Tool toolType)
    {
        if (toolType == Define.Tool.Axe)
        {
            Debug.Log("벌목 벌목");

            transform.localScale = transform.localScale - Vector3.one * 0.2f;
            capacity--;

            if (capacity == 0)
            {
                Debug.Log("나무 파괴");
                Instantiate(_resource, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
