using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject LoadingUI;
    public GameObject TalkCanvas;
    public CanvasGroup canvasGroup;
    public List<string> dialogueList1;
    public List<string> dialogueList2;
    private int currentDialogueIndex = 0;

    public float fadeDuration = 1.0f;
    public float initialDelay = 2.0f; 
    public string nextSceneName;

    private bool waitForClick = false;
    public AudioSource ButtonClickSound;
    public AudioSource BGM;
    private void Start()
    {
        Cursor.visible = true;
        BGM.Play();
        canvasGroup.alpha = 1f;
        if (dialogueList1.Count > 0 && !PlayerData.isEnglish)
        {
            text.text = "";
            StartCoroutine(InitialDelayCoroutine1());
        }

        if (dialogueList1.Count > 0 && PlayerData.isEnglish)
        {
            text.text = "";
            StartCoroutine(InitialDelayCoroutine2());
        }
        
       
    }
    public void Skip()
    {
        SceneManager.LoadScene(nextSceneName);
    }
    private IEnumerator InitialDelayCoroutine1()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(TypingCoroutine1());
    }

    private IEnumerator InitialDelayCoroutine2()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(TypingCoroutine2());
    }

    private IEnumerator TypingCoroutine1()
    {
        while (currentDialogueIndex < dialogueList1.Count)
        {
            string dialogue = dialogueList1[currentDialogueIndex];
            for (int i = 0; i <= dialogue.Length; ++i)
            {
                text.text = dialogue.Substring(0, i);
                yield return new WaitForSeconds(0.04f);
            }

            waitForClick = true; 
            while (waitForClick)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ButtonClickSound.Play();
                    waitForClick = false;
                    break;

                }
                yield return null;
            }

            currentDialogueIndex++;

            if (currentDialogueIndex >= dialogueList1.Count)
            {
                StartCoroutine(FadeOutAndLoadScene());
                yield break;
            }

            text.text = "";

            yield return null;
        }
    }

    private IEnumerator TypingCoroutine2()
    {
        while (currentDialogueIndex < dialogueList2.Count)
        {
            string dialogue = dialogueList2[currentDialogueIndex];
            for (int i = 0; i <= dialogue.Length; ++i)
            {
                text.text = dialogue.Substring(0, i);
                yield return new WaitForSeconds(0.04f);
            }

            waitForClick = true;
            while (waitForClick)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    ButtonClickSound.Play();
                    waitForClick = false;
                    break;

                }
                yield return null;
            }

            currentDialogueIndex++;

            if (currentDialogueIndex >= dialogueList2.Count)
            {
                StartCoroutine(FadeOutAndLoadScene());
                yield break;
            }

            text.text = "";

            yield return null;
        }
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;
        Cursor.visible = false;
        BGM.Stop();
        LoadingUI.SetActive(true);
       
        Invoke("StartScene", 2f);
    }
    void StartScene()
    {
        /*if (!File.Exists("PlayerData.json"))
        {
            PlayerData playerData = new PlayerData();
            playerData.isStartEnd = true;
            string json = JsonUtility.ToJson(playerData);

            File.WriteAllText("playerData.json", json);
        }*/
        SceneManager.LoadScene(nextSceneName);
    }
}
