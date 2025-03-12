using UnityEngine;
using UnityEngine.InputSystem;

public class TestInput : MonoBehaviour
{
    public Vector2 testInput;

    void OnTestMove(InputValue inputValue)
    {
        testInput = inputValue.Get<Vector2>();
        Debug.Log("testInput: " + testInput);
    }

}
