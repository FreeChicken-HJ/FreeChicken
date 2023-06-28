using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnVehicle : MonoBehaviour
{
    public GameObject car;
    public GameObject destroyPos;
    Vector3 curPosition;
    Vector3 curRotation;
    Quaternion rotation;
   
    void Start()
    {
        curPosition = transform.position;
        curRotation = transform.localEulerAngles;
        rotation = Quaternion.Euler(curRotation);
        
            
        InvokeRepeating("Spawn", 1f,5f);
        
    }
   
 
    void Spawn()
    {
        GameObject go = Instantiate(car, curPosition, rotation);
        
        
    }
 
}
