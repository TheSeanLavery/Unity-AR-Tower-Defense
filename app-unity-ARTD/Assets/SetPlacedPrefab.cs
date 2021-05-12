using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation.Samples;

public class SetPlacedPrefab : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        placer = GetComponent<PlaceMultipleObjectsOnPlane>();
    }

    private PlaceMultipleObjectsOnPlane placer;



    public GameObject TowerPrefab;
    public GameObject TowerPrefab2;

    public GameObject OriginPrefab;

    public GameObject Destination;

    public void SetTower()
    {
        placer.placedPrefab = TowerPrefab;
    }public void SetTower2()
    {
        placer.placedPrefab = TowerPrefab2;
    }
    public void SetOrigin()
    {
        placer.placedPrefab = OriginPrefab;
    }
    public void SetDestination()
    {
        placer.placedPrefab = Destination;
    }
}
