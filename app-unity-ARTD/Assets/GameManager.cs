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
    
    
    public void ResetGame()
    {
        Destroy(origin);
        Destroy(destination);
        CurrentWave = 0;
        EnemiesLeft = 0;
    }

    [SerializeField] public List<EnemyList> WaveList = new List<EnemyList>();
    public void StartWave(int wave)
    {
        origin.EnemyPrefabs = WaveList[0].Enemies;
    }
    
    public bool gameReady = false;
    // Update is called once per frame
    void Update()
    {
        if (origin != null && destination != null)
        {
            gameReady = true;

        }
        else
        {
            gameReady = false;
        }
    }
}

[Serializable]
public class EnemyList
{
    public List<Enemy> Enemies;
}
