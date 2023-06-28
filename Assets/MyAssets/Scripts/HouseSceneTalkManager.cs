using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HouseSceneTalkManager : MonoBehaviour//, IPointerDownHandler
{
    public TextMeshProUGUI text;
    public GameObject nextText;
    //public CanvasGroup dialoguegroup;

    public Queue<string> sentences;
    private string currentSentences;
    public bool isTyping;

    public static HouseSceneTalkManager instance;
    public GameObject NpcImage;
    public GameObject PlayerImage;
    public bool isNPCImage;
    public bool isPlayerImage;

    public bool isTalkEnd;
    
    //public HouseScenePlayer player;
    //public GameObject npccam;
    //public GameObject maincam;

    //public bool isTalkEnd;
    //public GameObject npc;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        //instance.gameObject.SetActive(true);
        sentences = new Queue<string>();
        isTalkEnd = false;
        isPlayerImage = true;
    }

    public void OndiaLog(string[] lines)
    {
        sentences.Clear();

        foreach (string line in lines)
        {
            sentences.Enqueue(line);
        }
        //dialoguegroup.alpha = 1;
        //dialoguegroup.blocksRaycasts = true;

        //NextSentence();
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
        //else
        //{
        //    dialoguegroup.alpha = 0;
        //    dialoguegroup.blocksRaycasts = false;
        //}

        if (sentences.Count == 0)
        {
            instance.gameObject.SetActive(false);
            isTalkEnd = true;
        }
    }

    void ChangeImage()
    {
        if(isNPCImage)
        {
            isNPCImage = false;
            NpcImage.gameObject.SetActive(false);
            PlayerImage.gameObject.SetActive(true);
            isPlayerImage = true;
        }
        else if(isPlayerImage)
        {
            isNPCImage= true;
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
            yield return new WaitForSeconds(0.1f);
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
            NextSentence();
            ChangeImage();
            //if (!isTyping)
            //    NextSentence();
        }

        /*if(sentences.Count == 0 )
        {
            
            instance.gameObject.SetActive(false);
            player.isTalk = false;
            //npccam.SetActive(false);
            //maincam.SetActive(true);
        }*/
    }
}
