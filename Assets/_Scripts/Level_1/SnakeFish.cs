using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeFish : MonoBehaviour
{
    public Material bgMaterial;
    public GameObject fish;
    public float speed;
    public bool isInstantiated;
    public bool isCounted;
    public int counter;

    void Start()
    {
        bgMaterial.mainTextureOffset = new Vector2(0.55f, 0);
    }

    void FixedUpdate()
    {
        if (!isInstantiated)
        {
            if (bgMaterial.mainTextureOffset.x >= 0.65f && bgMaterial.mainTextureOffset.x <0.66f)
            {
                isInstantiated = true;
                counter++;
                if (counter % 3 == 0 )
                {
                    Instantiate(fish);
                }
            }

        }
        if (bgMaterial.mainTextureOffset.x >= 0.99f)
        {
            
            isInstantiated = false;
        }

    }
}
