using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Origin : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;

        GameManager.Instance.origin = Instance;
        
        
        waves.Clear();
        waves.Add(WavePattern01);
        waves.Add(WavePattern02);
    }

    private List<Func<IEnumerator>> waves = new List<Func<IEnumerator>>();


    public void StartWave(int wave)
    {
        Func<IEnumerator> wavePattern = waves[wave];
        if (wavePattern != null)
        {
            StartCoroutine(wavePattern());
        }
    }

    public static Origin Instance;

    private void Update()
    {
        if (GameManager.Instance.gameReady)
        {
            
            
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemy = (int)(EnemyPrefabs.Count * Random.Range(0f, 1f));
        Enemy enemy =  Instantiate(EnemyPrefabs[randomEnemy],transform.position,transform.rotation);
        spawnedEnemies.Add(enemy);
        GameManager.Instance.EnemiesLeft--;


    }

    public void DestroyEnemies()
    {
        for (int i = spawnedEnemies.Count-1; i >= 0; i--)
        {
            Enemy e = spawnedEnemies[i];
            spawnedEnemies.Remove(e);
            Destroy(e.gameObject);
        }
    }

    public List<Enemy> spawnedEnemies = new List<Enemy>();

    public List<Enemy> EnemyPrefabs = new List<Enemy>();
    
    private IEnumerator WavePattern01()
    {
        GameManager.Instance.EnemiesLeft = 20;
        while (GameManager.Instance.EnemiesLeft > 0)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1);    
        }
        
        yield return null;
    }
    private IEnumerator WavePattern02()
    {
        GameManager.Instance.EnemiesLeft = 20;
        
        while (GameManager.Instance.EnemiesLeft > 0)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1);    
        }
        yield return null;
    }
}
