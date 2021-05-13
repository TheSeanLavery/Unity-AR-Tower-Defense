using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    public static GameManager Instance;


    public Origin origin;
    public Destination destination;

    public int currentCoins = 0;
    public int CurrentWave;

    public int EnemiesLeft;

    public List<Enemy> AliveEnemies;
    
    public enum GameState 
    {
        Scanning,
        NotReady,
        Ready,
        Countdown,
        Playing,
        Finish,
        InterWave
    }

    public void StartGame()
    {

        if (CheckGameReady())
        {
            ResetVariables();
            StartWave(CurrentWave);
        }
        else
        {

            GameNotReady();
        }
    }

    public void ResetVariables()
    {
        CurrentWave = 0;
        EnemiesLeft = 0;
        currentScore = 0;
        currentCoins = 0;
        Health = 5;
        
    }

    public GameObject GameNotReadyUI;

    private void GameNotReady()
    {
        GameNotReadyUI.SetActive(true);
    }


    public void ResetGame()
    {
        origin.DestroyEnemies();
        Destroy(origin.gameObject);
        Destroy(destination.gameObject);
        ResetVariables();
    }
    private int currentScore;
    public float defaultSpeedOfEnemies = 1;

    [SerializeField] public List<EnemyList> WaveList = new List<EnemyList>();
    public void StartWave(int wave)
    {
        origin.EnemyPrefabs = WaveList[wave].Enemies;
        origin.StartWave(wave);
    }
    
    public bool gameReady = false;

    private int Health;

    // Update is called once per frame
    void Update()
    {
        DrawUI();
    }

    private bool CheckGameReady()
    {
        if (origin != null && destination != null)
        {
            return  true;

        }
        else
        {
            return  false;

        }
        
    }

    public void TakeDamage()
    {
        Health -= 1;
        if (Health == 0)
        {
            Health = 0;
            TriggerGameOver();
        }

        if (Health < 0) Health = 0;
    }

    private void TriggerGameOver()
    {
        GameOverPanel.SetActive(true);
    }

    public void DrawUI()
    {
        HealthText.text = "Lives: " + Health;
        WaveText.text = "Wave: " + CurrentWave;
        CoinsText.text = "Coins: " + currentCoins;
        RemainingText.text = "Remaining: " + EnemiesLeft;
        scoreText.text = "Score: " + currentScore;
    }

    public  TMP_Text HealthText;
    public TMP_Text scoreText;
    public  TMP_Text WaveText;
    public  TMP_Text CoinsText;
    public  TMP_Text RemainingText;

    public GameObject GameOverPanel;

}

[Serializable]
public class EnemyList
{
    public List<Enemy> Enemies;
}
