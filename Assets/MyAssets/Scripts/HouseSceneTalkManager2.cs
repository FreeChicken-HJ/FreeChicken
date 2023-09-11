using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;

public class HouseSceneTalkManager2 : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject nextText;

    public Queue<string> sentences;
    private string currentSentences;
    public bool isTyping;

    public static HouseSceneTalkManager2 instance;
    public GameObject NpcImage;
    public GameObject PlayerImage;
    public bool isNPCImage;
    public bool isPlayerImage;
    public bool isTalkEnd;

    HouseScene2_Player player;

    public AudioSource TalkSound;
    public AudioSource ClickButtonSound;

    public CinemachineVirtualCamera maincam;
    public CinemachineVirtualCamera npccam;
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HouseScene2_Player>();
        player.isTalk1 = true;
        player.isTalk2 = true;

        NpcImage.SetActive(false);
        PlayerImage.SetActive(false);
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
            TalkSound.Play();
            StartCoroutine(Typing(currentSentences));
        }

        if (sentences.Count == 0)
        {
            maincam.Priority = 2;
            npccam.Priority = -5;


            if (gameObject != null)
            {
                Destroy(gameObject);

            }
            Cursor.visible = false;
            player.isTalk1 = false;
            player.isTalk2 = false;
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
                ClickButtonSound.Play();
                if (sentences.Count == 0)
                {
                    NpcImage.SetActive(false);
                    PlayerImage.SetActive(false);
                }
                ChangeImage();
            }
        }
    }
}
