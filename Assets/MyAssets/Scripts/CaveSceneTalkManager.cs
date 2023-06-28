using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CaveSceneTalkManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject nextText;

    public Queue<string> sentences;
    public string currentSentences;
    public bool isTyping;

    public static CaveSceneTalkManager instance;
    public GameObject NpcImage;
    public GameObject PlayerImage;
    public bool isNPCImage;
    public bool isPlayerImage;

    public bool isTalkEnd;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        sentences = new Queue<string>();
        isTalkEnd = false;
        isPlayerImage = true;
        /*NpcImage.gameObject.SetActive(true);
        PlayerImage.gameObject.SetActive(false);*/
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

            instance.gameObject.SetActive(false);
            isTalkEnd = true;
            //SceneManager.LoadScene("CityScene");

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
                

        }


    }
}
