﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverlay : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.J))
        {
            Destroy(gameObject);
        }
    }
}