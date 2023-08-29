using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    FactoryPlayer factoryPlayer1;
    FactoryPlayer_2 factoryPlayer2;
    FactoryPlayer_3 factoryPlayer3;
    HouseScenePlayer housePlayer1;
    HouseScene2_Player housePlayer2;
    EvloutionPlayer evolutionPlayer;
    CityScenePlayer cityPlayer;
    CaveScenePlayer cavePlayer;


    public GameObject menuSet;
    public AudioSource ClickButtonAudio;


    void Start()
    {
        factoryPlayer1 = GameObject.FindGameObjectWithTag("Player").GetComponent<FactoryPlayer>();
        factoryPlayer2 = GameObject.FindGameObjectWithTag("Player").GetComponent<FactoryPlayer_2>();
        factoryPlayer3 = GameObject.FindGameObjectWithTag("Player").GetComponent<FactoryPlayer_3>();
        housePlayer1 = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
        housePlayer2 = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScene2_Player>();
        evolutionPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<EvloutionPlayer>();
        cityPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CityScenePlayer>();
        cavePlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();


    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if(factoryPlayer1 != null)
            {
                menuSet.SetActive(true);
                factoryPlayer1.mainAudio.Pause();
                factoryPlayer1.runAudio.Pause();
                Time.timeScale = 0f;
                factoryPlayer1.isTalk = true;
            }
            else if(factoryPlayer2 != null)
            {
                menuSet.SetActive(true);
                factoryPlayer2.BGM.Pause();
                Time.timeScale = 0f;
                factoryPlayer2.isTalk = true;
            }
            else if(factoryPlayer3!=null)
            {
                menuSet.SetActive(true);
                factoryPlayer3.BGM.Pause();
                Time.timeScale = 0f;
                factoryPlayer3.isTalk = true;
            }
            else if(housePlayer1!=null)
            {
                menuSet.SetActive(true);
                housePlayer1.mainAudio.Pause();
                housePlayer1.runAudio.Pause();
                Time.timeScale = 0f;
                housePlayer1.isTalk = true;
            }
            else if(housePlayer2!=null)
            {
                menuSet.SetActive(true);
                housePlayer2.mainAudio.Pause();
                housePlayer2.runAudio.Pause();
                Time.timeScale = 0f;
                housePlayer2.isTalk1 = true;
                housePlayer2.isTalk2 = true;
            }
            else if(evolutionPlayer!=null)
            {
                menuSet.SetActive(true);
                evolutionPlayer.mainAudio.Pause();
                evolutionPlayer.runAudio.Pause();
                Time.timeScale = 0f;
                evolutionPlayer.isTalk2 = true;
            }
            else if(cityPlayer!=null)
            {
                menuSet.SetActive(true);
                cityPlayer.BGM.Pause();
                Time.timeScale = 0f;
                cityPlayer.isAllStop= true;
            }
            else if(cavePlayer!=null)
            {
                menuSet.SetActive(true);
                cavePlayer.mainSound.Pause();
                Time.timeScale = 0f;
                cavePlayer.isTalk= true;
            }
        }
    }

    public void ContinueGame()
    {
        menuSet.SetActive(false);

        if (factoryPlayer1 != null)
        {
            factoryPlayer1.mainAudio.UnPause();
            factoryPlayer1.runAudio.UnPause();
            factoryPlayer1.isTalk = false;
        }
        else if (factoryPlayer2 != null)
        {
            factoryPlayer2.BGM.UnPause();
            factoryPlayer2.isTalk = false;
        }
        else if (factoryPlayer3 != null)
        {
            factoryPlayer3.BGM.UnPause();
            factoryPlayer3.isTalk = false;
        }
        else if (housePlayer1 != null)
        {
            housePlayer1.mainAudio.UnPause();
            housePlayer1.runAudio.UnPause();
            housePlayer1.isTalk = false;
        }
        else if (housePlayer2 != null)
        {
            housePlayer2.mainAudio.UnPause();
            housePlayer2.runAudio.UnPause();    
            housePlayer2.isTalk1 = false;
            housePlayer2.isTalk2 = false;
        }
        else if (evolutionPlayer != null)
        {
            evolutionPlayer.mainAudio.UnPause();
            evolutionPlayer.runAudio.UnPause();
            evolutionPlayer.isTalk2 = false;
        }
        else if (cityPlayer != null)
        {
            cityPlayer.BGM.UnPause();
            cityPlayer.isAllStop = false;
        }
        else if (cavePlayer != null)
        {
            cavePlayer.mainSound.UnPause();
            cavePlayer.isTalk = false;
        }
        
        Time.timeScale = 1;
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void ReplayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("HouseScene2");
    }

    public void ClickButtonSound()
    {
        ClickButtonAudio.Play();
    }
}
