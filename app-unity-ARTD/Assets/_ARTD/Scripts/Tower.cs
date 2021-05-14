using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //todo super hacky, can clean up later
        if (GameManager.Instance.currentCoins - cost < 0)
        {
            Destroy(gameObject);
            return;
        }

        GameManager.Instance.currentCoins -= cost;
        GameManager.Instance.SpawnedTowers.Add(this);
        StartCoroutine(ShootEveryInterval());
        
    }
    public enum damageType
    {
        piercing,aoe
    }

    public damageType type;

    public float Damage = 1;
    public float speed = 1;
    public float range = 1;
    public int cost = 1;
    public float ProjectileSpeed = 5;
    
    
    public void ShootTarget(Vector3 pos)
    {
        
        var projectile =  Instantiate(projectilePrefab, transform.position, transform.rotation);

        ProjectileBase projectileBase = projectile.GetComponent<ProjectileBase>();
        projectileBase.damage = Damage;
        projectile.transform.LookAt(pos);
        GameManager.Instance.Projectiles.Add((projectile));
        var rb = projectile.GetComponent<Rigidbody>();
    
        rb.velocity = rb.transform.forward * ProjectileSpeed;
    }
    

    public GameObject projectilePrefab;
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootEveryInterval()
    {
        while (true)
        {
            if (GameManager.Instance.gameState == GameManager.GameState.Playing)
            {
                yield return new WaitForEndOfFrame();
            
                Rigidbody target = GameManager.Instance.getClosestEnemy(transform.position);
                if(target == null) continue;

                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                Vector3 realTarget = target.position + (target.velocity * (distanceToTarget / ProjectileSpeed));
                if (target != null && distanceToTarget < range)
                {
                    ShootTarget(realTarget);
                    yield return new WaitForSeconds(speed);
                }
            }
           

           yield return new WaitForEndOfFrame();

        }
        
        yield return null;

    }

   
}