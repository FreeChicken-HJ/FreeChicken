using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireTest : MonoBehaviour
{
    public ParticleSystem particleObj;

    void Start()
    {
        particleObj.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            particleObj.gameObject.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            particleObj.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            particleObj.gameObject.SetActive(false);
        }
    }
}
