using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ExtraEnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Vector3 Direction;
    [SerializeField] private float Speed;
    [SerializeField] private float Lifetime = 1.5f;
    [SerializeField] private GameObject Explosion;

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

        if (_startTime + Lifetime < Time.time)
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            LevelManager.Instance.IncreaseScore();
            Destroy(this.gameObject);
        }
    }
}
