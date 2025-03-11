using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers _instance;
    static Managers Instance { get { Init(); return _instance; } }

    //InputManager _inputManager = new InputManager();
    //public static InputManager Input { get { return Instance._inputManager; } }

    GameManager _game = new GameManager();
    public static GameManager Game { get { return Instance._game; } }
    void Awake()
    {
        Init();
    }


    void Start()
    {
        
    }

    void Update()
    {
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
        }
    }
}