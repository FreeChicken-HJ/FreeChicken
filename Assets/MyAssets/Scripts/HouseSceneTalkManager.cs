using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;

public class HouseSceneTalkManager : MonoBehaviour//, IPointerDownHandler
{
    public TextMeshProUGUI text;
    public GameObject nextText;

    public Queue<string> sentences;
    private string currentSentences;
    public bool isTyping;

    public static HouseSceneTalkManager instance;
    public GameObject NpcImage;
    public GameObject PlayerImage;
    public bool isNPCImage;
    public bool isPlayerImage;
    public bool isTalkEnd;

    HouseScene2_Player player;
    //HouseScenePlayer player1;
    public AudioSource TalkSound;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
        isTalkEnd = false;
        isPlayerImage = true;
        Cursor.visible = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScene2_Player>();
        //player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
        //player.isTalk = true;
        //player1.isTalk = true;
    }

    public void OndiaLog(string[] lines)
    {
        sentences.Clear();

        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
    }

 

    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentences = sentences.Dequeue();
            isTyping = true;
            nextText.SetActive(false);
            StartCoroutine(Typing(currentSentences));
        }

        if (sentences.Count == 0)
        {
            player.isTalk1 = false;
            player.isTalk2 = false;
            // 게임 오브젝트가 파괴되지 않았을 때에만 처리
            if (gameObject != null)
            {
                Destroy(gameObject);

            }
            Cursor.visible = false; 
            //isTalkEnd = true;
            //player.isTalk=false;
            //player1.isTalk = false;
        }
    }

    void ChangeImage()
    {
        if (isNPCImage)
        {
            isNPCImage = false;
            NpcImage.gameObject.SetActive(false);
            PlayerImage.gameObject.SetActive(true);
            isPlayerImage = true;
        }
        else if (isPlayerImage)
        {
            isNPCImage = true;
            NpcImage.gameObject.SetActive(true);
            PlayerImage.gameObject.SetActive(false);
            isPlayerImage = false;
        }
    }

    IEnumerator Typing(string line)
    {
        text.text = "";
        foreach (char ch in line.ToCharArray())
        {
            text.text += ch;
            yield return new WaitForSeconds(0.04f);
        }
    }

    void Update()
    {
        if (text.text.Equals(currentSentences))
        {
            nextText.SetActive(true);
            isTyping = false;
        }

        if (Input.GetMouseButton(0)|| Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            if (!isTyping)
            {
                TalkSound.Play();
                NextSentence();
                ChangeImage();
            }
        }
    }
}
