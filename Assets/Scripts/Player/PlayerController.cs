using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static PlayerController _instance;
    public static PlayerController Instance => _instance;

    [Header("������Ʈ")]
    Rigidbody _rb;

    [Header("������")]
    [SerializeField] float _moveSpeed = 3f;
    [SerializeField] float _rotateSpeed = 5f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        
    }

    void Rotate()
    {
    }
}
