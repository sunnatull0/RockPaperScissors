using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    [Header("RIGHT HANDS")]
    [SerializeField] private List<GameObject> mainRightHands;
    private int activeRightHandID;

    [Header("LEFT HANDS")]
    [SerializeField] private List<GameObject> mainLeftHands;
    private int activeLeftHandID;

    private bool isGameActive = false;
    public bool IsGameActive { get { return isGameActive; } set { isGameActive = value; } }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        ResetAllHands();
    }

    private void Start()
    {
        isGameActive = true;

        // Events
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && IsGameActive)
        {
            ChangeToNextHand(mainRightHands, ref activeRightHandID);
        }

        if (Input.GetKeyDown(KeyCode.T) && IsGameActive)
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
    private void OnWinActions(Transform myTransform, Transform otherTransform)
    {
        otherTransform.gameObject.SetActive(false);
    }

    private void OnDrawActions(Transform myTransform, Transform otherTransform)
    {
        otherTransform.gameObject.SetActive(false);
    }

    private void OnLossActions(Transform myTransform, Transform otherTransform)
    {
        //myTransform.gameObject.SetActive(false);
        Debug.Log("Game Over!");
    }
}
