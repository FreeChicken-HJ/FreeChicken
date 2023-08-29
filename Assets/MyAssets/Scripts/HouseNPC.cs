using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HouseNPC : MonoBehaviour
{
    public Slider NpcUI;
   
    public GameObject GetMemoryUI;
    public HouseScenePlayer player;

    public bool isEbutton;
    public GameObject Ebutton;
    public TextMeshProUGUI E;


    public bool isNear;
    public static bool isFinish;
    public bool isSet;
   
    public GameObject npc;
   
    public AudioSource getMemorySound;


    public float t;
    void Start()
    {

        Ebutton.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponent<HouseScenePlayer>();
   
       
        t = 0;

    }

    void Update()
    {

        if (Input.GetButton("E") && isEbutton)
        {

            E.color = Color.red;

            if (NpcUI.value < 100f)
            {
                t += Time.deltaTime;
                NpcUI.value = Mathf.Lerp(0, 100, t);
            }
            else
            {
                
                isEbutton = false;
                npc.SetActive(false);

                

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

       
        GetMemoryUI.SetActive(false);
        isFinish = true;

        this.gameObject.SetActive(false);

        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isNear = true;
            isEbutton = true;

            Ebutton.SetActive(true);
          


        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isNear = false;
            isEbutton = false;

            Ebutton.SetActive(false);
           

        }
    }
}
