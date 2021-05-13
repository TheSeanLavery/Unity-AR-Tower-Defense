using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    // Start is called before the first frame update
    public virtual void Start()
    {
        Invoke("DestroyDelayed",5);      
    }
    
    void DestroyDelayed()
    {
        Destroy(gameObject);
    }

    public float damage;

    // Update is called once per frame
    void Update()
    {
        
    }
}
