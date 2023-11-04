using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public Action OnWin;
    public Action OnDraw;
    public Action OnLoss;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void InvokeOnWinActions()
    {
        OnWin?.Invoke();
    }

    public void InvokeOnDrawActoins()
    {
        OnDraw?.Invoke();
    }

    public void InvokeOnLossActoins()
    {
        OnLoss?.Invoke();
    }

}
