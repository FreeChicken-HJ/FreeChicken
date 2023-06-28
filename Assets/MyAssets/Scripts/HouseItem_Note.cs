using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HouseItem_Note : MonoBehaviour
{
    public TextMeshProUGUI nearNoteText;
    public TextMeshProUGUI getNoteText;
    public Image getNoteImage;

    bool isPickUpNote;

    HouseScenePlayer player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
        //nearNoteText.gameObject.SetActive(false);
        //getNoteText.gameObject.SetActive(false);
        //getNoteImage.gameObject.SetActive(false);
    }

    void Update()
    {
        PickUpNote();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("¬ ¡ˆø° ∞°±Ó¿Ã ∞¨¥ﬂ");
            nearNoteText.gameObject.SetActive(true);
            isPickUpNote = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            nearNoteText.gameObject.SetActive(false);
            isPickUpNote = false;
        }
    }

    void PickUpNote()
    {
        if (Input.GetButtonDown("Interaction") && isPickUpNote)
        {
            gameObject.SetActive(false);
            nearNoteText.gameObject.SetActive(false);
            getNoteImage.gameObject.SetActive(true);   
            getNoteText.gameObject.SetActive(true);
        }
    }

    public void notshowtext()
    {
        getNoteImage.gameObject.SetActive(false);
        getNoteText.gameObject.SetActive(false);
    }
}
