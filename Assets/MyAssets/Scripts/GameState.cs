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
           
            SetLoadingUI();
            Invoke("FactoryScenePlay",5f);
           
            
        }
        else if (isH)
        {
            ClickSound.Play();
           
            SetLoadingUI();
            Invoke("HouseScenePlay",5f);
            
        }
        else if (isCi)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CityScenePlay", 5f);
          
            
        }
        else if (isCa)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CaveScenePlay", 5f);
         
        }
    }  
    void SetLoadingUI()
    {
        LoadingUI.SetActive(true);
        Cursor.visible = false;
        BGM.Stop();
      
    }
    public void FactoryScenePlay()
    {
        
        SceneManager.LoadScene("FactoryScene_1");
       
       
    }
    public void HouseScenePlay()
    {
       
        SceneManager.LoadScene("HouseScene1");

    }
    public void CityScenePlay()
    {
       
        SceneManager.LoadScene("CityScene");

    }
    public void CaveScenePlay()
    {
        SceneManager.LoadScene("CaveScene_Final");
    }
}
