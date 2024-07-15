using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    [SerializeField] private Material zombieMat;
    [SerializeField] private Material skeletonMat;

    [SerializeField] private Subject sub;
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        rend.material = zombieMat;
        sub.ThisHappend += OnSpacePressed;
    }

    private void OnSpacePressed()
    {
        if (rend.material.color == zombieMat.color)
        {
            rend.material = skeletonMat;
        }
        else
        {
            rend.material = zombieMat;
        }
    }
}
