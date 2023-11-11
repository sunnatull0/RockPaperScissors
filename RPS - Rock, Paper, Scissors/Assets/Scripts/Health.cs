using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public static Health Instance;

    public static int _Health;
    public int defaultHealth;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        _Health = defaultHealth;

        // Event subscribing.
        EventManager.Instance.OnDraw += MinusHealth;
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnDraw -= MinusHealth;
    }



    private void MinusHealth(Transform transform, Transform transform2)
    {
        _Health--;
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        if (_Health <= 0)
        {
            _Health = 0;
            GameManager.Instance.state = GameManager.State.GameOver;
        }
    }

}
