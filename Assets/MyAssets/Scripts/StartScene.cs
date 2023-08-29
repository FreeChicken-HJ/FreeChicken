using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    public GameObject loadingUI;

   
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           
        }
    }
  
    public void StartLoad()
    {
        loadingUI.SetActive(true);
        Invoke("Load", 1f);

    }
    public void Load()
    {


        SceneManager.LoadScene("Enter2DScene");
    }
}
