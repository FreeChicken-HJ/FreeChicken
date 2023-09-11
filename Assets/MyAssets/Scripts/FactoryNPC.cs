using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class FactoryNPC : MonoBehaviour
{
    public Slider NpcUI;
    public GameObject factoryUI;
    public GameObject GetMemoryUI;
    public FactoryPlayer player;
    public FactoryPlayer_2 player_2;
    public FactoryPlayer_3 player_3;
    public HouseScenePlayer player_4;
    
    public bool isEbutton;
    public GameObject Ebutton;
    public TextMeshProUGUI E;

   
    public bool isNear;
    public static bool isFinish;
    public bool isSet;
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera npcCam;
    public GameObject npc;
    public GameObject CamImage;
    public AudioSource getMemorySound;

   
    public float t;
    void Start()
    {
        
        Ebutton.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer>();
        player_2 = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer_2>();
        player_3 = GameObject.FindWithTag("Player").GetComponent<FactoryPlayer_3>();
        
        t = 0;
       
    }
  
    void Update()
    {
        
        if (Input.GetButton("E") && isEbutton)
        {
           
            E.color = Color.red;
            
            if (NpcUI.value <100f)
            {
                t += Time.deltaTime;
                NpcUI.value = Mathf.Lerp(0,100,t);
            }
            else
            {
                CamImage.SetActive(true);
                isEbutton = false;
                npc.SetActive(false);
                
                if (player != null)
                {
                    player.isStopSlide = true;
                    player.isSlide = false;
                    player.isTalk = true;
                }
                else if(player_2 != null)
                {
                    player_2.isStopSlide = true;
                    player_2.isSlide = false;
                    player_2.isTalk = true;
                }
                else if (player_3 != null)
                {
                    player_3.isStopSlide = true;
                    player_3.isSlide = false;
                    player_3.isTalk = true;
                   
                }
                
                getMemorySound.Play();
                GetMemoryUI.SetActive(true);
               
                Ebutton.SetActive(false);

                MemoryCount.memCount++;
                
                Invoke("ReStart", 2f);


            }
        }
        if (Input.GetButtonUp("E"))
        {
            E.color = Color.white;
            t = 0;
            NpcUI.value = 0;
        }
        
    }
   void ReStart()
    {
        
        CamImage.SetActive(false);
        GetMemoryUI.SetActive(false);
        isFinish = true;
       
        this.gameObject.SetActive(false);
       
        if (factoryUI != null)
        {
            factoryUI.gameObject.SetActive(true);
        }
        else if(factoryUI == null)
        {
            isNear = false;
            isEbutton = false;
            Ebutton.SetActive(false);
            mainCam.Priority = 2;
            npcCam.Priority = 1;
            if (player != null)
            {
                player.isTalk = false;
            }
            else if (player_2 != null)
            {
                player_2.isTalk = false;
            }
            else if(player_3 != null)
            {
                player_3.isTalk = false;
                
            }
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isNear = true;
            isEbutton = true;

            Ebutton.SetActive(true);
            mainCam.Priority = 1;
            npcCam.Priority = 2;
        }
    }
   
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNear = false;
            isEbutton = false;
            
            Ebutton.SetActive(false);
            mainCam.Priority = 2;
            npcCam.Priority = 1;
            
        }
    }

}
