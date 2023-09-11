using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearAudio : MonoBehaviour
{
    public GameObject player;
    public AudioSource newAudio;
    public float proximityDistance;
    bool isPlaying = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(newAudio == null)
        {
            newAudio = GetComponent<AudioSource>();
        }
        newAudio.spatialBlend = 1.0f;  
        newAudio.minDistance = proximityDistance;
        newAudio.maxDistance = proximityDistance * 2f; 
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.gameObject.transform.position);

        if (distance <= proximityDistance && !isPlaying)
        {
            newAudio.Play();
            isPlaying = true;
        }
        else if (distance > proximityDistance && isPlaying)
        {
            newAudio.Stop();
            isPlaying = false;
        }
    }
}
