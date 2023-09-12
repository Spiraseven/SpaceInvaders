using UnityEngine;

/*
 * Controls an individual invaders shooting and reaction to triggers 
 */
public class UnitController : MonoBehaviour
{
    [SerializeField] private GameObject Explosion;
    [SerializeField] private Transform FirePosition;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float FireRate = 1f;
    [SerializeField] private bool ShouldFire = false;

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
            TriggerNextUnitToFire();
            LevelManager.Instance.IncreaseScore();
            LevelManager.Instance.LoseUnit();
        }
        else if (other.CompareTag("End"))
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            TriggerNextUnitToFire();
            LevelManager.Instance.LoseLife();
            LevelManager.Instance.LoseUnit();
        }
    }

    private void TriggerNextUnitToFire()
    {
        if (ShouldFire)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.forward, out hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
            {
                hit.transform.GetComponent<UnitController>().StartFiring();
            }
        }
    }
}
