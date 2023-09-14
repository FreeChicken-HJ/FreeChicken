using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadSceneInfo : MonoBehaviour
{
    public static bool isStartScene; // 1
    public static bool is2DEnterScene; // 2
    public static bool isFactory_1; // 3
    public static bool isFactory_2; // 4
    public static bool isFactory_3; // 5
    public static bool isHouse_1;   // 6
    public static bool isHouse_2;   // 7
    public static bool isCity;         //8
    public static bool isCave;        //9
    public static bool isEndScene;   //10


    public static int LevelCnt;
    public bool isChk;
    public void Load()
    {
        int SceneStart = PlayerPrefs.GetInt("SceneStart");
        if(SceneStart == 1)
        {
            isStartScene = true;
            LevelCnt = 1;
        }
        
        int Scene2D = PlayerPrefs.GetInt("Scene2D");
        if (Scene2D == 1)
        {
            is2DEnterScene = true;
            LevelCnt = 2;
        }
        int SceneFatory_1 = PlayerPrefs.GetInt("SceneFactory_1");
        if (SceneFatory_1 == 1)
        {
            isFactory_1 = true;
            LevelCnt = 3;
        }

        int intSceneFactory_2 = PlayerPrefs.GetInt("SceneFactory_2");

        if (intSceneFactory_2 == 1)
        {
            isFactory_2 = true;
            LevelCnt = 4;

        }
        int intSceneFactory_3 = PlayerPrefs.GetInt("SceneFactory_3");

        if (intSceneFactory_3 == 1)
        {
            isFactory_3 = true;
            LevelCnt = 5;

        }
        int SceneHouse_1 = PlayerPrefs.GetInt("SceneHouse_1");

        if (SceneHouse_1 == 1)
        {
            isHouse_1 = true;
            LevelCnt = 6;

        }
        int SceneHouse_2 = PlayerPrefs.GetInt("SceneHouse_2");

        if (SceneHouse_2 == 1)
        {
            isHouse_2 = true;
            LevelCnt = 7;

        }
        int SceneCity = PlayerPrefs.GetInt("SceneCity");

        if (SceneCity == 1)
        {
            isCity = true;
            LevelCnt = 8;

        }
        int SceneCave = PlayerPrefs.GetInt("SceneCave");

        if (SceneCave == 1)
        {
            isCave = true;
            LevelCnt = 9;

        }
        int SceneEnd = PlayerPrefs.GetInt("SceneEnd");

        if (SceneEnd == 1)
        {
            isEndScene = true;
            LevelCnt = 10;

        }
    }
    public void Update()
    {
        if (LevelCnt == 1 && !isChk)
        {
            Invoke("LoadStart", 2.5f);
            isChk = true;
        }
        if(LevelCnt == 2 && !isChk)
        {
            Invoke("Load2D", 2.5f);
            isChk=true;
        }
        if(LevelCnt == 3 && !isChk)
        {
            Invoke("LoadFac_1", 2.5f);
            isChk = true;
        }
        if (LevelCnt == 4 && !isChk)
        {
            
            Invoke("LoadFac_2", 2.5f);
            isChk = true;
        }
      
       
        if (LevelCnt == 5 && !isChk)
        {
            Invoke("LoadFac_3", 2.5f);
            isChk = true;
        }
        if (LevelCnt == 6 && !isChk)
        {
            Invoke("LoadHouse_1", 2.5f);
            isChk = true;
        }
        if (LevelCnt == 7 && !isChk)
        {
            Invoke("LoadHouse_2", 2.5f);
            isChk = true;
        }
      
        if (LevelCnt == 8 && !isChk)
        {
            Invoke("LoadCity", 2.5f);
            isChk = true;
        }
        if (LevelCnt == 9 && !isChk)
        {
            Invoke("LoadCave", 2.5f);
            isChk = true;
        }
        if (LevelCnt == 10 && !isChk)
        {
            Invoke("LoadEnd", 2.5f);
            isChk = true;
        }

    }

    void LoadStart()
    {
        SceneManager.LoadScene("Start");
    }
    void Load2D()
    {
        SceneManager.LoadScene("Enter2DScene");
    }
    void LoadFac_1()
    {
        SceneManager.LoadScene("FactoryScene_1");
    }
    void LoadFac_2()
    {
        SceneManager.LoadScene("FactoryScene_2");
    }
    void LoadFac_3()
    {
        SceneManager.LoadScene("FactoryScene_3");
    }
    void LoadHouse_1()
    {
        SceneManager.LoadScene("HouseScene1");
    }
    void LoadHouse_2()
    {
        SceneManager.LoadScene("HouseScene2");
    }
    void LoadCity()
    {
        SceneManager.LoadScene("CityScene");
    }
    void LoadCave()
    {
        SceneManager.LoadScene("CaveScene_Final");
    }
    void LoadEnd()
    {
        SceneManager.LoadScene("EndingScene");
    }
   
}
