using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    public static HandsManager Instance;

    // Hands Manage variables.
    [Header("RIGHT HANDS")]
    [SerializeField] private List<GameObject> mainRightHands;
    private int activeRightHandID;

    [Header("LEFT HANDS")]
    [SerializeField] private List<GameObject> mainLeftHands;
    private int activeLeftHandID;


    // Object Pooling variables.
    [SerializeField] private List<GameObject> hands;
    [SerializeField] private int amountToPool;

    [Space(10f)]

    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private float spawnSpeed;
    private List<GameObject> handsPool;


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
        CreatePoolObjects();
        InvokeRepeating(nameof(ActivatePooledObject), 2f, spawnSpeed); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && GameManager.Instance.IsGameActive)
        {
            ChangeToNextHand(mainRightHands, ref activeRightHandID);
        }

        if (Input.GetKeyDown(KeyCode.T) && GameManager.Instance.IsGameActive)
        {
            ChangeToNextHand(mainLeftHands, ref activeLeftHandID);
        }
    }


    // Hands Managing
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




    // Object Pooling.
    private void CreatePoolObjects()
    {
        handsPool = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            for (int j = 0; j < hands.Count; j++)
            {
                GameObject hand = Instantiate(hands[j]);
                handsPool.Add(hand);
                hand.SetActive(false);
            }
        }
    }

    private void ActivatePooledObject()
    {
        if (!GameManager.Instance.IsGameActive)
            return;

        GameObject obj = GetPoolObject();

        if (obj != null)
        {
            int randomID = Random.Range(0, spawnPositions.Count);

            obj.transform.position = spawnPositions[randomID].position;
            obj.SetActive(true);
        }
        else
        {
            Debug.LogError("No inactive objects in the hierarchy! Check GetPoolObject()");
        }
    }

    private GameObject GetPoolObject()
    {
        List<int> chosenIDs = new List<int>();

        while (HasInactiveObjects())
        {
            int randomID = Random.Range(0, handsPool.Count);

            if (chosenIDs.Contains(randomID))
                continue;

            chosenIDs.Add(randomID);
            if (!handsPool[randomID].activeInHierarchy)
            {
                return handsPool[randomID];
            }
        }
        return null;
    }

    private bool HasInactiveObjects()
    {
        foreach (GameObject item in handsPool)
        {
            if (!item.activeInHierarchy)
                return true;
        }
        return false;
    }

}
