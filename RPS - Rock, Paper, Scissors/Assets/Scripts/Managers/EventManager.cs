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

    public Action OnStateChanged;

    public Action<Transform> OnHandChange;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }
}
