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

    public void ShootNearest()
    {
        Rigidbody target = GameManager.Instance.getClosestEnemy(transform.position);
        
        if(target == null) return;
        var projectile =  Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.transform.LookAt(target.transform.position);
        GameManager.Instance.Projectiles.Add((projectile));
        var rb = projectile.GetComponent<Rigidbody>();

        rb.velocity = rb.transform.forward * 5;
    }
    

    public GameObject projectilePrefab;
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootEveryInterval()
    {
        while (GameManager.Instance.gameState == GameManager.GameState.Playing)
        {
            yield return new WaitForEndOfFrame();
            ShootNearest();
            yield return new WaitForSeconds(speed);
        }
        
        yield return null;

    }

   
}