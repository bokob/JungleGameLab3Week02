using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] TrainController _trainController;

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
        if(_trainController == null)
        {

        }
    }

    public void Restart()
    {
        // 게임 재시작
    }
}   