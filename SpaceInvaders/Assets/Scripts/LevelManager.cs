using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
 * The main game manager, controls the flow of each level and makes calls to update UI
 */
public class LevelManager : MonoBehaviour
{
    [SerializeField] private int CurrentLevel = 0;
    [SerializeField] private int StartLives = 3;
    [SerializeField] private LevelData[] Levels;
    [SerializeField] private GameObject[] EnemyTypes;
    
    [SerializeField] private GameObject LeftLane;
    [SerializeField] private GameObject RightLane;
    [SerializeField] private Transform EndLane;
    [SerializeField] private Transform UnitHolder;

    private bool _running = false;
    private int _score=0;
    private int _lives=0;
    private int _highscore = 0;
    private int _totalunits = 0;

    public static LevelManager Instance;
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
        int score = PlayerPrefs.GetInt("HighScore", 0);
        _highscore = score;
        UIManager.Instance.SetHighScore(score);
    }

    public void SetupLevel()
    {
        LevelData data = Levels[CurrentLevel];

        // Setup Lanes
        float xpos = data.LevelWidth / 2f;
        Vector3 pos = RightLane.transform.position;
        pos.x = xpos;
        RightLane.transform.position = pos;
        pos.x = -xpos;
        LeftLane.transform.position = pos;
        Vector3 scale = EndLane.localScale;
        scale.y = data.LevelWidth;
        EndLane.localScale = scale;

        // Setup Units
        float centeroffset = ((data.SpaceBetweenUnits * data.UnitsPerRow) / 2f) - (data.SpaceBetweenUnits / 2f);
        _totalunits = 0;
        for (int z = data.StartRowPosition; z < data.StartRowPosition+data.TotalRows; z++)
        {
            
            for(int x = 0; x < data.UnitsPerRow; x++)
            {
                Vector3 spawnpos = new Vector3(((float)x*data.SpaceBetweenUnits) - centeroffset, 0, (float)z*data.SpaceBetweenUnits);
                GameObject obj = Instantiate(EnemyTypes[Random.Range(0, EnemyTypes.Length)], spawnpos, Quaternion.Euler(0, 180, 0), UnitHolder);
                if(z==data.StartRowPosition) obj.GetComponent<UnitController>().StartFiring();
                _totalunits++;
            }
        }

    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        UIManager.Instance.ShowPause(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        UIManager.Instance.ShowPause(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        RemoveUnits();
        _running = true;
        _lives = 3;
        _score = 0;
        UIManager.Instance.SetLives(_lives);
        UIManager.Instance.SetScore(_score);
        UIManager.Instance.ShowPause(false);
        UIManager.Instance.ShowStart(false);
        UIManager.Instance.ShowRestart(false,"");
        MovementManager.Instance.Restart();
        CurrentLevel = 0;
        SetupLevel();
        MovementManager.Instance.Started = true;
        ExtraEnemyManager.Instance.StartSpawning();
    }

    public void NextLevel()
    {
        CurrentLevel++;
        if (CurrentLevel > Levels.Length - 1)
        {
            EndGame("You Win!");
        }
        else
        {
            MovementManager.Instance.Restart();
            SetupLevel();
            MovementManager.Instance.Started = true;
        }
    }

    public void IncreaseScore()
    {
        if (_running)
        {
            _score++;
            UIManager.Instance.SetScore(_score);
        }
    }

    public void LoseUnit()
    {
        _totalunits--;

        if (_totalunits == 0 && _lives!=0)
        {
            NextLevel();
        }
    }

    public void LoseLife()
    {
        if (_running)
        {
            _lives--;
            UIManager.Instance.SetLives(_lives);

            if (_lives == 0)
            {
                EndGame("Game Over");
            }
        }
    }

    public void EndGame(string endtext)
    {
        RemoveUnits();
        MovementManager.Instance.Started = false;
        if (_score > _highscore)
        {
            _highscore = _score;
            UIManager.Instance.SetHighScore(_highscore);
            PlayerPrefs.SetInt("HighScore", _highscore);
        }
        UIManager.Instance.ShowRestart(true, endtext);
        _running = false;
        ExtraEnemyManager.Instance.StopSpawning();
    }

    private void RemoveUnits()
    {
        foreach(Transform child in UnitHolder)
        {
            Destroy(child.gameObject);
        }
    }
}
