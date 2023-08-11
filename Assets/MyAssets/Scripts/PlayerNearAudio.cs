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
        newAudio.spatialBlend = 1.0f;  // 3D �Ҹ��� ����
        newAudio.minDistance = proximityDistance;
        newAudio.maxDistance = proximityDistance * 2f;  // �Ҹ��� �ִ� �Ÿ� ����
    }


    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.gameObject.transform.position);

        // �÷��̾ ������ ���� �� �Ҹ� ���
        if (distance <= proximityDistance && !isPlaying)
        {
            newAudio.Play();
            isPlaying = true;
        }
        // �÷��̾ �־��� �� �Ҹ� ����
        else if (distance > proximityDistance && isPlaying)
        {
            newAudio.Stop();
            isPlaying = false;
        }
    }
}
