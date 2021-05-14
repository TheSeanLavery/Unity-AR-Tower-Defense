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

    public Rigidbody rb;
    
    

    public float Health = 1;
    public float MaxHealth = 1;
    public float speed = 1;
    public float DefaultSpeed = 1;
    public int Value = 1;
    

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
            GameManager.Instance.TakeDamage();
            
            Destroy(gameObject);
            
        }
    }

    public GameObject explosionParticle;
    public void ExplodeEnemy()
    {
        GameManager.Instance.currentScore += Value *GameManager.Instance.CurrentWave;
        GameManager.Instance.currentCoins += Value;
        GameManager.Instance.origin.spawnedEnemies.Remove(this);
        Destroy(gameObject);

        Instantiate(explosionParticle, transform.position, transform.rotation); 

    }
    
    public void TakeDamge(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            ExplodeEnemy();
        }
    }
}
