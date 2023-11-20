using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; private set; }

    // HandChange effect.
    [SerializeField] private ParticleSystem handChangeEffectPrefab;
    private ParticleSystem handChangeEffect;

    // Win effect.
    [SerializeField] private ParticleSystem winEffectPrefab;
    private ParticleSystem winEffect;

    // Draw effect.
    [SerializeField] private ParticleSystem drawEffectPrefab;
    private ParticleSystem drawEffect;

    // Loss effect.
    [SerializeField] private ParticleSystem lossEffectPrefab;
    private ParticleSystem lossEffect;



    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        handChangeEffect = Instantiate(handChangeEffectPrefab, GameManager.Instance.InGameCreatedObjects);
        winEffect = Instantiate(winEffectPrefab, GameManager.Instance.InGameCreatedObjects);
        drawEffect = Instantiate(drawEffectPrefab, GameManager.Instance.InGameCreatedObjects);
        lossEffect = Instantiate(lossEffectPrefab, GameManager.Instance.InGameCreatedObjects);

        // Event subscribings.
        EventManager.Instance.OnHandChange += PlayHandEffect;
        EventManager.Instance.OnWin += PlayWinEffect;
        EventManager.Instance.OnDraw += PlayDrawEffect;
        EventManager.Instance.OnLoss += PlayLossEffect;
    }

    private void OnDisable()
    {
        // Event unsubscribings.
        EventManager.Instance.OnHandChange -= PlayHandEffect;
        EventManager.Instance.OnWin -= PlayWinEffect;
        EventManager.Instance.OnDraw -= PlayDrawEffect;
        EventManager.Instance.OnLoss -= PlayLossEffect;
    }


    private void PlayHandEffect(Transform pos)
    {
        handChangeEffect.transform.position = pos.position;
        handChangeEffect.Play();
    }

    private void PlayWinEffect(Transform myPos, Transform _transform)
    {
        winEffect.transform.position = myPos.position;
        winEffect.Play();
    }

    private void PlayDrawEffect(Transform myPos, Transform _transform)
    {
        drawEffect.transform.position = myPos.position;
        drawEffect.Play();
    }

    private void PlayLossEffect(Transform myPos, Transform _transform)
    {
        lossEffect.Play();
    }

}
