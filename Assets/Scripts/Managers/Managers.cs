using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }

    void Awake()
    {
        Init();
    }

    static void Init()
    {
        if (_instance == null)
        {
            GameObject go = GameObject.Find("Managers");
            if (go == null) // 없는 경우
            {
                go = new GameObject { name = "Managers" };
                go.AddComponent<Managers>();
            }
            _instance = go.GetComponent<Managers>();
            //Game.SpawnPlayer((GameObject)Resources.Load(""));
            Input.Init();
        }
    }

    void OnDisable()
    {
        Input.Clear();
    }
}