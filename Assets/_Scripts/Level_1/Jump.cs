﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    
    void OnMouseDown()
    {
        Shnici.instance.Jump();
    }
}
