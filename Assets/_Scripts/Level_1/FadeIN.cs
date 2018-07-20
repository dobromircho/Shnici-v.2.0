using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FadeIN : MonoBehaviour
{
    Image fade;
    public float timer = 0f;
    public static bool isFadeIN;

    void Start()
    {
        fade = GetComponent<Image>();
        
    }

    void Update()
    {
        timer += Time.deltaTime;
        fade.color = new Color(0, 0, 0, timer * 2);
        if (timer*2 >= 0.8f)
        {
            isFadeIN = true;
            if (timer * 2 > 1.1f)
            {
                timer = 0;
                fade.color = new Color(0, 0, 0, 0);
                gameObject.SetActive(false);

            }
        }
    }
}
