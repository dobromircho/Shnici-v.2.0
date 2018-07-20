using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "SnakeFish")
        {
            Destroy(other.transform.parent.gameObject);
        }
        else
        {
            Destroy(other.gameObject);
        }

    }
}
