using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public Vector3 Direction;
    public float Speed;
    public float Lifetime = 1.5f;

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
