using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBox : MonoBehaviour
{
    public bool isTrigger;
    //FollowCam cameraShake;
    public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        //cameraShake = GameObject.Find("Main Camera").GetComponent<FollowCam>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTrigger)
        {
            this.gameObject.transform.rotation = Quaternion.Euler(-170, 0, 90);
            
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isTrigger = true;
            particle.gameObject.SetActive(true);
            //cameraShake.StartCoroutine(cameraShake.Shake(.5f, .5f));
        }  
    }
}
