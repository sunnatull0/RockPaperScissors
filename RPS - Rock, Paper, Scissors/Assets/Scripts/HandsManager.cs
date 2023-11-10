using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    public static HandsManager Instance;

    [SerializeField] private List<GameObject> hands;
    [SerializeField] private int amountToPool;

    [Space(10f)]
    [SerializeField] private List<Transform> spawnPositions;
    [SerializeField] private float spawnSpeed;

    private List<GameObject> handsPool;
    public static int Health;
    public int defaultHealth;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    private void Start()
    {
        Health = defaultHealth;
        CreatePoolObjects();
        InvokeRepeating(nameof(ActivatePooledObject), 2f, spawnSpeed);

        // Events
        EventManager.Instance.OnDraw += MinusHealth;
        EventManager.Instance.OnLoss += (Transform tr, Transform tr2) => { GameManager.Instance.IsGameActive = false; };
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDraw -= MinusHealth;
        EventManager.Instance.OnLoss -= (Transform tr, Transform tr2) => { };
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


    // Hands Health
    private void MinusHealth(Transform transform, Transform transform2)
    {
        Health--;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if(Health <= 0)
        {
            GameManager.Instance.IsGameActive = false;
            Debug.Log("Over!");
        }
    }

}
