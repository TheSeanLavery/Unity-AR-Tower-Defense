using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bomb : ProjectileBase
{
  
    public GameObject ExplosionPrefab;

    public GameObject ExplosionParticles;
    public override void Start()
    {
        base.Start();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        { 
            Enemy e = other.collider.GetComponent<Enemy>();

           GameObject explosion =  Instantiate(ExplosionPrefab,transform.position,transform.rotation);

           Explosion b = explosion.GetComponent<Explosion>();


           Instantiate(ExplosionParticles, transform.position, transform.rotation);
           
           b.damage = damage;
            
           CancelInvoke();
           Destroy(gameObject);

        }
    }
}
