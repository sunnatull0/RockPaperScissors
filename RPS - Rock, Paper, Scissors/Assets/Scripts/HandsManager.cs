using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    public static HandsManager Instance;

    private List<GameObject> handsPool;
    [SerializeField] private List<GameObject> hands;
    [SerializeField] private int amountToPool;

    [Space(10f)]
    [SerializeField] private List<Transform> spawnPositions;

    [SerializeField] private bool isRunning;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        CreatePoolObjects();
        SetRunningGame(true);
        InvokeRepeating(nameof(HandleHands), 2f, 2f);
    }


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

    private void HandleHands()
    {
        while (isRunning)
        {
            ActivatePooledObject();
        }
    }

    private void ActivatePooledObject()
    {
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

    private bool SetRunningGame(bool value)
    {
        isRunning = value;
        return isRunning;
    }

}
