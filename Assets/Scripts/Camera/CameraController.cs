using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    static CameraController _instance;
    public static CameraController Instance => _instance;

    Transform _target;

    void Start()
    {
        _target = FindAnyObjectByType<TrainController>().transform;
    }

    void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = _target.position.x;
            transform.position = newPosition;
        }
    }
}
