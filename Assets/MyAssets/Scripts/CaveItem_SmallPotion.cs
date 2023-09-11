using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CaveItem_SmallPotion : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI InteractionSmallPotionText;

    bool isMovePotion;
    public bool isInteraction;

    Vector3 vec = new Vector3(100f, 2.6500001f, -100f);

    void Start()
    {
        InteractionSmallPotionText.gameObject.SetActive(false);
    }
    void Update()
    {
        MovePotion();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInteraction = false;
            InteractionSmallPotionText.gameObject.SetActive(true);
            isMovePotion = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isInteraction = false;
            InteractionSmallPotionText.gameObject.SetActive(false);
            isMovePotion = false;
        }
    }
    void MovePotion()
    {
        if (Input.GetButtonDown("Interaction") && isMovePotion)
        {
            isInteraction = true;

            this.transform.position = Vector3.MoveTowards(this.transform.position, vec, 0.1f);
            InteractionSmallPotionText.gameObject.SetActive(false);
        }
    }
}
