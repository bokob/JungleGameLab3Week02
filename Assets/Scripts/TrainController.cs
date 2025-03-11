using UnityEngine;

public class TrainController : MonoBehaviour
{
    static TrainController _instance;
    public static TrainController Instance => _instance;

    [SerializeField] float _moveSpeed = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.position += Vector3.right * _moveSpeed * Time.deltaTime;
    }
}
