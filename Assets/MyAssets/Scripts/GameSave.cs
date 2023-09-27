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
    public GameObject Factory; // 1
    public GameObject House;   // 2
    public GameObject City;   // 3
    public GameObject Cave;   // 4

    public AudioSource ShowSound;
    public GameObject[] Objects;
    public ParticleSystem ShowParticle_1;
    public ParticleSystem ShowParticle_2;
    public ParticleSystem ShowParticle_3;

    public static int Level = 1;
    public bool isExist;
    private void Start()
    {
        Cursor.visible = true;
        /* if (File.Exists("playerData.json"))
         {
             isExist = true;
             string jsonData = File.ReadAllText("playerData.json");
             PlayerData loadedData = JsonUtility.FromJson<PlayerData>(jsonData);

             Level  = loadedData.LevelChk;

         }*/
        for (int i = 1; i < Level; i++)
        {
            Objects[i].SetActive(true);
        }

    }



    public void Update()
    {

        if (Level == 2 && !isChk)
        {
            House.SetActive(true);
            ShowSound.Play();

            ShowParticle_1.Play();
            SetFile();
            isChk = true;
        }
        if (Level == 3 && !isChk)
        {
            House.SetActive(true);
            City.SetActive(true);
            ShowSound.Play();

            ShowParticle_2.Play();
            SetFile();
            isChk = true;
        }
        if (Level == 4 && !isChk)
        {
            House.SetActive(true);
            City.SetActive(true);
            Cave.SetActive(true);
            ShowSound.Play();
            ShowParticle_3.Play();
            SetFile();
            isChk = true;
        }
    }
    public void SetFile()
    {
        PlayerData playerData = new PlayerData();
        playerData.LevelChk = Level;

        if (PlayerData.isEnglish)
        {
            playerData.isEng = true;
        }
        else if (!PlayerData.isEnglish)
        {
            playerData.isEng = false;
        }
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText("playerData.json", json);
        Debug.Log(Level + "현재저장");
        Debug.Log(playerData.LevelChk + "파일저장");
    }
}