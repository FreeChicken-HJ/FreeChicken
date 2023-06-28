using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTest : MonoBehaviour
{
    //bool playAura; //파티클 제어 bool
    //public ParticleSystem particleObject; //파티클시스템
    public ParticleSystem particleObj;

    CaveScenePlayer player;

    void Start()
    {
        particleObj.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("불에 가까워짐!");
            particleObj.gameObject.SetActive(true);
            //playAura = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            particleObj.gameObject.SetActive(false);
            //playAura = false;
        }
    }

}
