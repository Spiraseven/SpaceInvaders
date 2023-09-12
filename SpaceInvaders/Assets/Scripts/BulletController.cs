using UnityEngine;

/*
 * Moves a bullet in a direction and destroys it after its lifetime
 */
public class BulletController : MonoBehaviour
{
    [SerializeField] private Vector3 Direction;
    [SerializeField] private float Speed;
    [SerializeField] private float Lifetime = 1.5f;

    private Vector3 _position;
    private float _startTime;

    private void Start()
    {
        _startTime = Time.time;
    }

    void Update()
    {
        _position = transform.position;
        _position += Direction * Speed * Time.deltaTime;
        transform.position = _position;

        if(_startTime+Lifetime<Time.time)
        {
            Destroy(this.gameObject);
        }
    }
}
