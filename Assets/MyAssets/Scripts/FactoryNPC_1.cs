using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class FactoryNPC_1 : MonoBehaviour
{
    public Slider NpcUI;
    
    public HouseScene2_Player player;
    public bool isEbutton;
    public GameObject Ebutton;
    public TextMeshProUGUI E;

    public bool isNear;
    
    public CinemachineVirtualCamera npccam;
    public CinemachineVirtualCamera maincam;
    
    public GameObject npc;
    public GameObject Video;

    public AudioSource BGM;
    public AudioSource Memory;
    public GameObject TalkUI;
    public bool isFin;
    public GameObject Wall;
    //public Animator animator;
    float t = 0;
    void Start()
    {
        Ebutton.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<HouseScene2_Player>();
       
    }
    void Update()
    {
        
        if (Input.GetButton("E") && isEbutton)
        {
            Debug.Log("E");
            E.color = Color.red;
            if (NpcUI.value <100f)
            {
                t += Time.deltaTime;
                NpcUI.value = Mathf.Lerp(0,100,t);
            }
            else
            {
                isEbutton = false;
                Video.SetActive(true);

                E.color = Color.white;
                player.isTalk1 = true;
                Destroy(Ebutton);
                BGM.Stop();
                Memory.Play();
                Cursor.visible = true;
                Invoke("ReStart", 38f);
                isFin = true;
            }
        }
        if (Input.GetButtonUp("E"))
        {
            t = 0;
            E.color = Color.white;
            NpcUI.value = 0;
        }

       
    }
    public void ReStart()
    {
        if (isFin)
        {
            Video.SetActive(false);
          /*  maincam.Priority = 2;
            npccam.Priority = -5;*/
            Destroy(this.gameObject);
            BGM.Play();
            Wall.SetActive(false);
            Memory.Stop();
            TalkUI.SetActive(true);
            isFin = false;
            //player.isTalk1 = false;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            npccam.Priority = 100;
            maincam.Priority = 1;
            Ebutton.SetActive(true);
            isEbutton = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            npccam.Priority = 1;
            maincam.Priority = 10;
            Ebutton.SetActive(false);
            isEbutton = false;
        }
    }

}
