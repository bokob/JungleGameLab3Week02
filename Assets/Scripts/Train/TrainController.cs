using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    [Header("감지")]
    TrainCheckRail _trainCheckRail;

    [Header("상태")]
    [SerializeField] Define.TrainState _railState = Define.TrainState.Stop;

    [Header("이동")]
    [SerializeField] float _moveSpeed = 0;
    float faster = 0.05f;

    void Start()
    {
        Init();
    }

    void Update()
    {
        switch(_railState)
        {
            case Define.TrainState.Stop:
                CheckSide();
                CheckFront();
                break;
            case Define.TrainState.Move:
                CheckSide();
                CheckFront();
                if (!_trainCheckRail.IsFindFront && !_trainCheckRail.IsFindRight && !_trainCheckRail.IsFindLeft)
                {
                    Destroy(gameObject);
                    //Stop();
                }
                else
                    Move();
                break;
            case Define.TrainState.LeftRotate:
                Rotation(_railState);
                break;
            case Define.TrainState.RightRotate:
                Rotation(_railState);
                break;
        }
    }

    void Init()
    {
        _trainCheckRail = GetComponent<TrainCheckRail>();
        _railState = Define.TrainState.Move;

        _moveSpeed = 0;
        StartCoroutine(SpeedIncreaseCoroutine());
    }

    // 앞 검사하면서 이동
    void Move()
    {
        _trainCheckRail.CheckFront();
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }

    // 멈춤 상태로 변경
    void Stop()
    {
        _railState = Define.TrainState.Stop;
    }

    // 회전
    void Rotation(Define.TrainState state)
    {
        if (state == Define.TrainState.RightRotate)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0, 90, 0);
            Debug.Log("오른쪽 회전");
        }
        else if(state == Define.TrainState.LeftRotate)
        {
            transform.rotation = transform.rotation * Quaternion.Euler(0, -90, 0);
            Debug.Log("왼쪽 회전");
        }
        Stop();
    }

    // 좌우 검사
    void CheckSide()
    {
        _trainCheckRail.CheckSide();
        if (_trainCheckRail.IsFindRight)
            _railState = Define.TrainState.RightRotate;
        else if (_trainCheckRail.IsFindLeft)
            _railState = Define.TrainState.LeftRotate;
        else
            Move();
    }

    void CheckFront()
    {
        _trainCheckRail.CheckFront();
        if(_trainCheckRail.IsFindFront)
            _railState = Define.TrainState.Move;
    }

    IEnumerator SpeedIncreaseCoroutine()
    {
        SetMoreFaster();
        yield return new WaitForSeconds(30f);
    }

    public void SetMoreFaster()
    {
        _moveSpeed += faster;
    }
}