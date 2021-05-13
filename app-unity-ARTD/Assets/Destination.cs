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
        Instance = this;

        GameManager.Instance.destination = Instance;
    }

    public static Destination Instance;

    // Update is called once per frame
    void Update()
    {
        
    }
}
