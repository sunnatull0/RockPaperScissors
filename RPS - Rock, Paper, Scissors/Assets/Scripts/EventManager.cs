using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public Action<Transform, Transform> OnWin;
    public Action<Transform, Transform> OnDraw;
    public Action<Transform, Transform> OnLoss;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }



    public void InvokeOnWinActions(Transform myTransform, Transform otherTransform)
    {
        OnWin?.Invoke(myTransform, otherTransform);
    }

    public void InvokeOnDrawActoins(Transform myTransform, Transform otherTransform)
    {
        OnDraw?.Invoke(myTransform, otherTransform);
    }

    public void InvokeOnLossActoins(Transform myTransform, Transform otherTransform)
    {
        OnLoss?.Invoke(myTransform, otherTransform);
    }

}
