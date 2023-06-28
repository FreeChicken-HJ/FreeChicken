using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaveInteraction_Door : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI OpenDoorText;
    [SerializeField] TextMeshProUGUI donotOpenDoorText;
    bool isOpen;

    CaveScenePlayer player;
    CaveItem_Key key;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        OpenDoorText.gameObject.SetActive(false);
        donotOpenDoorText.gameObject.SetActive(false);
        key = GameObject.FindGameObjectWithTag("Key").GetComponent<CaveItem_Key>();

    }

    void Update()
    {
        OpenDoor();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("¹®¿¡ °¡±îÀÌ °¬´ß");
            OpenDoorText.gameObject.SetActive(true);
            isOpen = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            OpenDoorText.gameObject.SetActive(false);
            isOpen = false;
        }
    }

    void OpenDoor()
    {
        if (Input.GetButtonDown("Interaction") && isOpen && player.hasKey)
        {

            OpenDoorText.gameObject.SetActive(false);
            --player.keyCount;
            Destroy(gameObject);
            Debug.Log("¹®À» ¿­¾ú´ß");
        }
        else if(Input.GetButtonDown("Interaction") && isOpen &&!player.hasKey)
        {
            donotOpenDoorText.gameObject.SetActive(true);
            Invoke("test", 1.5f);

        }
    }

    void test()
    {
        donotOpenDoorText.gameObject.SetActive(false);
    }
}
