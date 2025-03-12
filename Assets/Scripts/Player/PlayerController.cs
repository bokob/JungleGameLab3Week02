using System;
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
    [SerializeField] float _dashForce = 100f;

    public Action<Vector3> OnMoveAction;
    void Awake()
    {
        if(_instance == null)
            _instance = this;
        else
            Destroy(gameObject);

        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (Managers.Input.IsMove)
        {
            Vector3 moveDirection = Managers.Input.MoveDirection;
            Vector3 moveVelocity = moveDirection * _moveSpeed;
            _rb.linearVelocity = new Vector3(moveVelocity.x, _rb.linearVelocity.y, moveVelocity.z);
        }
        else
        {
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
        }
    }

    public void Rotate()
    {

    }

    public void Dash(Vector3 moveDirection)
    {
        Vector3 dashDirection = moveDirection * _dashForce;
        _rb.AddForce(dashDirection, ForceMode.Impulse);
    }

    public void Interact()
    {

    }
}
