using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEX : MonoBehaviour
{
    static SceneManagerEX _instance;
    public static SceneManagerEX Instance { get { return _instance; } }

    public Define.Scene CurrentSceneType;

    public Action OnSceneAction;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;

            // 이벤트 등록
            SceneManager.sceneLoaded += OnSceneLoaded;
            OnSceneAction += StartScene;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 씬 로드 될때마다 호출
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        CurrentSceneType = (Define.Scene)Enum.Parse(typeof(Define.Scene), sceneName);
    }

    // 씬 시작할 때 호출 (씬 세팅)
    void StartScene()
    {
        switch (CurrentSceneType)
        {
            case Define.Scene.TitleScene:
                Debug.Log("타이틀 씬");
                break;
            case Define.Scene.InGameScene:
                break;
        }
    }

    public void StartTitleScene()
    {

    }

    public void SwitchScene(Define.Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }
}