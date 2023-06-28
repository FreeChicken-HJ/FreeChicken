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
    //public GameObject Dieparticle;
    int box;
    public FactoryPlayer player;
    //public PlayerChangeEgg playeregg;

    public GameObject talkCanvas1;
    public GameObject talkCanvas2;
    public CinemachineVirtualCamera mainCam;
    
    public CinemachineVirtualCamera managerCam;
    public CinemachineVirtualCamera managerInCam;
    public GameObject eggBoxSpawnPos;
    public GameObject eggBox;
    public bool isChk;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("FactoryPlayer").GetComponent<FactoryPlayer>();
        //playeregg = GameObject.Find("PlayerEgg").GetComponent<PlayerChangeEgg>();
    }

    // Update is called once per frame
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
        if(player.tmpBox.transform.position == pos)
        {
            //GameObject ob = Instantiate(Dieparticle, pos, Quaternion.identity);
            Debug.Log("�߾� �� �ְ͈�");
            yield return new WaitForSeconds(3f);
            //Destroy(ob);
            talkCanvas1.SetActive(true);
            managerInCam.Priority = 2;
            managerCam.Priority = 1;
            Invoke("Die",3f);
            
        }
        else
        {
            
            Debug.Log("�� ��Ҵ�");
            yield return new WaitForSeconds(1.5f);
            talkCanvas2.SetActive(true);
            Invoke("Trun",3f);
            isChk = true;

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

        //eggBox.SetActive(true);
        eggBox.transform.position = pos;
        eggBox.transform.rotation = rotate;
        eggBox.GetComponent<FactoryMoveEggBox>().isChk = false;
        managerInCam.Priority = 1;
        mainCam.Priority = 2;
        player.isSetEggFinish = false;
        player.Pos();
    }
    void Trun()
    {
        talkCanvas2.SetActive(false);
        managerCam.Priority = 1;
        managerInCam.Priority = -1;
        mainCam.Priority = 2;
        player.EggPrefab.SetActive(false);
        player.thisMesh.SetActive(true);
        player.isEgg = false;
        eggBox.GetComponent<FactoryMoveEggBox>().Speed = 0.1f;
        Debug.Log("�ӵ� ��");
        anim.SetBool("isAttack",false);
        player.isStopSlide = false;

    }
    Vector3 GetRandomPos()
    {
       
        box = Random.Range(0, boxPos.Length);
        
        Vector3 pos = boxPos[box].transform.position;
        return pos;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EggBox" && !isChk)
        {
            Debug.Log("�浹");
            isContact = true;
            //isChk = true;
            eggBox.GetComponent<FactoryMoveEggBox>().Speed = 0f;
            Debug.Log("�ӵ��ٿ�");
            anim.SetBool("isAttack", true);
        }
        
    }
}