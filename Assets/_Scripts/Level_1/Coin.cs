using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Transform target;
    public Animator rbAnim;
    public float smoothMove;
    float timer;
    float upTimer;
    float upSpeed;
    float distance;
    bool hit;
    bool isUp;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("rb").GetComponent<Transform>();
        rbAnim = GameObject.FindGameObjectWithTag("rbAnim").GetComponent<Animator>();
        rbAnim.SetBool("rbhit", false);

    }

    void Update()
    {
        upTimer += Time.deltaTime;
        if (upTimer >= 0.5f)
        {
            isUp = !isUp;
            upTimer = 0;
        }
        if (isUp)
        {
            upSpeed = 12;
        }
        else
        {
            upSpeed = -5;
        }
        transform.Translate(0, upSpeed * Time.deltaTime, 0);
        timer += Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, target.position, timer * smoothMove);
        distance = Vector3.Distance(transform.position, target.position);
        if (distance < 1.7f)
        {
            rbAnim.ResetTrigger("rbhit");
        }
        if (distance < 1.5f)
        {
            rbAnim.SetTrigger("rbhit");
            GameManager.instance.IncreaseFillment();
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "rb")
        {
            GameManager.instance.IncreaseFillment();
        }
    }
}
