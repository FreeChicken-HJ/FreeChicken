using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveScene_Sense : MonoBehaviour
{
    Obstacle_Cave obstacle;

    public bool isSense;
    public bool ContactSense;

    void Start()
    {
        obstacle = GameObject.FindGameObjectWithTag("Obstacle").GetComponent<Obstacle_Cave>();
    }

    void Update()
    {
        obstacle.deguldegul();
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ContactSense)
            isSense = true;
    }
}
