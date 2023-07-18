using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
public class FactorySceneChangeZone : MonoBehaviour
{
    [Header("Stats")]
    public GameObject ChangeConveorZone;
    public GameObject ChangeFinish;
    public GameObject Player;
    public ParticleSystem Particle;

    public GameObject zoneL;
    public GameObject zoneR;
    public GameObject zoneG;
    public float t;

    public GameObject BigEgg;
    public GameObject Pos;

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
    }
    void Chk()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isL && !isG && !isR)
        {
            Particle.Play();
            zoneR.gameObject.SetActive(false);
            zoneL.gameObject.SetActive(true);
            isL = true;
            isR = false;
            isG = false;
        }
       
        else if (Input.GetKeyDown(KeyCode.R) && !isG && isL && !isR)
        {
            Particle.Play();
            zoneL.gameObject.SetActive(false);
            zoneG.gameObject.SetActive(true);
            isL = false;
            isG = true;
            isR = false;
        }
        
        else if (Input.GetKeyDown(KeyCode.R) && !isR && !isL && isG)
        {
            Particle.Play();
            
            zoneG.gameObject.SetActive(false);
            zoneR.gameObject.SetActive(true);
            isG = false;
            isR = true;
            isL = false;
            
        } 
        isR = false;
            

        
        if (Input.GetButton("E"))
        {
            Debug.Log("E");
            if (ChangeConveorSlider.value < 100f)
            {
                t += Time.deltaTime;
                ChangeConveorSlider.value = Mathf.Lerp(0, 100, t);
            }
            else // 다 채워지면
            {


                ChangeConveorZone.gameObject.SetActive(false);
                ChangeFinish.gameObject.SetActive(true);
                isButton = false;
                if (isScene_2)
                {
                    Player.GetComponent<FactoryPlayer_2>().isSlide = true;
                }
                else
                {
                    Player.GetComponent<FactoryPlayer>().isSlide = true;
                }
                StartCoroutine(TheEnd());

            }

        }
        if (Input.GetButtonUp("E"))
        {
            t = 0;
            ChangeConveorSlider.value = 0;
        }

    }
    IEnumerator TheEnd()
    {
        yield return new WaitForSeconds(1.5f);
        ChangeCam.Priority = 1;
        mainCam.Priority = 3;
        ChangeFinish.gameObject.SetActive(false);
        isChk = false;
        isEnd = true;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isChk && !isEnd)
        {
            isChk = true;
            ChangeConveorZone.SetActive(true);
            isButton = true;
            ChangeCam.Priority = 3;
            mainCam.Priority = 1;
            Invoke("SpawnBigEgg", 3f);
            //StartCoroutine(turnZone());
        }
    }
    void SpawnBigEgg()
    {
        Instantiate(BigEgg, Pos.transform.position, Quaternion.identity);
    }
}
// 반복 while
//e 누르면 왼쪽 
// 다시 누르면 왼쪽 x 가운데 
// 다시 누르면 가운데 x 오른쪽
// 다시 누르면 오른쪽 x 왼쪽 
