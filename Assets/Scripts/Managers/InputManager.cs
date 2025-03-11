using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] Vector2 _moveDirection;
    [SerializeField] bool _isDashPressed;
    [SerializeField] bool _isInteractPressed;
    public Vector2 MoveDirection => _moveDirection;
    public bool IsDashPressed { get; private set; }
    public bool IsInteractPressed { get; private set; } 

    void OnMove(InputValue inputValue)
    {
        _moveDirection = inputValue.Get<Vector2>();

        if (_moveDirection != Vector2.zero)
            Debug.Log("움직임 존재");
    }

    void OnDash(InputValue inputValue)
    {
        _isDashPressed = inputValue.isPressed;

        if (_isDashPressed)
            Debug.Log("대쉬 입력");
    }

    void OnInteract(InputValue inputValue)
    {
        _isInteractPressed = inputValue.isPressed;
        if (_isInteractPressed)
            Debug.Log("상호작용 입력");
    }
}
