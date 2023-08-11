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

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        key = GameObject.FindGameObjectWithTag("Key").GetComponent<CaveItem_Key>();
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
            isOpen = true;
        }

        if(other.gameObject.tag.Equals("Player") && player.hasKey)
        {
            Debug.Log("¹®À» ¿­ ¼ö ÀÖ´ß");
            isOpen = true;
            OpenDoorText.SetActive(true);
        }
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
            Debug.Log("¹®À» ¿­¾ú´ß");
            
        }
        else if(Input.GetButtonDown("Interaction") && isOpen &&!player.hasKey)
        {
            donotOpenDoorText.SetActive(true);
            Invoke("DestroyOpenDoorText", 1.5f);
            //Invoke("test", 1.5f);

        }
    }

    void DestroyOpenDoorText()
    {
        OpenDoorText.gameObject.SetActive(false);
    }
}
