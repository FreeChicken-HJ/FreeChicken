using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using TMPro;
public class FactorySceneChangeZone : MonoBehaviour
{
    [Header("Stats")]
    public GameObject ChangeConveorZone;
    public GameObject ChangeFinish;
    public GameObject Player;
    public ParticleSystem Particle;
    public AudioSource ParticleSound;

    public GameObject zoneL;
    public GameObject zoneR;
    public GameObject zoneG;
    public float t;

    public GameObject BigEgg;
    public GameObject Pos;

    public AudioSource ClickSound;

    public TextMeshProUGUI Rtxt;
    public TextMeshProUGUI Etxt;
    [Header("Bool")]
    public bool isButton;
    public bool isL;
    public bool isR;
    public bool isG;
   
    public bool isScene_2;
    public bool isChk;
    public bool isEnd;
   
    [Header("UI")]
    public Slider ChangeConveorSlider;
  

    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera ChangeCam;

    void Start()
    {
        Player = GameObject.FindWithTag("Player");   
    }
    void Update()
    {
        if (isButton)
        {
            Chk();
        }
        if(Player.GetComponent<FactoryPlayer_2>().isDie)
        {
            zoneL.gameObject.SetActive(false);
            zoneR.gameObject.SetActive(false);
            zoneG.gameObject.SetActive(false);
            ChangeConveorSlider.value = 0;
            t = 0;
            isEnd = false;
            ChangeConveorZone.SetActive(false);
            isButton = false;
            isChk = false;
            ChangeCam.Priority = 1;
            mainCam.Priority = 3;
        }
    }
    void Chk()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isL && !isG && !isR)
        {
            Rtxt.color = Color.red;
            Particle.Play();
            ClickSound.Play();
            
            zoneR.gameObject.SetActive(false);
            zoneL.gameObject.SetActive(true);
            isL = true;
            isR = false;
            isG = false;
        }
       
        else if (Input.GetKeyDown(KeyCode.R) && !isG && isL && !isR)
        {
            Rtxt.color = Color.red;
            Particle.Play();
            ClickSound.Play();
            zoneL.gameObject.SetActive(false);
            zoneG.gameObject.SetActive(true);
            isL = false;
            isG = true;
            isR = false;
        }
        
        else if (Input.GetKeyDown(KeyCode.R) && !isR && !isL && isG)
        {
            Rtxt.color = Color.red;
            Particle.Play();
            ClickSound.Play();
            zoneG.gameObject.SetActive(false);
            zoneR.gameObject.SetActive(true);
            isG = false;
            isR = true;
            isL = false;
            
        } 
        isR = false;
       
        if (Input.GetKeyUp(KeyCode.R))
        {
            Rtxt.color = Color.white;
        }

        if (Input.GetButton("E"))
        {
            Debug.Log("E");
            Etxt.color = Color.red;
            if (ChangeConveorSlider.value < 100f)
            {
                t += Time.deltaTime;
                ChangeConveorSlider.value = Mathf.Lerp(0, 100, t);
            }
            else
            {
                Etxt.color = Color.white;
                ParticleSound.Play();
                ChangeConveorZone.gameObject.SetActive(false);
                ChangeFinish.gameObject.SetActive(true);
                isButton = false;


                Player.GetComponent<FactoryPlayer_2>().isSlide = true;
                
               
                StartCoroutine(TheEnd());

            }

        }
        if (Input.GetButtonUp("E"))
        {
            Etxt.color = Color.white;
            t = 0;
            ChangeConveorSlider.value = 0;
        }

    }
    IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(1f);
        ChangeCam.Priority = 1;
        mainCam.Priority = 3;
        ChangeFinish.gameObject.SetActive(false);
        isChk = false;
        isEnd = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isChk && !isEnd)
        {
            isChk = true;
            ChangeConveorZone.SetActive(true);
            isButton = true;
            ChangeCam.Priority = 3;
            mainCam.Priority = 1;
            Invoke("SpawnBigEgg", 3f);
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isChk = false;
            ChangeConveorZone.SetActive(false);
            isButton = false;
            ChangeCam.Priority = 1;
            mainCam.Priority = 3;
        }
    }
    void SpawnBigEgg()
    {
        Instantiate(BigEgg, Pos.transform.position, Quaternion.identity);
       
       
    }
   
}
