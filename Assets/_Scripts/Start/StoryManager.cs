using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    public GameObject instructionPanel;
    public GameObject[] instrPanelPages;
    public AudioClip[] sounds;
    public GameObject fadeIN;
    public GameObject fadeOUT;
    public GameObject soundOff;
    public GameObject soundOn;
    public float timer = -1;
    AudioSource music;

    int currPage = 0;
    float timeToStart = 0.5f;
    float timerStarter;
    bool isStarted;


    private void Start()
    {
        music = GetComponent<AudioSource>();
       // music.clip = sounds[0];
       // music.loop = true;
       // music.Play();

    }


    void Update()
    {
        timer += Time.deltaTime;
        
        if (isStarted)
        {
            fadeIN.SetActive(true);
            timerStarter += Time.deltaTime;
            if (timerStarter >= timeToStart)
            {
                SceneManager.LoadScene("Shnici_Level_1", LoadSceneMode.Single);
            }
        }
        if (FadeIN.isFadeIN)
        {
            fadeOUT.SetActive(true);
            FadeIN.isFadeIN = false;
        }
        if (FadeOut.isFadeOut)
        {
            FadeOut.isFadeOut = false;
        }
    }

    public void StartGame()
    {
        isStarted = true;
        music.clip = sounds[1];
        music.loop = false;
        music.Play();
    }

    public void OpenInstructionPanel()
    {
        music.clip = sounds[2];
        music.loop = false;
        music.Play();
        FadeINOUT();
        instrPanelPages[currPage].SetActive(false);
        currPage = 0;
        instrPanelPages[currPage].SetActive(true);
        Invoke("Open", 0.5f);
    }
    public void CloseInstructionPanel()
    {
        music.clip = sounds[2];
        music.loop = false;
        music.Play();
        FadeINOUT();

        Invoke("Close", 0.5f);
    }

    public void FadeINOUT()
    {
        fadeIN.SetActive(true);
    }

    void Open()
    {
        instructionPanel.SetActive(true);
    }

    void Close()
    {
        instructionPanel.SetActive(false);
    }

    public void NextPage()
    {
        music.clip = sounds[2];
        music.loop = false;
        music.Play();
        instrPanelPages[currPage].SetActive(false);
        currPage++;
        if (currPage > instrPanelPages.Length -1)
        {
            currPage = 0;
        }
        instrPanelPages[currPage].SetActive(true);

    }

    public void PreviousPage()
    {
        music.clip = sounds[2];
        music.loop = false;
        music.Play();
        instrPanelPages[currPage].SetActive(false);
        currPage--;
        if (currPage < 0)
        {
            currPage = instrPanelPages.Length - 1;
        }
        instrPanelPages[currPage].SetActive(true);

    }

    public void SoundsOff()
    {
        music.clip = sounds[2];
        music.loop = false;
        music.Play();
        soundOff.SetActive(false);
        AudioListener.volume = 0;
        soundOn.SetActive(true);
    }
    public void SoundsOn()
    {
        music.clip = sounds[2];
        music.loop = false;
        music.Play();
        soundOn.SetActive(false);
        AudioListener.volume = 1;
        soundOff.SetActive(true);
    }
}
