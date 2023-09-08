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
    public GameObject LoadingUI;

    //public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera endCam;
    public AudioSource CarSound;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CityScenePlayer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isLast)
        {
            //this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 4f, Space.World);
            if (isContact)
            {
                //mainCam.Priority = 1;
                endCam.Priority = 2;
                CarSound.Play();
                player.gameObject.transform.position = pos.transform.position;
                player.anim_2.SetBool("isRun",false);
                this.gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 4f, Space.World);
                //isContact = false;
                Invoke("LoadCaveScene", 3f);
                
                // cave로 옮겨가기
            }
        }

    }
    void LoadCaveScene()
    {
       
        LoadingUI.SetActive(true);
        Invoke("Last", 2f);
    }
    void Last()
    {
        GameSave.isCave = true;
        PlayerPrefs.SetInt("GoCave", GameSave.isCave ? 1 : 0);
        GameSave.Level = 4;
        PlayerData playerData = new PlayerData();
        playerData.LevelChk = GameSave.Level;
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText("playerData.json", json);
       
        SceneManager.LoadScene("Enter2DScene");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isContact = true;
        }
    }
}
