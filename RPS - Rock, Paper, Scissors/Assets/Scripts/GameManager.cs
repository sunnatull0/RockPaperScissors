using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton.
    public static GameManager Instance { get; private set; }

    // Variables.
    [SerializeField] private List<GameObject> mainRightHands;
    private int activeRightHandID;

    [SerializeField] private List<GameObject> mainLeftHands;
    private int activeLeftHandID;


    private void Awake()
    {
        Instance = this;
        ResetAllHands();
        activeRightHandID = 0;
        activeLeftHandID = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ChangeToNextHand(mainRightHands, ref activeRightHandID);
        }
        
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeToNextHand(mainLeftHands, ref activeLeftHandID);
        }
    }

    public void ChangeToNextHand(List<GameObject> mainHands, ref int handID)
    {
        HideCurrentHand(mainHands, ref handID);
        ShowNextHand(mainHands, ref handID);
    }



    private void HideCurrentHand(List<GameObject> mainHands, ref int handID)
    {
        // Deactivating current hand.
        mainHands[handID].SetActive(false);
    }

    private void ShowNextHand(List<GameObject> mainHands, ref int handIDD)
    {
        // Moving to the next hand.
        handIDD++;

        // Reseting handID if it is out of bounds.
        if (handIDD >= mainHands.Count)
            handIDD = 0;

        // Activating the next hand.
        mainHands[handIDD].SetActive(true);
    }

    private void ResetAllHands()
    {
        // Deactivating all right hands.
        foreach (var hand in mainRightHands)
        {
            hand.SetActive(false);
        }

        // Deactivating all left hands.
        foreach (var hand in mainLeftHands)
        {
            hand.SetActive(false);
        }

        // Activating the first right hand.
        mainRightHands[0].SetActive(true);

        // Activating the first left hand.
        mainLeftHands[0].SetActive(true);
    }

}
