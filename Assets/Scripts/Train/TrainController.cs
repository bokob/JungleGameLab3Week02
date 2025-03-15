using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour
{
    static TrainController _instance;
    public static TrainController Instance => _instance;

    [SerializeField] float _moveSpeed = 5f;
    float SteerSpeed = 180;
    float BodySpeed = 5;
    public int Gap = 10;

    public GameObject BodyPrefab;

    List<GameObject> BodyParts = new List<GameObject>();
    List<Vector3> PositionsHistory = new List<Vector3>();

    void Start()
    {
        Init();
    }

    public void Init()
    {
        AddBox();
        AddBox();
        AddBox();
        AddBox();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            return;
        //TestMove();

        Move();
    }

    public void TestMove()
    {
        //transform.position += transform.forward * _moveSpeed * Time.deltaTime;

        //float steerDirection = Input.GetAxis("Horizontal");
        //transform.Rotate(Vector3.up * steerDirection * SteerSpeed * Time.deltaTime);

        // 움직임 기록
        PositionsHistory.Insert(0, transform.position);

        int index = 0;
        foreach(GameObject go in BodyParts)
        {
            Vector3 point = PositionsHistory[Mathf.Min(index * Gap, PositionsHistory.Count - 1)];
            Vector3 moveDirection = point - go.transform.position;
            go.transform.position += moveDirection * BodySpeed * Time.deltaTime;
            go.transform.LookAt(point);
            index++;
        }
    }



    void Move()
    {
        transform.position += transform.forward * _moveSpeed * Time.deltaTime;
    }



    void AddBox()
    {
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }

    void CheckRail()
    {

    }


}
