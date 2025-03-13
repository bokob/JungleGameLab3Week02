using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("컴포넌트")]
    Rigidbody _rb;

    [Header("움직임")]
    float _moveSpeed = 5f;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Move();
    }

    // 움직임(이동 및 회전)
    public void Move()
    {
        if (Managers.Input.IsMove)
        {
            Vector3 moveDirection = Managers.Input.MoveDirection;
            Vector3 moveVelocity = moveDirection * _moveSpeed;

            transform.LookAt(transform.position + moveVelocity);

            _rb.linearVelocity = new Vector3(moveVelocity.x, _rb.linearVelocity.y, moveVelocity.z);
        }
        else
        {
            _rb.linearVelocity = new Vector3(0, _rb.linearVelocity.y, 0);
        }
    }
}