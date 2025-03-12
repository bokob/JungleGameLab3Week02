using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager
{
    static InputManager _instance;
    public static InputManager Instance => _instance;

    Vector3 _moveDirection;
    bool _isDashPressed;
    bool _isInteractPressed;
    bool _isMove;

    public bool IsMove => _isMove;
    public Vector3 MoveDirection => _moveDirection;
    public bool IsDashPressed { get; private set; }
    public bool IsInteractPressed { get; private set; }

    PlayerInputSystem _playerInputSystem;
    InputAction _move;
    InputAction _dash;
    InputAction _interact;

    public void Init()
    {
        _playerInputSystem = new PlayerInputSystem();
        _move = _playerInputSystem.Player.Move;
        _dash = _playerInputSystem.Player.Dash;
        _interact = _playerInputSystem.Player.Interact;

        _move.Enable();
        _dash.Enable();
        _interact.Enable();

        // 이벤트 등록
        _move.performed += OnMove;
        _move.canceled += OnMove;

        _dash.performed += OnDash;
        _interact.performed += OnInteract;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            Vector2 moveInput = context.ReadValue<Vector2>();
            _moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
            _isMove = true;

            //Debug.Log("이동 키 누름");
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            _moveDirection = Vector3.zero;
            _isMove = false;

            //Debug.Log("이동 키 뗌");
        }
    }

    void OnDash(InputAction.CallbackContext context)
    {
        _isDashPressed = context.ReadValueAsButton();

        if (context.ReadValueAsButton())
        {
            Debug.Log("대시 누름");

            // TODO
            
            _isDashPressed = false;
        }
    }

    void OnInteract(InputAction.CallbackContext context)
    {
        _isInteractPressed = context.ReadValueAsButton();
        if (context.ReadValueAsButton())
        {
            Debug.Log("상호작용 누름");
            _isInteractPressed = false;
            
            // TODO
        }
    }

    public void Clear()
    {
        _move.Disable();
        _dash.Disable();
        _interact.Disable();
    }
}