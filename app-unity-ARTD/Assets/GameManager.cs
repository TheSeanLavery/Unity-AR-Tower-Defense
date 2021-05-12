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


    public GameObject origin;
    public GameObject destination;

    public int CurrentWave;

    public int EnemiesLeft;
    
    
    
    
    
    
    public void ResetGame()
    {
        Destroy(origin);
        Destroy(destination);
        CurrentWave = 0;
        EnemiesLeft = 0;
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
