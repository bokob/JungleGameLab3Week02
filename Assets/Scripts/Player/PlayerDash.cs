using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _dashForce = 2500f;

    void Start()
    {
        Init();
    }

    void Init()
    {
        _rb = GetComponent<Rigidbody>();
        Managers.Input.OnDashEvent += Dash;
    }

    public void Dash()
    {
        _rb.AddForce(transform.forward * _dashForce, ForceMode.Acceleration);
        Debug.Log("~대시~");
    }

    void OnDestroy()
    {
        Managers.Input.OnDashEvent -= Dash;
    }
}