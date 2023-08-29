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

    [Header("UI")]
    public GameObject EndingCanvas;
    public GameObject ButtonCanvas;

    [Header("Audio")]
    public AudioSource ChickenAudio;
    public AudioSource ClickButtonAudio;

    public float camera1Duration; // camera1�� ��ȯ�ϴ� ���� �ð� (��)
    public float camera2Duration; // camera2�� ��ȯ�ϴ� ���� �ð� (��)
    public float camera3Duration; // camera3�� ��ȯ�ϴ� ���� �ð� (��)
    public float cameraSwitchDelay; // ���� �ð� �Ŀ� ī�޶� ��ȯ (��)

    private bool hasSwitchedToCamera1 = false;
    private bool hasSwitchedToCamera2 = false;

    public TextMeshProUGUI text;
    public List<string> dialogueList;
    public float typingSpeed;
    public float timeBetweenSentences;
    //private int currentDialogueIndex = 0;


    void Start()
    {
        // �ʱ� ���¿��� mainCam�� Ȱ��ȭ
        mainCam.Priority = 10;
        camera1.Priority = 0;
        camera2.Priority = 0;
        camera3.Priority = 0;
        ChickenAudio.Play();

        text.text = "";
        StartCoroutine(ShowDialogues());
    }

    void Update()
    {
        if (!hasSwitchedToCamera1 && !hasSwitchedToCamera2) // �� ��° ī�޶�� ��ȯ���� �ʾ��� ����
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
        SceneManager.LoadScene("New Scene");
    }

    public void ClickButtonSound()
    {
        ClickButtonAudio.Play();
    }

    private IEnumerator ShowDialogues()
    {
        yield return new WaitForSeconds(3.0f); // �ʱ� ��� �ð�

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
                yield break; // �̹� ��ȯ �Ϸ�Ǿ��� �� �ߴ�
        }

        mainCam.Priority = 0;
        camera1.Priority = 10;

        hasSwitchedToCamera1 = true;
        StartCoroutine(SwitchToCamera2()); // ���� ī�޶�� ��ȯ
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
                yield break; // �̹� ��ȯ �Ϸ�Ǿ��� �� �ߴ�
        }

        camera1.Priority = 0;
        camera2.Priority = 10;

        StartCoroutine(SwitchToCamera3()); // ���� ī�޶�� ��ȯ
    }

    //IEnumerator SwitchToCamera3()
    //{
    //    yield return new WaitForSeconds(cameraSwitchDelay);

    //    float currentTime = 0;
    //    float initialPriority = camera2.Priority;

    //    while (currentTime < camera3Duration)
    //    {
    //        currentTime += Time.deltaTime;
    //        float t = currentTime / camera3Duration;

    //        camera2.Priority = (int)Mathf.Lerp(initialPriority, 0, t);
    //        camera3.Priority = (int)Mathf.Lerp(10, 0, t);

    //        yield return new WaitForEndOfFrame();
    //    }

    //    camera2.Priority = 0;
    //    camera3.Priority = 10;
    //}

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

            // Gradually move the camera upwards
            mainCam.transform.position = Vector3.Lerp(initialPosition, targetPosition, t);

            yield return new WaitForEndOfFrame();
        }

        camera2.Priority = 0;
        camera3.Priority = 10;

        mainCam.transform.position = targetPosition;
    }
}
