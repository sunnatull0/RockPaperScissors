using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton.
    public static GameManager Instance { get; private set; }


    // Variables.

    // Right hand.
    [SerializeField] private List<GameObject> mainRightHands;
    private int activeRightHandID;

    // Left hand.
    [SerializeField] private List<GameObject> mainLeftHands;
    private int activeLeftHandID;


    private void OnEnable()
    {
        EventManager.Instance.OnWin += OnWinActions;
        EventManager.Instance.OnDraw += OnDrawActions;
        EventManager.Instance.OnLoss += OnLossActions;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnWin -= OnWinActions;
        EventManager.Instance.OnDraw -= OnDrawActions;
        EventManager.Instance.OnLoss -= OnLossActions;
    }

    private void Awake()
    {
        Instance = this;
        ResetAllHands();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeToNextHand(mainRightHands, ref activeRightHandID);
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeToNextHand(mainLeftHands, ref activeLeftHandID);
        }
    }

    private void ChangeToNextHand(List<GameObject> mainHands, ref int handID)
    {
        HideCurrentHand(mainHands, ref handID);
        ShowNextHand(mainHands, ref handID);
    }

    private void HideCurrentHand(List<GameObject> mainHands, ref int handID)
    {
        // Deactivating current hand.
        mainHands[handID].SetActive(false);
    }

    private void ShowNextHand(List<GameObject> mainHands, ref int handID)
    {
        // Moving to the next hand.
        handID++;

        // Reseting handID if it is out of bounds.
        if (handID >= mainHands.Count)
            handID = 0;

        // Activating the next hand.
        mainHands[handID].SetActive(true);
    }

    private void ResetAllHands()
    {
        // Deactivating all right hands.
        foreach (var hand in mainRightHands)
        {
            hand.SetActive(false);
        }

        // Activating the first right hand.
        mainRightHands[0].SetActive(true);
        activeRightHandID = 0;


        // Deactivating all left hands.
        foreach (var hand in mainLeftHands)
        {
            hand.SetActive(false);
        }

        // Activating the first left hand.
        mainLeftHands[0].SetActive(true);
        activeLeftHandID = 0;
    }


    // Event Actions.
    private void OnWinActions()
    {
        Debug.Log("Win!");
    }

    private void OnDrawActions()
    {
        Debug.Log("Draw!");
    }

    private void OnLossActions()
    {
        Debug.Log("Loss!");
    }

}
