using UnityEngine;

public class ResourceManager
{
    /// <summary>
    /// 특정 경로에 존재하는 오브젝트 반환
    /// </summary>
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    /// <summary>
    /// 경로에 있는 오브젝트 생성
    /// </summary>
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject GO = Load<GameObject>($"Prefabs/{path}");
        if(GO == null)
        {
            Debug.LogWarning("프리팹 불러오기 실패");
            return null;
        }
        return Object.Instantiate(GO, parent);
    }

    /// <summary>
    /// 오브젝트 파괴 (추후에 풀링매니저에 반환하게 변경하기)
    /// </summary>
    public void Destory(GameObject GO)
    {
        if(GO == null)
        {
            Debug.LogWarning("삭제하려는데 이미 널");
            return;
        }
        Destory(GO);
    }
}