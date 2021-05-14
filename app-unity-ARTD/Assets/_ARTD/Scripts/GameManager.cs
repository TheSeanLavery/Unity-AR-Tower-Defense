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
        source = GetComponent<AudioSource>();
    }

    public static GameManager Instance;


    public AudioClip Applause;
    private AudioSource source;

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
        InterWave,
        GameOver
    }

    public GameState gameState = GameState.Scanning;

    public void StartGame()
    {
        StopAllCoroutines();
        origin.StopAllCoroutines();
        gameState = GameState.Ready;
        StartCoroutine(GameStartCoroutine());

    }

    public GameObject WinScreen;
    
    private Coroutine roundRoutine;
    private bool finishedRound = false;
    public IEnumerator GameStartCoroutine()
    {
        SoftReset();
        
        if (CheckGameReady())
        {

            while (true)
            {

                if (CurrentWave < 3)
                {
                    finishedRound = false;
                    if (roundRoutine != null)
                    {
                        StopCoroutine(roundRoutine);
                    }
                    roundRoutine = StartCoroutine(RoundStartRoutine());

                    while (finishedRound != true)
                    {
                        yield return null;
                    }
                    
                }
                else
                {
                    TimeLeftText.text = "Thank you! :)";
                    WinScreen.SetActive(true);
                    source.PlayOneShot(Applause);
                    yield break;
                }
            }
        }
        else
        {

            GameNotReady();
        }

        yield return null;
    }

    IEnumerator RoundStartRoutine()
    {
        ResetRoundVariables();
        CurrentWave++;
        gameState = GameState.Countdown;
        TimeLeftText.text = "Wave Starts in: " +3;
        yield return new WaitForSeconds(1);
        TimeLeftText.text = "Wave Starts in: " +2;
        yield return new WaitForSeconds(1);
        TimeLeftText.text = "Wave Starts  in: " +1;
        yield return new WaitForSeconds(1);
        TimeLeftText.text = "START!";
        StartWave(CurrentWave); // gamestate is set inside Startwave
        yield return new WaitForSeconds(3);
        TimeLeftText.text = "";
        while (EnemiesLeft > 0 || origin.spawnedEnemies.Count > 0) yield return new WaitForSeconds(.1f);
        TimeLeftText.text = "Wave Complete";
        
        origin.StopAllCoroutines();
        
        yield return new WaitForSeconds(3);
        finishedRound = true;
    }

    public void ResetVariables()
    {
        CurrentWave = 0;
       
        currentScore = 0;
        currentCoins = 20;
        ResetRoundVariables();

    }

    public void ResetRoundVariables()
    {
        EnemiesLeft = 1;
        Health = 5;
        
    }

    public GameObject GameNotReadyUI;

    private void GameNotReady()
    {
        GameNotReadyUI.SetActive(true);
    }


    public void ResetGame()
    {
        SoftReset();
        Destroy(origin.gameObject);
        Destroy(destination.gameObject);
        
    }

    public void SoftReset()
    {
        origin.DestroyEnemies();
        DestroyAllTowers();
        ResetVariables();
        DestroyAllProjectiles();
    }

    public void DestroyAllTowers()
    {
        for (int i= SpawnedTowers.Count - 1; i >= 0; i--)
        {
            Tower t = SpawnedTowers[i];

            SpawnedTowers.RemoveAt(i);
            Destroy(t.gameObject);
        }
    }


    public List<GameObject> Projectiles = new List<GameObject>();

    public void DestroyAllProjectiles()
    {
        for (int i = Projectiles.Count - 1; i >= 0; i--)
        {
            var p = Projectiles[i];
            
            Projectiles.RemoveAt(i);
            Destroy(p);

        }
        
    }
    
    public int currentScore;
    public float defaultSpeedOfEnemies = 1;

    [SerializeField] public List<EnemyList> WaveList = new List<EnemyList>();
    public void StartWave(int wave)
    {
        gameState = GameState.Playing;
        origin.EnemyPrefabs = WaveList[wave-1].Enemies;
        origin.StartWave(wave-1);
        
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
    
    
    /// <summary>
    /// GAME OVER
    /// </summary>
    private void TriggerGameOver()
    {
        StopAllCoroutines();
        TimeLeftText.text = "GAME OVER";
        
        gameState = GameState.GameOver;
        GameOverPanel.SetActive(true);
    }

    public void DrawUI()
    {
        HealthText.text = "Lives: " + Health;
        WaveText.text = "Wave: " + CurrentWave;
        CoinsText.text = "Coins: " + currentCoins;
        RemainingText.text = "Remaining: " + EnemiesLeft;
        scoreText.text = "Score: " + currentScore;

        if (gameState == GameState.Playing)
        {
            
        }
        
    }

    public  TMP_Text HealthText;
    public TMP_Text scoreText;
    public  TMP_Text WaveText;
    public  TMP_Text CoinsText;
    public  TMP_Text RemainingText;
    public TMP_Text TimeLeftText;

    public GameObject GameOverPanel;
    public List<Tower> SpawnedTowers;

    public Rigidbody getClosestEnemy(Vector3 transformPosition)
    {
        float distance = float.MaxValue;
        Enemy closestEnemy = null;
        foreach (var e in origin.spawnedEnemies)
        {
            var d = Vector3.Distance(e.transform.position, transformPosition);
            if (d < distance)
            {

                distance = d;
                closestEnemy = e;
            }
        }

        if (closestEnemy == null)
        {
            return null;
        }
        return closestEnemy.rb;
    }
}

[Serializable]
public class EnemyList
{
    public List<Enemy> Enemies;
}
