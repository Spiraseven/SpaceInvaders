using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Controlls input for ship movement and bullets, also takes damage to life if hit
 */
public class ShipController : MonoBehaviour
{
    [SerializeField] private float Speed = 10f;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private Transform FirePosition;
    [SerializeField] private float FireRate;
    [SerializeField] private GameObject ExplosionHit;

    private Vector2 _moveAmount;
    private Vector3 _velocity;
    private Rigidbody _rigidbody;
    private float _shotTime;
    private bool _pause = false;

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
        if (_pause) return;

        _moveAmount = value.Get<Vector2>();
    }

    public void OnFire()
    {
        if (_pause) return;

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
            Instantiate(ExplosionHit, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    public void PauseInput()
    {
        _pause = true;
    }

    public void ContinueInput()
    {
        _pause = false;
    }
}

