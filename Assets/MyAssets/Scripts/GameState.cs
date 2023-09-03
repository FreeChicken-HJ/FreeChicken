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
            Invoke("FactoryScenePlay",2f);
           
            
        }
        else if (isH)
        {
            ClickSound.Play();
           
            SetLoadingUI();
            Invoke("HouseScenePlay",2f);
            
        }
        else if (isCi)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CityScenePlay", 2f);
          
            
        }
        else if (isCa)
        {
            ClickSound.Play();
            SetLoadingUI();
            Invoke("CaveScenePlay", 2f);
         
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
