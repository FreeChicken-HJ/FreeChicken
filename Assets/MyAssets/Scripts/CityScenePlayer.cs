using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class CityScenePlayer : MonoBehaviour
{
    public GameObject LastZonePlayer;
    public GameObject CurrentZonePlayer;
    Rigidbody rigid;
    public Animator anim_1;
    public Animator anim_2;
  
    public bool isStart;
    
    public ParticleSystem jumpPs;
    public ParticleSystem DiePs;
    public float jumpPower;
    bool isJump;
    public float hAxis;
    public float vAxis;
    public float Speed;
    public bool isfallingFruits;
    public bool ishurdleUp;

    bool isDie;
    bool particleAttack;

    public bool isLast;
    public bool isAllStop;
    public GameObject TalkUI;

    public GameObject Cam;
    public CinemachineVirtualCamera startCam;
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera dieCam;
    public CinemachineVirtualCamera jumpCam;

    public AudioSource startAudio;
    public AudioSource BGM;
    public AudioSource DieAudio;
    public AudioSource JumpAudio;
    
    // Start is called before the first frame update
    void Start()
    {
        startAudio.Play();
        rigid = GetComponent<Rigidbody>();
        DiePs.gameObject.SetActive(false);
        particleAttack = false;
        isAllStop = true;

        Invoke("NewStart", 3f);
    }
    void NewStart()
    {
        startAudio.Stop();
        BGM.Play();
        isAllStop = false;
        isStart = true;
        startCam.Priority = -1;
        mainCam.Priority = 1;
    }
    void Update()
    {
       
        if (!isDie && !isAllStop)
        {

            Jump();
        }
        if (isAllStop)
        {

            anim_1.SetBool("isRun", false);
        }
        
    }
    void FixedUpdate()
    {
        if (!isDie && !isAllStop)
        {
            GetInput();
            Move();
        }
        if (this.transform.position.y < -5f &&!isAllStop &&!isDie)
        {
            isAllStop = true;
            TagisObj();
        }
        
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        
    }
    void Move()
    {

        //Move(direction * Time.deltaTime);

        Vector3 position = transform.position;
        position.x += hAxis * Time.smoothDeltaTime * Speed;
        if (!isLast)
        {
            position.z += Time.smoothDeltaTime * Speed ;
        }
        else if (isLast)
        {
            if (vAxis != 0 || hAxis !=0)
            {
                anim_2.SetBool("isRun", true);
                position.z += vAxis * Time.smoothDeltaTime * Speed;
            }
            else if(vAxis == 0)
            {
                anim_2.SetBool("isRun", false);
            }
        }
        transform.position = position;
        //GetInput();
        //anim.SetTrigger("doRun");
        //rigid.AddForce(direction * Time.deltaTime * Speed);
        //transform.position += direction * Time.deltaTime;
        anim_1.SetBool("isRun", true);
        

    }
    void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                //Cam.SetActive(false);
                //jumpCam.SetActive(true);
                
                if (!isLast)
                {
                    jumpCam.Priority = 100;
                    anim_1.SetTrigger("doJump");
                    Invoke("ReSetJumpCam", 1f);
                }
                //anim.SetBool("isJump", true);
                isJump = true;
                JumpAudio.Play();
                rigid.AddForce( Vector3.up* jumpPower, ForceMode.Impulse);
               
                jumpPs.Play();
               
            }

        }
       
    }
    void ReSetJumpCam()
    {
        //Cam.SetActive(true);
        //jumpCam.SetActive(false);
        jumpCam.Priority = -1;
        //anim.SetBool("isJump", false);
    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Floor")
        {
            
            isJump = false;
        }
        if (collision.gameObject.tag == "Obstacle" && !isDie)
        {
            TagisObj();
        }
        

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Swings")
        {
            rigid.velocity += transform.forward * 3.5f;
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if(other.tag == "Obstacle" && !particleAttack )
        {
            particleAttack = true;
            Destroy(other.gameObject);
            TagisObj();
        }    
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "DropBox")
        {
            isfallingFruits = true;

        }
        if (other.tag == "Obstacle" &&!isDie)
        {
            TagisObj();
        }
        if(other.tag == "Hurdle")
        {
            ishurdleUp = true;
            Invoke("hurdleDownSet", 2.5f);
        }
        if(other.tag == "Rain")
        {
            isJump = true;
            Speed = 10f;
        }
        if(other.tag == "Ice")
        {
            isJump = true;
            Speed = 20f;
        }
        if(other.tag == "LastZone")
        {
            isAllStop = true;
            BGM.Stop();
            
            TalkUI.gameObject.SetActive(true);
            Invoke("Exit", 2f);
        }
      
    }
    void Exit()
    {
        TalkUI.gameObject.SetActive(false);
        isAllStop = false;

        isLast = true;
        LastZonePlayer.SetActive(true);
        CurrentZonePlayer.SetActive(false);
        jumpPower = 15f;
        Debug.Log(jumpPower);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Rain")
        {
            isJump = false;
            Speed = 6f;
        }
        if (other.tag == "Ice")
        {
            isJump = false;
            Speed = 6f;
        }
    }
    void hurdleDownSet()
    {
        ishurdleUp = false;
    }
    void TagisObj()
    {
        if (!isDie)
        {
            isDie = true;
            DieAudio.Play();
            //this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
            DiePs.gameObject.SetActive(true);
            dieCam.Priority = 100;
            anim_1.SetTrigger("doDie");
            anim_1.SetBool("isRun", false);
            rigid.isKinematic = true;
            Invoke("ReLoadScene", 1f);
        }
    }
    void ReLoadScene()
    {
        //anim.SetBool("isDie", false);
        
        SceneManager.LoadScene("CityScene");
    }
}
