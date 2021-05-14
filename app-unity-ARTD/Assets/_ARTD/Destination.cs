using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }

        if (GameManager.Instance.origin != null)
        {
            float distance = Vector3.Distance(GameManager.Instance.origin.transform.position,
                transform.position);
            if (distance
                < GameManager.Instance.defaultSpeedOfEnemies * MinTime)
            {
                Destroy(gameObject);
            
            }
            
        }
        
        Instance = this;
        GameManager.Instance.destination = Instance;
    }

    public float MinTime = 10;
    public static Destination Instance;

    // Update is called once per frame
    void Update()
    {
        
    }
}
