using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    static TitleUIManager _instance;
    public static TitleUIManager Instance { get { return _instance; } }

    Button _startBtn;
    Button _exitBtn;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            Init();
        }
    }

    public void Init()
    {
        _startBtn = transform.GetChild(0).GetComponent<Button>();
        _exitBtn = transform.GetChild(1).GetComponent<Button>();
        _startBtn.onClick.AddListener(OnClickStartBtn);
        _exitBtn.onClick.AddListener(OnClickExitBtn);
    }

    void OnClickStartBtn()
    {
        SceneManagerEX.Instance.SwitchScene(Define.Scene.InGameScene);
    }

    void OnClickExitBtn()
    {
        Application.Quit();
    }
}