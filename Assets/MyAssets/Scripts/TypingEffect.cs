using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject TalkCanvas;
    public CanvasGroup canvasGroup;
    public List<string> dialogueList;
    private int currentDialogueIndex = 0;
    //private bool isFading = false;

    public float fadeDuration = 1.0f;
    public string nextSceneName;

    private void Start()
    {
        canvasGroup.alpha = 1f; // ÃÊ±âÈ­
        if (dialogueList.Count > 0)
        {
            text.text = "";
            StartCoroutine(TypingCoroutine());
        }
    }

    private IEnumerator TypingCoroutine()
    {
        yield return new WaitForSeconds(2f);

        while (currentDialogueIndex < dialogueList.Count)
        {
            string dialogue = dialogueList[currentDialogueIndex];
            for (int i = 0; i <= dialogue.Length; ++i)
            {
                text.text = dialogue.Substring(0, i);
                yield return new WaitForSeconds(0.05f);
            }

            while (!Input.GetMouseButtonDown(0))
            {
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
        //isFading = true;

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0f;

        //isFading = false;

        // Load the next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
