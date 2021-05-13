using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Enemy enemy =  Instantiate(EnemyPrefabs[0],transform.position,transform.rotation);
        
        

    }

    public List<Enemy> EnemyPrefabs = new List<Enemy>();
    private IEnumerator WavePattern01()
    {
        while (GameManager.Instance.EnemiesLeft > 0)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1);    
        }
        
        yield return null;
    }
    private IEnumerator WavePattern02()
    {
        
        yield return null;
    }
}
