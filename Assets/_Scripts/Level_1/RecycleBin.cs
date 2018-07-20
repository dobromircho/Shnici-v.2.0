using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleBin : MonoBehaviour
{
    Animator anim;
    public bool pressed;
    public float timerAnim;
    public float animOffset;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("RBAnim");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            pressed = !pressed;
        }
        if (pressed)
        {

            anim.speed = 0;
            timerAnim = 0;
        }
        else
        {
            anim.speed = 1;
            timerAnim += Time.deltaTime;
            if (timerAnim >= animOffset)
            {
                anim.speed = 0;
            }
        }
    }
}
