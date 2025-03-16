using UnityEngine;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    static TitleUIManager _instance;
    public static TitleUIManager Instance { get { return _instance; } }

    Button _startBtn;

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
        _startBtn = GetComponentInChildren<Button>();
        _startBtn.onClick.AddListener(OnClickStartBtn);
    }

    void OnClickStartBtn()
    {
        SceneManagerEX.Instance.SwitchScene(Define.Scene.InGameScene);
    }
}