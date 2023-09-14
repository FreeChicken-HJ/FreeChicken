using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using UnityEngine.SceneManagement;
using System.IO;
public class CitySceneToCaveScene : MonoBehaviour
{
    
    public GameObject pos;
    public CityScenePlayer player;
    public bool isContact;
    public bool isMove;
  
    public CinemachineVirtualCamera endCam;
    public AudioSource CarSound;

    
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CityScenePlayer>();
        
    }

    
    void Update()
    {
        if (player.isLast)
        {
            
            if (isContact)
            {
                
                endCam.Priority = 2;
                CarSound.Play();
                player.gameObject.transform.position = pos.transform.position;
                player.anim_2.SetBool("isRun",false);
                this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 4f, Space.World);
                
                Invoke("LoadCaveScene", 3f);
                
               
            }
        }

    }
    void LoadCaveScene()
    {

        GameSave.isCave = true;
        PlayerPrefs.SetInt("GoCave", GameSave.isCave ? 1 : 0);
        GameSave.Level = 4;
        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText("playerData.json", json);
        
        LoadSceneInfo.is2DEnterScene = true;
        PlayerPrefs.SetInt("SceneFactory_2", LoadSceneInfo.is2DEnterScene ? 1 : 0);
        LoadSceneInfo.LevelCnt = 2;
        SceneManager.LoadScene("LoadingScene");
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isContact = true;
        }
    }
}
