using UnityEngine;

/*
 * Spawns a unit at SpawnRate that will move across the screen for extra points
 */
public class ExtraEnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private float SpawnRate = 6;

    private float _lastTime = 0f;
    private bool _start = false;

    public static ExtraEnemyManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _lastTime = Time.time + Random.Range(0, SpawnRate);
    }

    void Update()
    {
        if (_start)
        {
            if (_lastTime + SpawnRate < Time.time)
            {
                Instantiate(Enemy, transform.position, Quaternion.Euler(0, 270, 0));
                _lastTime = Time.time;
            }
        }
    }
    public void StartSpawning()
    {
        _start = true;
    }
    public void StopSpawning()
    {
        _start = false;
    }
}
