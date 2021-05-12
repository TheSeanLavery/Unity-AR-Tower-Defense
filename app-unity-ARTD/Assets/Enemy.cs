using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        speed = DefaultSpeed;
        Health = MaxHealth;
    }

    public int Health = 100;
    public int MaxHealth = 100;
    public float speed = 1;
    public float DefaultSpeed = 1;


    public Vector3 targetLocation;
    // Update is called once per frame
    void Update()
    {
        
    }
}
