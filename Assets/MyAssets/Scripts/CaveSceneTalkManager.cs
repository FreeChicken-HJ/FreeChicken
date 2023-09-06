using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;

public class CaveSceneTalkManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject nextText;

    public Queue<string> sentences;
    private string currentSentences;
    public bool isTyping;

    public static CaveSceneTalkManager instance;
    public GameObject NpcImage;
    public GameObject PlayerImage;
    public bool isNPCImage;
    public bool isPlayerImage;
    CaveScenePlayer Player;
    public bool isTalkEnd;
    public AudioSource TalkSound;
    public AudioSource ClickButtonSound;
    //public CinemachineVirtualCamera NPC4Cam;
    //public CinemachineVirtualCamera mainCam;

    //---------------------------------


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
        isTalkEnd = false;
        isPlayerImage = true;
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<CaveScenePlayer>();
        Player.isTalk = true;
        Cursor.visible = true;

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
            //instance.gameObject.SetActive(false);
            Destroy(instance.gameObject);
            Player.isTalk = false;
            Cursor.visible = false;
            //NPC4Cam.Priority = 1;
            //mainCam.Priority = 10;
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
            yield return new WaitForSeconds(0.02f);
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
                ChangeImage();
            }
        }
    }


}
