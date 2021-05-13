using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        speed = DefaultSpeed;
        Health = MaxHealth;
        
        rb = GetComponent<Rigidbody>();
        
        
        //TODO turn this into a method inside GameManager
        GameManager.Instance.AliveEnemies.Add(this);
        
    }

    private Rigidbody rb;
    
    

    public float Health = 1;
    public float MaxHealth = 1;
    public float speed = 1;
    public float DefaultSpeed = 1;

    
    public void Kill()
    {
        //TODO turn this into a method inside GameManager
        GameManager.Instance.AliveEnemies.Remove(this);
    }

    public Vector3 targetLocation;
    // Update is called once per frame
    void Update()
    {
        var T = transform;
        T.LookAt(GameManager.Instance.destination.transform);
        
        rb.velocity = T.forward * speed;


    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Destination")
        {

            GameManager.Instance.origin.spawnedEnemies.Remove(this);
            GameManager.TakeDamage();
            
            Destroy(gameObject);
            
        }
    }
}
