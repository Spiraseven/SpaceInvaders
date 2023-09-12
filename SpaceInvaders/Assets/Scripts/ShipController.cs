using UnityEngine;
using UnityEngine.InputSystem;

public class ShipController : MonoBehaviour
{
    public float Speed = 10f;
    public GameObject Bullet;
    public Transform FirePosition;
    public float FireRate;

    private Vector2 _moveAmount;
    private Vector3 _velocity;
    private Rigidbody _rigidbody;
    private float _shotTime;

    void Start()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _velocity = Vector3.zero;
        _shotTime = 0;
    }

    private void FixedUpdate()
    {
        if (!Mathf.Approximately(_moveAmount.SqrMagnitude(), 0))
        {
            _velocity.x = _moveAmount.x * Speed;
            _rigidbody.velocity = _velocity;
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
        }
    }

    public void OnMove(InputValue value)
    {
        _moveAmount = value.Get<Vector2>();
    }

    public void OnFire()
    {
        if (_shotTime + FireRate < Time.time)
        {
            Instantiate(Bullet, FirePosition.position, Quaternion.identity);
            _shotTime = Time.time;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            LevelManager.Instance.LoseLife();
        }
    }
}
