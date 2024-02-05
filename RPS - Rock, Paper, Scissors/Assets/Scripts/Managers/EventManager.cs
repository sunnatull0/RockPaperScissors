using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public Action<Transform, Transform> OnWin;
    public Action<Transform, Transform> OnDraw;
    public Action<Transform, Transform> OnLoss;

    public Action<Transform> OnHandChange;

    public Action OnStateChanged;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
