using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public enum damageType
    {
        piercing,aoe
    }

    public damageType type;

    public float Damage = 1;
    public float speed = 1;
    public float range = 1;

    // Update is called once per frame
    void Update()
    {
        
    }
}