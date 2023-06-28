using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class CitySceneToCaveScene : MonoBehaviour
{
    
    public GameObject pos;
    public CityScenePlayer player;
    public bool isContact;
    public bool isMove;

    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera endCam;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("CityCharacter").GetComponent<CityScenePlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isLast)
        {
            this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 4f, Space.World);
            if (isContact)
            {
                mainCam.Priority = 1;
                endCam.Priority = 2;
                player.gameObject.transform.position = pos.transform.position;
                player.anim.SetBool("isRun",false);

                // cave로 옮겨가기
            }
        }

    }
  
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isContact = true;
        }
    }
}
