using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaveInteraction_Door : MonoBehaviour
{
    public GameObject OpenDoorText;
    public GameObject donotOpenDoorText;
    bool isOpen;

    CaveScenePlayer player;
    CaveItem_Key key;

    public GameObject DadChick;
    public GameObject Thx;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        if (key != null)
        {
            key = GameObject.FindGameObjectWithTag("Key").GetComponent<CaveItem_Key>();
        }
    }

    void Update()
    {
        OpenDoor();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !player.hasKey)
        {
            Debug.Log("¹®¿¡ °¡±îÀÌ °¬´ß");
            donotOpenDoorText.SetActive(true);
            Invoke("CloseText", 2f);
            isOpen = true;
        }

        if(other.gameObject.tag.Equals("Player") && player.hasKey)
        {
            Debug.Log("¹®À» ¿­ ¼ö ÀÖ´ß");
            isOpen = true;
            OpenDoorText.SetActive(true);
        }
    }
    void CloseText()
    {
        donotOpenDoorText.SetActive(false);
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !player.hasKey)
        {
            donotOpenDoorText.SetActive(false);
            isOpen = false;
        }

        if(other.gameObject.tag.Equals("Player") && player.hasKey)
        {
            isOpen = false;
            OpenDoorText.SetActive(false);
        }
    }

    void OpenDoor()
    {
        if (Input.GetButtonDown("Interaction") && isOpen && player.hasKey)
        {

            OpenDoorText.SetActive(false);
            --player.keyCount;
            Destroy(gameObject);
            Thx.gameObject.SetActive(true);
            Invoke("Last", 3f);
            Debug.Log("¹®À» ¿­¾ú´ß");
            
        }
        else if(Input.GetButtonDown("Interaction") && isOpen &&!player.hasKey)
        {
            donotOpenDoorText.SetActive(true);
            Invoke("DestroyOpenDoorText", 1.5f);
            //Invoke("test", 1.5f);

        }
    }
    void Last()
    {
        Thx.gameObject.SetActive(false);
        DadChick.gameObject.SetActive(false);
    }
    void DestroyOpenDoorText()
    {
        OpenDoorText.gameObject.SetActive(false);
    }
}
