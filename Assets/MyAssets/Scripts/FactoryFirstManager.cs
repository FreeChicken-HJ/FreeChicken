using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class FactoryFirstManager : MonoBehaviour
{
    public Animator anim;
    public GameObject[] boxPos;
    public bool isContact;
    public GameObject particle;
   
    int box;
    public FactoryPlayer player;
   
    public GameObject talkCanvas1;
    public GameObject talkCanvas2;
    public CinemachineVirtualCamera mainCam;
    
    public CinemachineVirtualCamera managerCam;
    public CinemachineVirtualCamera managerInCam;
    public GameObject eggBoxSpawnPos;
    public GameObject eggBox;
    public bool isChk;

    public AudioSource hitAudio;
    public AudioSource mainAudio_1;
    public AudioSource mainAudio_2;
    public AudioSource heartAudio;

    public GameObject attackBox;
    public GameObject Wall;
  
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
      
    }

    void Update()
    {
        if (isContact)
        {
            StartCoroutine(Spawn());
            
        }
    }
    IEnumerator Spawn()
    {
        isContact = false;
        Vector3 pos = GetRandomPos();
        GameObject instance = Instantiate(particle,pos,Quaternion.identity);
        if (player.tmpBox.transform.position == pos)
        {
           
            yield return new WaitForSeconds(2f);
          
            talkCanvas1.SetActive(true);
            managerInCam.Priority = 2;
            managerCam.Priority = 1;
            Invoke("Die",2f);

    }
        else
        {

            
            yield return new WaitForSeconds(1.5f);
            talkCanvas2.SetActive(true);
            Invoke("Turn", 3f);


        }
      
       
       Destroy(instance,3f);
    }
    void Die()
    {

        talkCanvas1.SetActive(false);
        player.EggPrefab.SetActive(false);
        player.thisMesh.SetActive(true);
        player.isEgg = false;
        Vector3 pos = eggBoxSpawnPos.transform.position;
        Quaternion rotate = new Quaternion(-0.0188433286f, -0.706855774f, -0.706855536f, 0.0188433584f);
        Wall.SetActive(true);
       
        eggBox.transform.position = pos;
        eggBox.transform.rotation = rotate;
        eggBox.GetComponent<FactoryMoveEggBox>().isChk = false;
        managerInCam.Priority = 1;
        mainCam.Priority = 2;
        heartAudio.Stop();
        mainAudio_1.Play();
        DeadCount.count++;
        player.isSetEggFinish = false;
        player.isClick = false;
        player.Pos();
        anim.SetBool("isAttack", false);
        isChk = false;
    }
    void Turn()
    {
        talkCanvas2.SetActive(false);
        managerCam.Priority = 1;
        managerInCam.Priority = -1;
        mainCam.Priority = 2;
        player.EggPrefab.SetActive(false);
        player.thisMesh.SetActive(true);
        player.isEgg = false;
        eggBox.GetComponent<FactoryMoveEggBox>().Speed = 0.1f;        
        anim.SetBool("isAttack",false);
        player.isStopSlide = false;
        mainAudio_2.Play();

    }
    Vector3 GetRandomPos()
    {
   
        Vector3 pos = attackBox.transform.position;       
        return pos;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("EggBox") && !isChk)
        {           
            isContact = true;
            isChk = true;
            eggBox.GetComponent<FactoryMoveEggBox>().Speed = 0f;
            anim.SetBool("isAttack", true);
            Invoke("PlayHitSound", .5f);
        }
        
    }
    void PlayHitSound()
    {
        hitAudio.Play();
    }
}
