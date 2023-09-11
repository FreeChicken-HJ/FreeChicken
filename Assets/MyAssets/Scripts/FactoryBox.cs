using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBox : MonoBehaviour
{
    public bool isTrigger;
    
    public ParticleSystem particle;
    public AudioSource bombSound;
   
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&&!isTrigger)
        {
            isTrigger = true;
            particle.Play();
            bombSound.Play();
            
        }  
    }
}
