using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] private Image healthBar;
    private float currentValue;
    private float targetValue;

    [SerializeField] private float healthChangeSpeed;

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
        currentValue = Health._Health;
        
        // Event subscribing.
        EventManager.Instance.OnDraw += UpdateHealthBar;
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnDraw -= UpdateHealthBar;
    }

    public void UpdateHealthBar(Transform myTransform, Transform otherTransform)
    {
        currentValue = (float)Health._Health / Health.Instance.defaultHealth;

        // Update bar after health has been changed
        StartCoroutine(UpdateBarAfterDelay());
    }

    
    private IEnumerator UpdateBarAfterDelay()
    {
        yield return null;

        targetValue = (float)Health._Health / Health.Instance.defaultHealth;

        // Decreasing fillAmount smoothly.
        StartCoroutine(DecreaseSmoothly());
    }

    private IEnumerator DecreaseSmoothly()
    {
        while (currentValue > targetValue)
        {
            currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * healthChangeSpeed);

            healthBar.fillAmount = currentValue;

            yield return null;
        }

        currentValue = targetValue;

        healthBar.fillAmount = currentValue;
    }

}
