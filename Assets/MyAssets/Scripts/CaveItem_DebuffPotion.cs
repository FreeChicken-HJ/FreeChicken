using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CaveItem_DebuffPotion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nearPotionItemText;
    [SerializeField] TextMeshProUGUI pickUpPotionItemText;

    bool isPickUp;
    public bool reversalPotion;

    CaveScenePlayer player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        pickUpPotionItemText.gameObject.SetActive(false);
        nearPotionItemText.gameObject.SetActive(false);
    }
    void Update()
    {
        PickUp();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            Debug.Log("Æ÷¼Ç¿¡ °¡±îÀÌ °¬´ß");
            reversalPotion = false;
            nearPotionItemText.gameObject.SetActive(true);
            isPickUp = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            reversalPotion = false;
            nearPotionItemText.gameObject.SetActive(false);
            isPickUp = false;
        }
    }
    void PickUp()
    {
        if (Input.GetButtonDown("Interaction") && isPickUp)
        {
            Debug.Log("Æ÷¼ÇÀ» ¾ò¾ú´ß");
            reversalPotion = true; 
            gameObject.SetActive(false);
            nearPotionItemText.gameObject.SetActive(false);
            pickUpPotionItemText.gameObject.SetActive(true);
            
            Invoke("notshowtext", 1.5f);
        }
    }
    void notshowtext()
    {
        pickUpPotionItemText.gameObject.SetActive(false);
    }
}
