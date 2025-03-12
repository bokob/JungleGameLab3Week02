using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    public static Managers Instance { get { Init(); return _instance; } }

    InputManager _input = new InputManager();
    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } }
    public static InputManager Input { get { return Instance._input; } }
    void Awake()
    {
        Init();
    }


    void Start()
    {
    }

    void Update()
    {
        // _input.Update();
        //_input.test();
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
            DontDestroyOnLoad(go);
            _instance = go.GetComponent<Managers>();
            
            Input.Init();
        }
    }
}