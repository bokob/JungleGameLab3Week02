using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{
    static CameraController _instance;
    public static CameraController Instance => _instance;

    Transform _target;

    void Start()
    {
        //_target = FindObjectOfType<TrainController>().transform;
    }

    void LateUpdate()
    {
        if (_target != null)
        {
            transform.LookAt(_target);
        }
    }
}
