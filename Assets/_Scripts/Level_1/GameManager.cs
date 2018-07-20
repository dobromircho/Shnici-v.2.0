using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioClip[] sounds;
    public GameObject[] trash;
    public GameObject[] badFish;
    public GameObject[] gifts;
    public GameObject jump;
    public GameObject shnici;
    public GameObject fadeIn;
    public GameObject soundOff;
    public GameObject soundOn;
    public GameObject finalAnim;
    public GameObject tryAgain;
    public GameObject tryAgainButton;

    public Transform endTarget;
    public Slider oxigenLevel;
    public Slider trashLevel;
    public Slider soundLevel;
    public Text gameOver;
    public Image rbin;
    public Text oxygenText;
    public float maxHeght;
    public float minHeght;
    public bool isLastGift;

    AudioSource suspend;
    AudioSource audioSource;
    Animator shniciAnim;
    //Gifts giftsScript;
    SnakeFish snakeScript;

    float timerWin;
    float timer = -3;
    float timerEnergy;
    float timerGameOver;
    float timerLastGift;
    float endTimer;
    float loseTimer;
    public float smoothEnd;

    bool isGameStarted;
    bool isGameOver;
    bool firstGiftTaken;
    bool secondGiftTaken;
    bool thirdGiftTaken;

    int randomTrash;
    int trashNumber;
    int randomFish;
    int currentFish;
    int fishCounter = 0;
    int giftsCounter;


    void Start()
    {
        shniciAnim = shnici.GetComponent<Animator>();
        oxigenLevel.value = 100;
        instance = this;
        audioSource = GetComponent<AudioSource>();
        //giftsScript = gifts[2].GetComponent<Gifts>();
        suspend = GameObject.FindGameObjectWithTag("suspend").GetComponent<AudioSource>();
        snakeScript = GameObject.FindGameObjectWithTag("snake").GetComponent<SnakeFish>();
        StartGame();
    }

    void Update()
    {
        timerEnergy += Time.deltaTime;
        timer += Time.deltaTime;

        if (FadeOut.isFadeOut)
        {
            FadeOut.isFadeOut = false;
        }
        if (isGameOver)
        {
            GameOver();
        }
        if (timerEnergy >= 1)
        {
            DecreaseOxygen(1);
            timerEnergy = 0;
        }

        if (rbin.fillAmount >= 0.25f && !firstGiftTaken)
        {
            CreateGift();
            firstGiftTaken = true;
        }
        if (rbin.fillAmount >= 0.75f && !secondGiftTaken)
        {
            CreateGift();
            secondGiftTaken = true;
        }
        if (rbin.fillAmount >= 0.99f && !thirdGiftTaken)
        {
            CreateGift();
            thirdGiftTaken = true;
        }
        if (giftsCounter > 2)
        {
            WinGame();
        }
        if (oxigenLevel.value <= 1)
        {
            loseTimer += Time.deltaTime;
            endTimer += Time.deltaTime;
            timerEnergy = 0;
            timer = 0;
            jump.gameObject.SetActive(false);
            Shnici.instance.rb.velocity = Vector3.zero;
            Shnici.instance.cc.enabled = false;
            shniciAnim.SetTrigger("loosestart");
            //isGameOver = true;
            shnici.transform.position = Vector3.Lerp(shnici.transform.position, endTarget.position, endTimer * smoothEnd);

            if (loseTimer >= 4)
            {
                tryAgain.SetActive(true);
                tryAgainButton.SetActive(true);
                //gameOver.gameObject.SetActive(true);
            }
        }

        if (timer >= Random.Range(1.5f, 3))
        {
            CreateTrash();
        }
    }

    private void CreateGift()
    {
        Instantiate(gifts[giftsCounter], new Vector3(20, 2, 5.5f), Quaternion.identity);
        StartSuspenseSound();
        giftsCounter++;
        if (giftsCounter > 2)
        {
            isLastGift = true;
        }
    }

    private void WinGame()
    {
        if (isLastGift)
        {
            timerLastGift += Time.deltaTime;
            timerEnergy = 0;
            timer = 0;
            snakeScript.enabled = false;
        }
        if (timerLastGift > 12)
        {
            timerWin += Time.deltaTime;
            shniciAnim.SetTrigger("winstart");
            jump.gameObject.SetActive(false);
            Shnici.instance.rb.velocity = Vector3.zero;
            Shnici.instance.cc.enabled = false;
            shnici.transform.position = Vector3.Lerp(shnici.transform.position, new Vector3(5,1,0), timerWin);
            isGameOver = true;
            if (timerWin >= 0.8f)
            {
                finalAnim.SetActive(true);
            }
        }
    }

    private void GameOver()
    {
        timerEnergy = 0;
        timer = 0;
        timerGameOver += Time.deltaTime;
        if (timerGameOver >= 15)
        {
            fadeIn.SetActive(true);
        }
        if (timerGameOver >= 15.5f)
        {
            SceneManager.LoadScene("Start_Scene");
        }
    }
    public void TryAgain()
    {
        isGameOver = true;
        timerGameOver = 14.8f;
    }

    public void DecreaseOxygen(int value)
    {
        oxigenLevel.value -= value;
    }

    public void IncreaseOxygen(int value)
    {
        oxigenLevel.value += value;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        isGameStarted = true;
        Time.timeScale = 1;
    }
    

    public void IncreaseCollectedGrams(int grams)
    {
        audioSource.clip = sounds[0];
        audioSource.volume = 0.5f;
        audioSource.Play();
        trashLevel.value += 10;
    }
    public void IncreaseFillment()
    {
        rbin.fillAmount +=  0.005f;
    }

    private void CreateTrash()
    {
        randomTrash = Random.Range(0, trash.Length);
        if (randomTrash == trashNumber && randomTrash != trash.Length - 1)
        {
            randomTrash++;
        }
        if (randomTrash == trashNumber && randomTrash == trash.Length - 1)
        {
            randomTrash--;
        }
        Instantiate(trash[randomTrash], new Vector3(22, Random.Range(minHeght + 1, maxHeght), 5.54f), Quaternion.identity);
        trashNumber = randomTrash;
        fishCounter++;
        if (fishCounter >= Random.Range(1, 2))
        {
            CreateFish();
        }
        timer = 0;
    }

    private void CreateFish()
    {
        fishCounter = 0;
        randomFish = Random.Range(0, badFish.Length);
        if (randomFish == currentFish && randomFish != badFish.Length - 1)
        {
            randomFish++;
        }
        if (randomFish == currentFish && randomFish == badFish.Length - 1)
        {
            randomFish--;
        }
        Instantiate(badFish[randomFish], new Vector3(22, Random.Range(minHeght + 1, maxHeght), 5.54f), Quaternion.LookRotation(Vector3.forward));
        currentFish = randomFish;
    }

    public void StartSuspenseSound()
    {
        suspend.Play();
    }
    public void StopSuspenseSound()
    {
        suspend.Stop();
    }

    public void SoundsOff()
    {
        soundOff.SetActive(false);
        AudioListener.volume = 0;
        soundOn.SetActive(true);
    }
    public void SoundsOn()
    {
        soundOn.SetActive(false);
        AudioListener.volume = 1;
        soundOff.SetActive(true);
    }

}
