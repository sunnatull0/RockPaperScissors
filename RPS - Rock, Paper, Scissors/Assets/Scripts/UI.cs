using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    [SerializeField] private Image healthBar;

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
        EventManager.Instance.OnDraw += UpdateHealthBar;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnDraw -= UpdateHealthBar;
    }

    public void UpdateHealthBar(Transform myTransform, Transform otherTransform)
    {
        StartCoroutine(UpdateBarAfterDelay());
    }

    
    private IEnumerator UpdateBarAfterDelay()
    {
        yield return null;
        healthBar.fillAmount = (float)HandsManager.Health / HandsManager.Instance.defaultHealth;
    }

}
