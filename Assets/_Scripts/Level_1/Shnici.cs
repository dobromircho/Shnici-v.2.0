using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shnici : MonoBehaviour
{
    public static Shnici instance;

    Dictionary<string, int> trashGrams = new Dictionary<string, int>();
    AudioSource sound;
    AudioSource hitSound;

    public Rigidbody rb;
    public CapsuleCollider cc;
    public GameObject hitRedfish;
    public GameObject hitfish;
    public GameObject coin;
    public GameObject attractor;
    public Transform attractorPoint;
    public GameObject[] gramsPrefabs;
    public Slider oxygenLevel;

    Dictionary<int,GameObject> gramsText = new Dictionary<int, GameObject>();
    Image oxigenColor;
    Color oxigenRealColor;
    Animator anim;

    public float forceDown;
    public float forceUp;
    public float oxyRecharge;
    public float oxyDecharge;

    float timerRed;
    float timerGreen;
    bool isJump;
    bool isZeroVelocity;
    bool isRed;
    bool isGreen;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CapsuleCollider>();
        instance = this;
        oxigenColor = GameObject.FindGameObjectWithTag("oxygenColor").GetComponent<Image>();
        oxigenRealColor = oxigenColor.color;
        //color = GameObject.FindGameObjectWithTag("color").GetComponent<SpriteRenderer>();
        sound = GetComponent<AudioSource>();
        hitSound = GameObject.FindGameObjectWithTag("color").GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        trashGrams.Add("bag", 5);
        trashGrams.Add("can", 10);
        trashGrams.Add("yogurt", 20);
        trashGrams.Add("net", 80);
        trashGrams.Add("flipflop", 120);

        gramsText.Add(5, gramsPrefabs[0]);
        gramsText.Add(10, gramsPrefabs[1]);
        gramsText.Add(20, gramsPrefabs[2]);
        gramsText.Add(80, gramsPrefabs[3]);
        gramsText.Add(120, gramsPrefabs[4]);
    }
    
    void FixedUpdate()
    {
        timerRed += Time.deltaTime;
        timerGreen += Time.deltaTime;
        ColorChange();
        CheckVelocity();
        PushBodyDown();
        HoldBodyUp(10);
        HoldBodyDown(-10);
    }

    public void Jump()
    {
        isJump = true;
        rb.velocity = Vector3.zero;
        isZeroVelocity = true;
        rb.AddForce(Vector3.up * forceUp, ForceMode.Impulse);
        sound.Play();

    }

    void OnTriggerEnter (Collider other)
    {
        if (trashGrams.ContainsKey(other.gameObject.tag))
        {
            Instantiate(hitfish, other.transform.position, Quaternion.identity);
            Instantiate(coin, other.transform.position, Quaternion.identity);
            GameManager.instance.IncreaseCollectedGrams(10);
            GameManager.instance.IncreaseOxygen(5);
            Destroy(other.gameObject);
            oxigenColor.color = Color.green;
            isGreen = true;
            timerGreen = 0;
        }
        if (other.gameObject.tag == "badfish")
        {
            Instantiate(hitRedfish, other.transform.position, Quaternion.identity);
            //Destroy(other.gameObject);
            other.gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            GameManager.instance.DecreaseOxygen(20);
            anim.SetTrigger("hit");
            hitSound.Play();
            oxigenColor.color = Color.red;
            isRed = true;
            timerRed = 0;
        }
    }

    void HoldBodyUp(int value)
    {
        if (transform.position.y >= value)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
            GameManager.instance.IncreaseOxygen(2);
        }
    }
    void HoldBodyDown(int value)
    {
        if (transform.position.y <= value)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
    }
    void PushBodyDown()
    {
        if (!isJump)
        {
            if (!isZeroVelocity)
            {
                rb.velocity = Vector3.zero;
            }
            rb.AddForce(Vector3.down * forceDown, ForceMode.Force);
        }
    }
    void ColorChange()
    {
        if (timerRed >= 0.2f && isRed)
        {
            //color.color = Color.white;
            oxigenColor.color = oxigenRealColor;
            isRed = false;
        }
        if (timerGreen >= 0.15f && isGreen)
        {
            oxigenColor.color = oxigenRealColor;
            isGreen = false;
        }
    }
    void CheckVelocity()
    {
        if (rb.velocity.y == 0)
        {
            isJump = false;
        }
    }
}

