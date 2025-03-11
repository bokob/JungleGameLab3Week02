using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static PlayerController _instance;
    public static PlayerController Instance => _instance;

    [Header("컴포넌트")]
    Rigidbody _rb;

    [Header("움직임")]
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
