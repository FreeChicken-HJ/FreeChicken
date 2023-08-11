using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNearAudio : MonoBehaviour
{
    public GameObject player;
    public AudioSource newAudio;
    public float proximityDistance = 10f;
    bool isPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if(newAudio == null)
        {
            newAudio = GetComponent<AudioSource>();
        }
        newAudio.spatialBlend = 1.0f;  // 3D 소리로 설정
        newAudio.minDistance = proximityDistance;
        newAudio.maxDistance = proximityDistance * 2f;  // 소리의 최대 거리 설정
    }


    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.gameObject.transform.position);

        // 플레이어가 가까이 있을 때 소리 재생
        if (distance <= proximityDistance && !isPlaying)
        {
            newAudio.Play();
            isPlaying = true;
        }
        // 플레이어가 멀어질 때 소리 중지
        else if (distance > proximityDistance && isPlaying)
        {
            newAudio.Stop();
            isPlaying = false;
        }
    }
}
