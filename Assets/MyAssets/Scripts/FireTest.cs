using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FireTest : MonoBehaviour
{
    //bool playAura; //��ƼŬ ���� bool
    //public ParticleSystem particleObject; //��ƼŬ�ý���
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
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("�ҿ� �������!");
            particleObj.gameObject.SetActive(true);
            //playAura = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            particleObj.gameObject.SetActive(true);
        }
    }

    //void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        particleObj.gameObject.SetActive(false);
    //        //playAura = false;
    //    }
    //}
}
