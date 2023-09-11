using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    [Header("Camera")]
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera camera1;
    public CinemachineVirtualCamera camera2;
    public CinemachineVirtualCamera camera3;
    public CinemachineVirtualCamera camera4;

    [Header("UI")]
    public GameObject EndingCanvas;
    public GameObject ButtonCanvas;

    [Header("Audio")]
    public AudioSource ChickenAudio;
    public AudioSource ClickButtonAudio;

    public float camera1Duration; 
    public float camera2Duration; 
    public float camera3Duration; 
    public float camera4Duration;
    public float cameraSwitchDelay; 

    private bool hasSwitchedToCamera1 = false;
    private bool hasSwitchedToCamera2 = false;
    private bool hasSwitchedToCamera3 = false;
    public TextMeshProUGUI text;
    public List<string> dialogueList;
    public float typingSpeed;
    public float timeBetweenSentences;

    void Start()
    {
        mainCam.Priority = 10;
        camera1.Priority = 0;
        camera2.Priority = 0;
        camera3.Priority = 0;
        camera4.Priority = 0;
        ChickenAudio.Play();

        text.text = "";
        StartCoroutine(ShowDialogues());
        Invoke("ButtonShow", 3f);
    }
    void ButtonShow()
    {
        ButtonCanvas.SetActive(true);
    }
    void Update()
    {
        if (!hasSwitchedToCamera1 && !hasSwitchedToCamera2 && !hasSwitchedToCamera3) 
        {
            StartCoroutine(SwitchToCamera1());
        }
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("FirstScene");
    }

    public void ClickButtonSound()
    {
        ClickButtonAudio.Play();
    }

    private IEnumerator ShowDialogues()
    {
        yield return new WaitForSeconds(3.0f); 

        foreach (string dialogue in dialogueList)
        {
            yield return StartCoroutine(TypeSentence(dialogue));
            yield return new WaitForSeconds(timeBetweenSentences);
        }
    }

    private IEnumerator TypeSentence(string sentence)
    {
        text.text = "";

        foreach (char letter in sentence)
        {
            text.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator SwitchToCamera1()
    {
        float currentTime = 0;
        float initialPriority = mainCam.Priority;

        while (currentTime < camera1Duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / camera1Duration;

            mainCam.Priority = (int)Mathf.Lerp(initialPriority, 0, t);
            camera1.Priority = (int)Mathf.Lerp(10, 0, t);

            yield return new WaitForEndOfFrame();

            if (hasSwitchedToCamera1)
                yield break; 
        }

        mainCam.Priority = 0;
        camera1.Priority = 10;

        hasSwitchedToCamera1 = true;
        StartCoroutine(SwitchToCamera2()); 
    }

    IEnumerator SwitchToCamera2()
    {
        yield return new WaitForSeconds(cameraSwitchDelay);

        float currentTime = 0;
        float initialPriority = camera1.Priority;

        while (currentTime < camera2Duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / camera2Duration;

            camera1.Priority = (int)Mathf.Lerp(initialPriority, 0, t);
            camera2.Priority = (int)Mathf.Lerp(10, 0, t);

            yield return new WaitForEndOfFrame();

            if (hasSwitchedToCamera2)
                yield break; 
        }

        camera1.Priority = 0;
        camera2.Priority = 10;

        StartCoroutine(SwitchToCamera3()); 
    }

   
    IEnumerator SwitchToCamera3()
    {
        yield return new WaitForSeconds(cameraSwitchDelay);

        float currentTime = 0;
        float initialPriority = camera2.Priority;

        Vector3 initialPosition = mainCam.transform.position;
        Vector3 targetPosition = initialPosition + Vector3.up;

        while (currentTime < camera3Duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / camera3Duration;

            camera2.Priority = (int)Mathf.Lerp(initialPriority, 0, t);
            camera3.Priority = (int)Mathf.Lerp(10, 0, t);

            mainCam.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            yield return new WaitForEndOfFrame();
            if (hasSwitchedToCamera3)
                yield break;
        }

        camera2.Priority = 0;
        camera3.Priority = 10;
        StartCoroutine(SwitchToCamera4());
    }
    IEnumerator SwitchToCamera4()
    {
        yield return new WaitForSeconds(cameraSwitchDelay);

        float currentTime = 0;
        float initialPriority = camera2.Priority;

        Vector3 initialPosition = mainCam.transform.position;
        Vector3 targetPosition = initialPosition + Vector3.up;

        while (currentTime < camera4Duration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / camera4Duration;

            camera3.Priority = (int)Mathf.Lerp(initialPriority, 0, t);
            camera4.Priority = (int)Mathf.Lerp(10, 0, t);

            mainCam.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            yield return new WaitForEndOfFrame();
        }

        camera3.Priority = 0;
        camera4.Priority = 10;

        mainCam.transform.position = targetPosition;
    }
}
