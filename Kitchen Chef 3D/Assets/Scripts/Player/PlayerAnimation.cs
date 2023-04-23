using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Action OnPickStart, OnPickFinished, OnDropStart, OnDropFinished;
    public Animator animator { get; private set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PickStart()
    {
        if(OnPickStart != null)
        {
            OnPickStart();
        }
    }

    public void PickFinished()
    {
        if(OnPickFinished != null)
        {
            OnPickFinished();
        }
    }

    public void DropStart()
    {
        if(OnDropStart != null)
        {
            OnDropStart();
        }
    }
    public void DropFinished()
    {
        if(OnDropFinished != null)
        {
            OnDropFinished();
        }
    }
}
