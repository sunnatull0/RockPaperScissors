using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; private set; }

    [SerializeField] private ParticleSystem handEffectPrefab;
    private ParticleSystem handEffect;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        handEffect = Instantiate(handEffectPrefab, GameManager.Instance.InGameCreatedObjects);

        // Event subscribings.
        EventManager.Instance.OnHandChange += PlayHandEffect;
    }

    private void OnDisable()
    {
        // Event unsubscribings.
        EventManager.Instance.OnHandChange -= PlayHandEffect;
    }


    private void PlayHandEffect(Transform pos)
    {
        handEffect.transform.position = pos.position;
        handEffect.Play();
    }

}
