using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager Instance;

    public static int Score;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        // Event subscribing.
        EventManager.Instance.OnWin += (Transform myTransform, Transform otherTransform) => { Score++; };
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnWin -= (Transform myTransform, Transform otherTransform) => { Score++; };
    }
}
