using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float damage=1;
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            var enemy = other.collider.GetComponentInParent<Enemy>();
            enemy.TakeDamge(damage);
            Destroy(gameObject);
        }
    }
}
