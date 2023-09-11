using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenDoor_House : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI opendoorText;
    [SerializeField] TextMeshProUGUI neardoorText;
    bool isOpen;

    void Start()
    {
        opendoorText.gameObject.SetActive(false);
        neardoorText.gameObject.SetActive(false);
    }
    void Update()
    {
        OpenDoor();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            neardoorText.gameObject.SetActive(true);
            isOpen = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            neardoorText.gameObject.SetActive(false);
            isOpen = false;
        }
    }

    void OpenDoor()
    {
        if (Input.GetButtonDown("Interaction") && isOpen)
        {
            gameObject.SetActive(false);
            neardoorText.gameObject.SetActive(false);
            opendoorText.gameObject.SetActive(true);
            Invoke("notshowtext", 1.5f);
        }
    }

    void notshowtext()
    {
        opendoorText.gameObject.SetActive(false);
    }
}
