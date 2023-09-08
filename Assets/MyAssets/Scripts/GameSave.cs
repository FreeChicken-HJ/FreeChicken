using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
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

    public static int Level = 1;
    public bool isExist;
    private void Start()
    {
        Cursor.visible = true;
        if (File.Exists("playerData.json"))
        {
            isExist = true;
            string jsonData = File.ReadAllText("playerData.json");
            PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);
            
            Level  = loadedData.LevelChk;
            Debug.Log(Level + "·¹º§");
        }

       
    }
    
    
    public void Load()
    {
        
        int intHouse = PlayerPrefs.GetInt("GoHouse");
        
        if (intHouse == 1)
        {
            isHouse = true;
            Level = 2;
        }
        else
        {
            isHouse = false;
        }
        
        int intCity = PlayerPrefs.GetInt("GoCity");

        if (intCity == 1)
        {
            isCity = true;
            Level = 3;
        }
        else
        {
            isCity = false;
        }
       
        int intCave = PlayerPrefs.GetInt("GoCave");
        
        if (intCave == 1)
        {
            isCave = true;
            Level = 4;
        }
        else
        {
            isCave = false;
        }
        
    }
    public void Update()
    {
        if (isExist)
        {
            if (Level == 2 && !isChk)
            {
                House.SetActive(true);
                ShowSound.Play();

                ShowParticle_1.Play();

                isChk = true;
            }
            if (Level == 3 && !isChk)
            {
                House.SetActive(true);
                City.SetActive(true);
                ShowSound.Play();

                ShowParticle_2.Play();

                isChk = true;
            }
            if (Level == 4 && !isChk)
            {
                House.SetActive(true);
                City.SetActive(true);
                Cave.SetActive(true);
                ShowSound.Play();
                ShowParticle_3.Play();
                isChk = true;
            }
        }
        else
        {
            if (isHouse && !isChk && Level == 2)
            {
                House.SetActive(true);
                ShowSound.Play();

                ShowParticle_1.Play();

                isChk = true;
            }
            if (isCity && !isChk && Level == 3)
            {
                City.SetActive(true);
                ShowSound.Play();

                ShowParticle_2.Play();

                isChk = true;
            }
            if (isCave && !isChk && Level == 4)
            {
                Cave.SetActive(true);
                ShowSound.Play();
                ShowParticle_3.Play();
                isChk = true;
            }
        }
    }
}