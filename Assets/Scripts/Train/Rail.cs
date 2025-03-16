using UnityEngine;

public class Rail : MonoBehaviour
{
    bool _isUsed;
    public bool IsUsed { get { return _isUsed; } }

    bool _isEnd = false;

    public void SetUsed()
    {
        _isUsed = true;
    }
}