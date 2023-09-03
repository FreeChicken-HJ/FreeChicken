using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    public bool isChk;
    public static bool isFactory;
    public static bool isHouse;
    public static bool isCity;
    public static bool isCave;
    public GameObject Factory;
    public GameObject House;
    public GameObject City;
    public GameObject Cave;

    public AudioSource ShowSound;
    public ParticleSystem ShowParticle_1;
    public ParticleSystem ShowParticle_2;
    public ParticleSystem ShowParticle_3;

    private void Start()
    {
        Cursor.visible = true;
    }
    public void Load()
    {
        
        int intHouse = PlayerPrefs.GetInt("GoHouse");
        
        if (intHouse == 1)
        {
            isHouse = true;
        }
        else
        {
            isHouse = false;
        }
        
        int intCity = PlayerPrefs.GetInt("GoCity");

        if (intCity == 1)
        {
            isCity = true;
        }
        else
        {
            isCity = false;
        }
       
        int intCave = PlayerPrefs.GetInt("GoCave");
        
        if (intCave == 1)
        {
            isCave = true;
        }
        else
        {
            isCave = false;
        }
    }
    public void Update()
    {
        if (isHouse && !isChk)
        {
            House.SetActive(true);
            ShowSound.Play();

            ShowParticle_1.Play();
            
            isChk = true;
        }
        if (isCity && !isChk)
        {
            City.SetActive(true);
            ShowSound.Play();

            ShowParticle_2.Play();
            
            isChk = true;
        }
        if (isCave && !isChk)
        {
            Cave.SetActive(true);
            ShowSound.Play();
           ShowParticle_3.Play();
            isChk = true;
        }
    }
}