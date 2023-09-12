using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI HighScore1;
    public TextMeshProUGUI HighScore2;
    public TextMeshProUGUI HighScore3;
    public TextMeshProUGUI Score;
    public TextMeshProUGUI Lives;
    public TextMeshProUGUI EndText;

    public Canvas Pause;
    public Canvas Start;
    public Canvas Restart;

    public static UIManager Instance { get; private set; }
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

    public void SetScore(int score)
    {
        Score.text = "Score: " + score.ToString();
    }

    public void SetHighScore(int score)
    {
        HighScore1.text = "High Score: " + score.ToString();
        HighScore2.text = "High Score: " + score.ToString();
        HighScore3.text = "High Score: " + score.ToString();
    }

    public void SetLives(int lives)
    {
        Lives.text = "Lives: " + lives.ToString();
    }

    public void ShowPause(bool show)
    {
        Pause.enabled = show;
    }

    public void ShowStart(bool show)
    {
        Start.enabled = show;
    }

    public void ShowRestart(bool show, string endtext)
    {
        EndText.text = endtext;
        Restart.enabled = show;
    }
}
