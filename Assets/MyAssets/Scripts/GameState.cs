using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameState : MonoBehaviour
{
    
    
   
    public bool isF;
    public bool isH;
    public bool isCi;
    public bool isCa;
    public AudioSource BGM;
    public GameObject LoadingUI;

    public AudioSource ClickSound;
    public void OnMouseDown()
    {
        if (isF)
        {
            ClickSound.Play();
            GetComponent<Renderer>().material.color = Color.blue;
            SetLoadingUI();
            FactoryScenePlay();
           
            
        }
        else if (isH)
        {
            ClickSound.Play();
            GetComponent<Renderer>().material.color = Color.blue;
            SetLoadingUI();
            HouseScenePlay();
            
        }
        else if (isCi)
        {
            ClickSound.Play();
            SetLoadingUI();
            CityScenePlay();
            GetComponent<Renderer>().material.color = Color.blue;
        }
        else if (isCa)
        {
            ClickSound.Play();
            SetLoadingUI();
            CaveScenePlay();
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }  
    void SetLoadingUI()
    {
        LoadingUI.SetActive(true);
        BGM.Stop();
      
    }
    public void FactoryScenePlay()
    {
        //DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("FactoryScene_1");
       
        Debug.Log("«√∑π¿Ã");
        //House.SetActive(true);
    }
    public void HouseScenePlay()
    {
        //DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("HouseScene1");

    }
    public void CityScenePlay()
    {
        //DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("CityScene");

    }
    public void CaveScenePlay()
    {
        SceneManager.LoadScene("CaveScene_Final");
    }
}
