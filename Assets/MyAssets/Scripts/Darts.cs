using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Darts : MonoBehaviour
{
    public TextMeshProUGUI getNoteText;
    public Image getNoteImage;

    HouseScenePlayer player;



    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
    }

    void Update()
    {
        //PickUpNote();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EggShoot")
        {
            Debug.Log("ÂÊÁö¸ÂÃã");
            PickUpNote();
        }
    }

    void PickUpNote()
    {
        gameObject.SetActive(false);
        getNoteImage.gameObject.SetActive(true);
        getNoteText.gameObject.SetActive(true);
    }

    public void notshowtext()
    {
        getNoteImage.gameObject.SetActive(false);
        getNoteText.gameObject.SetActive(false);
    }

}
