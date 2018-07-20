using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BadFish : MonoBehaviour
{
    public bool isBalloonFish;
    public bool isSnakeFish;
    MeshRenderer color;
    float speed;
    public float snakeSpeed;
    public float timer;
    Animator anim;

    void Start()
    {
        speed = Random.Range(10, 15);
        //color = GetComponentInChildren<MeshRenderer>();
        //transform.Rotate(0, 0, 60);
        anim = GetComponentInChildren<Animator>();   
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (isBalloonFish)
        {
            if (timer >= 1)
            {
                anim.SetTrigger("popup");

            }
            if (timer >= 2)
            {
                anim.SetTrigger("popupswim");
            }
            
        }
        if (!isSnakeFish)
        {
            this.transform.Translate(-speed * Time.deltaTime, 0, 0);

        }
        else
        {
            this.transform.Translate(-snakeSpeed * Time.deltaTime, 0, 0);
        }
    }
}
