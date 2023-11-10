using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    private bool isGameActive = false;
    public bool IsGameActive { get { return isGameActive; } set { isGameActive = value; } }


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        isGameActive = true;

        // Event subscribing.
        EventManager.Instance.OnWin += OnWinActions;
        EventManager.Instance.OnDraw += OnDrawActions;
        EventManager.Instance.OnLoss += (Transform tr, Transform tr2) => { IsGameActive = false; };
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnWin -= OnWinActions;
        EventManager.Instance.OnDraw -= OnDrawActions;
        EventManager.Instance.OnLoss -= (Transform tr, Transform tr2) => { IsGameActive = false; };
    }



    // Event Actions.
    private void OnWinActions(Transform myTransform, Transform otherTransform)
    {
        otherTransform.gameObject.SetActive(false);
    }

    private void OnDrawActions(Transform myTransform, Transform otherTransform)
    {
        otherTransform.gameObject.SetActive(false);
    }
}
