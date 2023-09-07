using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;

public class HouseSceneTalkManager1 : MonoBehaviour//, IPointerDownHandler
{
    public TextMeshProUGUI text;
    public GameObject nextText;

    public Queue<string> sentences;
    private string currentSentences;
    public bool isTyping;

    public static HouseSceneTalkManager1 instance;
    public GameObject NpcImage;
    public GameObject PlayerImage;
    public bool isNPCImage;
    public bool isPlayerImage;
    public bool isTalkEnd;

    HouseScenePlayer player;
    public AudioSource TalkSound;
    public AudioSource ClickSound;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.visible = true;
        sentences = new Queue<string>();
        isTalkEnd = false;
        isPlayerImage = true;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScenePlayer>();
        player.isTalk = true;
    }

    public void OndiaLog(string[] lines)
    {
        sentences.Clear();

        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
    }

    //public void NextSentence()
    //{
    //    if (sentences.Count != 0)
    //    {
    //        currentSentences = sentences.Dequeue();
    //        isTyping = true;
    //        nextText.SetActive(false);
    //        StartCoroutine(Typing(currentSentences));
    //    }

    //    if (sentences.Count == 0)
    //    {
    //        Destroy(instance.gameObject);
    //        isTalkEnd = true;
    //    }
    //}

    public void NextSentence()
    {
        if (sentences.Count != 0)
        {
            currentSentences = sentences.Dequeue();
            isTyping = true;
            
            nextText.SetActive(false);
            TalkSound.Play();
            StartCoroutine(Typing(currentSentences));
        }

        if (sentences.Count == 0)
        {
            // 게임 오브젝트가 파괴되지 않았을 때에만 처리
            if (gameObject != null)
            {
                Destroy(gameObject);

            }
            //isTalkEnd = true;
            Cursor.visible = false;
            player.isTalk = false;
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
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Update()
    {
        if (text.text.Equals(currentSentences))
        {
            nextText.SetActive(true);
            isTyping = false;
        }

        if (Input.GetMouseButton(0) && !isTyping)
        {
            if (!isTyping)
            {
                NextSentence();
                ChangeImage();
                ClickSound.Play();
            }
        }
    }
}
