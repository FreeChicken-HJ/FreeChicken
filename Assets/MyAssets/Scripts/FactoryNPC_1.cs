using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FactoryNPC_1 : MonoBehaviour
{
    public Slider NpcUI;
    public GameObject factoryUI;
    public FactoryPlayer player;
    public bool isEbutton;
    public GameObject Ebutton;
    public FactoryTextBlink text;
    public bool isNear;
    public GameObject npccam;
    public GameObject maincam;
    public GameObject npc;

    //public Animator animator;
    float t = 0;
    void Start()
    {
        Ebutton.SetActive(false);
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
       
    }
    void Update()
    {
        Check();
        if (Input.GetButton("E") && isEbutton)
        {
            Debug.Log("E");
            if (NpcUI.value <100f)
            {
                t += Time.deltaTime;
                NpcUI.value = Mathf.Lerp(0,100,t);
            }
            else
            {
                isEbutton = false;
                Destroy(text.gameObject);
                
                npccam.gameObject.SetActive(true);
                maincam.gameObject.SetActive(false);
                player.isTalk = true;
                player.isSlide = false;
                Destroy(this.gameObject);
                Destroy(Ebutton);
                player.transform.LookAt(npc.transform.position);
                factoryUI.gameObject.SetActive(true);
                
            }
        }
        if (Input.GetButtonUp("E"))
        {
            t = 0;
            NpcUI.value = 0;
        }
        
    }
    public void Check()
    {
        if (Physics.Raycast(this.transform.position, this.transform.forward, 2f, LayerMask.GetMask("Player")))
        {
            isNear = true;
            isEbutton = true;
            Ebutton.SetActive(true);
           
        }
        else if(Physics.Raycast(this.transform.position, this.transform.forward, 4f, LayerMask.GetMask("Player")))
        {
            isEbutton = false;
            isNear = false;
            Ebutton.SetActive(false);
            
        }


    }

}
