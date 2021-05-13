using System;
using System.Collections;
using System.Collections.Generic;
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
            CurrentWave = 0;
            EnemiesLeft = 0;
            StartWave(CurrentWave);
            
        }
        else
        {

            GameNotReady();
        }
      
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
        CurrentWave = 0;
        EnemiesLeft = 0;
        Health = 5;
    }

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

    public static void TakeDamage()
    {
        
    }
}

[Serializable]
public class EnemyList
{
    public List<Enemy> Enemies;
}
