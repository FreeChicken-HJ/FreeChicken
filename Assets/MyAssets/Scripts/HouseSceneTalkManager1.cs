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

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
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
            StartCoroutine(Typing(currentSentences));
        }

        if (sentences.Count == 0)
        {
            // ���� ������Ʈ�� �ı����� �ʾ��� ������ ó��
            if (gameObject != null)
            {
                Destroy(gameObject);

            }
            //isTalkEnd = true;
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
            }
        }
    }
}