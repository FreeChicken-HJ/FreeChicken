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
    public List<string> dialogueList;
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
        if (dialogueList.Count > 0)
        {
            text.text = "";
            StartCoroutine(InitialDelayCoroutine());
        }
    }

    private IEnumerator InitialDelayCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);
        StartCoroutine(TypingCoroutine());
    }

    private IEnumerator TypingCoroutine()
    {
        while (currentDialogueIndex < dialogueList.Count)
        {
            string dialogue = dialogueList[currentDialogueIndex];
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

            if (currentDialogueIndex >= dialogueList.Count)
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
        PlayerData playerData = new PlayerData();
        playerData.isStartEnd = true;
        string json = JsonUtility.ToJson(playerData);

        File.WriteAllText("playerData.json", json);
        Invoke("StartScene", 2f);
    }
    void StartScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
