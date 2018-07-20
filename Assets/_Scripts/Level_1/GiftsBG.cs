using UnityEngine;

public class GiftsBG : MonoBehaviour
{
    Animator anim;
    float timer;
    public bool isTaken;
    bool repeat;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (isTaken)
        {
            timer += Time.deltaTime;
            if (timer >= 3 && !repeat)
            {
                anim.SetBool("repeat", true);
                repeat = true;
                timer = 0;
            }
            if (repeat && timer >= 0.7f)
            {
                anim.SetBool("repeat", false);
                repeat = false;
            }
        }
        
    }
}
