using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Explode());
    }

    
    

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
        yield return null;
    }

    public float damage;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Enemy")
        {
            var enemy = other.collider.GetComponentInParent<Enemy>();
            enemy.TakeDamge(damage);
            Destroy(gameObject);
        }
    }
}
