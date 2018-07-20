using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gifts : MonoBehaviour
{
    GameObject starBG;
    GameObject shelfishBG;
    GameObject perlBG;
    GiftsBG giftsBGScript;
    Transform shnici;

    Animator anim;
    Animator starBGAnim;
    Animator shelfishBGAnim;
    Animator perlBGAnim;

    AudioSource sound;

    public float speed;
    public float upSpeed;
    float upTimer;
    float timer;

    bool isUp;
    bool isHit;
    public bool isLastGift;

    public bool isStar;
    public bool isShellfish;
    public bool isPerl;


    void Start()
    {
        shnici = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        anim = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        starBG = GameObject.FindGameObjectWithTag("starBG");
        shelfishBG = GameObject.FindGameObjectWithTag("shellfishBG");
        perlBG = GameObject.FindGameObjectWithTag("perlBG");
        starBGAnim = starBG.GetComponent<Animator>();
        shelfishBGAnim = shelfishBG.GetComponent<Animator>();
        perlBGAnim = perlBG.GetComponent<Animator>();
        timer = -0.65f;
    }


    void Update()
    {
        if (!isHit)
        {
            MoveGifts();
        }
        else
        {
            GiftsTaken();
        }
       float distance = transform.position.x + shnici.position.x;
        if (distance < -25)
        {
            GameManager.instance.StopSuspenseSound();
        }
    }

    private void GiftsTaken()
    {
        timer += Time.deltaTime;
        if (isStar)
        {
            transform.position = Vector3.Lerp(transform.position, starBG.transform.position, timer);
        }
        if (isShellfish)
        {
            transform.position = Vector3.Lerp(transform.position, shelfishBG.transform.position, timer);
        }
        if (isPerl)
        {
            transform.position = Vector3.Lerp(transform.position, perlBG.transform.position, timer);
        }
        if (timer >= 0.45f)
        {
            if (isStar)
            {
                starBGAnim.SetTrigger("Taken");
            }
            if (isShellfish)
            {
                shelfishBGAnim.SetTrigger("Taken");
            }
            if (isPerl)
            {
                perlBGAnim.SetTrigger("Taken");
            }
        }
    }

    private void MoveGifts()
    {
        upTimer += Time.deltaTime;
        if (upTimer >= 1f)
        {
            isUp = !isUp;
            upTimer = 0f;
        }
        if (isUp)
        {
            upSpeed = 2;
        }
        else
        {
            upSpeed = -2;
        }
        transform.Translate(-speed * Time.deltaTime, upSpeed * Time.deltaTime, 0, Space.World);
        transform.Rotate(0, 0, 60 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isHit)
        {
            transform.eulerAngles = Vector3.zero;
            transform.position = new Vector3(0, 1, 0f);
            isHit = true;
            anim.SetTrigger("hit");
            sound.Play();
            GameManager.instance.StopSuspenseSound();
            if (isPerl)
            {
            }
        }
        //if (other.tag == "endLine")
        //{
        //    GameManager.instance.StopSuspenseSound();
        //
        //}
    }
}
