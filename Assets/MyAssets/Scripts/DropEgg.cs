using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DropEgg : MonoBehaviour
{
    public FactoryPlayer factoryPlayer;
    public GameObject getEggCanvas;

    public bool isGetEgg;
    
    public GameObject Fix;
    public GameObject SavePoint;
    public AudioSource fixSound;
    public AudioSource getEggSound;
    // Start is called before the first frame update
    void Awake()
    {
        factoryPlayer = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
        getEggCanvas.gameObject.SetActive(false);
       
    }
   
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            getEggSound.Play();
            getEggCanvas.gameObject.SetActive(true);
            Fix.SetActive(true);
            SavePoint.SetActive(true);
            isGetEgg = true;
            fixSound.Play();
            Destroy(this.gameObject);
           
        }    

    }
   
}
