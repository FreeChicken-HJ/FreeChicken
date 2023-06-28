using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class CityScenePlayer : MonoBehaviour
{
   
    Rigidbody rigid;
    public Animator anim;
    //MoveObstacle obstacle;
   
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
    
   

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        DiePs.gameObject.SetActive(false);
        particleAttack = false;
        isStart = true;
        
       
    }

    void Update()
    {
       
        if (!isDie && !isAllStop)
        {

            Jump();
        }
        if (isAllStop)
        {
            anim.SetBool("isRun", false);
        }
    }
    void FixedUpdate()
    {
        if (!isDie && !isAllStop)
        {
            GetInput();
            Move();
        }
        
    }
    void GetInput()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
    }
    void Move()
    {
        
        Vector3 position = transform.position;
        position.x += hAxis * Time.smoothDeltaTime * Speed;
        if (!isLast)
        {
            position.z += Time.smoothDeltaTime * Speed;
        }
        else if (isLast)
        {
            position.z += vAxis *Time.smoothDeltaTime * Speed;
        }
        transform.position = position;
        anim.SetTrigger("doRun");
        anim.SetBool("isRun", true);
        
    }
    void Jump()
    {

        if (Input.GetButtonDown("Jump"))
        {
            if (!isJump)
            {
                isJump = true;
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                jumpPs.Play();
            }

        }
       
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
            TalkUI.gameObject.SetActive(true);
            Invoke("Exit", 2f);
        }
      
    }
    void Exit()
    {
        TalkUI.gameObject.SetActive(false);
        isAllStop = false;
        isLast = true;
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
        isDie = true;
        //this.transform.position = new Vector3(this.transform.position.x, 0f, this.transform.position.z);
        DiePs.gameObject.SetActive(true);
       
        anim.SetTrigger("doDie");
        anim.SetBool("isRun",false);

        Invoke("ReLoadScene", 1.5f);
    }
    void ReLoadScene()
    {
        //anim.SetBool("isDie", false);
        
        SceneManager.LoadScene("CityScene");
    }
}
