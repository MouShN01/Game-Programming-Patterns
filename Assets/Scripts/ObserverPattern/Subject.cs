using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Subject : MonoBehaviour
{
    public event Action ThisHappend;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ThisHappend?.Invoke();
        }
    }
}
