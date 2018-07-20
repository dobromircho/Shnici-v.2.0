using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoryScript : MonoBehaviour
{
    AudioSource voice;
    float timer;

    void Start()
    {
        voice = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            voice.Play();
        }
    }
}
