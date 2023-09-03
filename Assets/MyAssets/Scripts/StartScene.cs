using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    public GameObject loadingUI;
    public AudioSource ClickSound;
    public AudioSource BGM;
    void Start()
    {
        Cursor.visible = false;
        Invoke("SoundPlay", 1f);
    }
    void SoundPlay()
    {
        
        BGM.Play();
        Invoke("MouseView", 2.5f);
    }
    void MouseView()
    {
        Cursor.visible = true;
    }
    public void StartLoad()
    {
        ClickSound.Play();


        Cursor.visible = false;

        loadingUI.SetActive(true);
        Invoke("Load", 1f);

    }
    public void Load()
    {
        SceneManager.LoadScene("Start");
    }
}
