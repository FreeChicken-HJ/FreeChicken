using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Cinemachine;
public class FactoryUIManager : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject nextText;
   
    public Queue<string> sentences;
    public string currentSentences;
    public bool isTyping;
   
    public static FactoryUIManager instance;
    public FactoryPlayer player;
    public CinemachineVirtualCamera npccam;
    public CinemachineVirtualCamera maincam;

    public bool isTalkEnd;
    public GameObject npc;
    public bool isTalkPoint2;

    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        //instance.gameObject.SetActive(true);
        sentences = new Queue<string>();
        isTalkEnd = false;
        
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
                player.isTalk = false;
                isTalkEnd = true;
                player.isStopSlide = false;
                npccam.Priority = 1;
                maincam.Priority = 2;
                npc.SetActive(false);
            
           
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

        if (Input.GetMouseButton(0))
        {
            if (!isTyping)
                NextSentence();
        }
      
       
    }
    
}
