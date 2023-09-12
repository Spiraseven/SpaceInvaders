using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public GameObject Explosion;
    public Transform FirePosition;
    public GameObject Bullet;
    public float FireRate = 1f;
    public bool ShouldFire = false;

    private float _lastShotTime = 0;

    private void Update()
    {
        if(ShouldFire)
        {
            if(_lastShotTime + FireRate < Time.time)
            {
                Instantiate(Bullet, FirePosition.position, Quaternion.identity);
                _lastShotTime = Time.time;
            }
        }
    }

    public void StartFiring()
    {
        _lastShotTime = Time.time;
        _lastShotTime += Random.Range(0,FireRate);
        ShouldFire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LeftLane"))
        {
            MovementManager.Instance.Side = 1;
            MovementManager.Instance.StartMoveForward();
        }
        else if (other.CompareTag("RightLane"))
        {
            MovementManager.Instance.Side = -1;
            MovementManager.Instance.StartMoveForward();
        }
        else if (other.CompareTag("Bullet"))
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            MovementManager.Instance.IncreaseSpeed();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            LevelManager.Instance.IncreaseScore();
            LevelManager.Instance.LoseUnit();
        }
        else if (other.CompareTag("End"))
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            LevelManager.Instance.LoseLife();
            LevelManager.Instance.LoseUnit();
        }
    }
}
