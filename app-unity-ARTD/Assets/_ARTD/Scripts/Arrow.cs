using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : ProjectileBase
{
    // Start is called before the first frame update
    public override void Start()
    {
         
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            var enemy = other.collider.GetComponentInParent<Enemy>();
            enemy.TakeDamge(damage);
            Destroy(gameObject);
            CancelInvoke();
        }
    }
}
