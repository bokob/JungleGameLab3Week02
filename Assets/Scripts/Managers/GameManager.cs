using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] TrainController _trainController;

    bool _isGameOver = false;
    bool _isGameClear = false;

    void Awake()
    {
        if(_instance == null)
            _instance = this;

        Init();
    }

    void Init()
    {
        _trainController = FindAnyObjectByType<TrainController>();
    }

    void Update()
    {
        if (_isGameOver || _isGameClear)
            return;

        if(_trainController == null)
        {
            Debug.Log("게임 오버");
            _isGameOver = true;
            Invoke("GameOver", 3f);
            GameOver();
        }
    }

    public void GameOver()
    {
        SceneManagerEX.Instance.SwitchScene(Define.Scene.TitleScene);
    }
}   