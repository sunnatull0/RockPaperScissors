using UnityEngine;
using UnityEngine.SceneManagement;

public class EffectsManager : MonoBehaviour
{
    public static EffectsManager Instance { get; private set; }


    #region Variables.
    // HandChange effect.
    [SerializeField] private ParticleSystem handChangeEffectPrefab;
    private ParticleSystem handChangeEffect;
    private ParticleSystem handChangeEffect2;

    // Win effect.
    [SerializeField] private ParticleSystem winEffectPrefab;
    private ParticleSystem winEffect;
    private ParticleSystem winEffect2;

    // Draw effect.
    [SerializeField] private ParticleSystem drawEffectPrefab;
    private ParticleSystem drawEffect;
    private ParticleSystem drawEffect2;

    // Loss effect.
    [SerializeField] private ParticleSystem lossEffectPrefab;
    private ParticleSystem lossEffect;
    #endregion


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
        // Event subscribings.
        EventManager.Instance.OnHandChange += PlayHandEffect;
        EventManager.Instance.OnWin += PlayWinEffect;
        EventManager.Instance.OnDraw += PlayDrawEffect;
        EventManager.Instance.OnLoss += PlayLossEffect;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }


    private void OnDisable()
    {
        // Event unsubscribings.
        EventManager.Instance.OnHandChange -= PlayHandEffect;
        EventManager.Instance.OnWin -= PlayWinEffect;
        EventManager.Instance.OnDraw -= PlayDrawEffect;
        EventManager.Instance.OnLoss -= PlayLossEffect;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            InstantiateAll();
        }
    }


    private void PlayHandEffect(Transform myPos)
    {
        PlayUnactiveParticle(handChangeEffect, handChangeEffect2, myPos);
    }

    private void PlayWinEffect(Transform myPos, Transform _transform)
    {
        PlayUnactiveParticle(winEffect, winEffect2, myPos);
    }

    private void PlayDrawEffect(Transform myPos, Transform _transform)
    {
        PlayUnactiveParticle(drawEffect, drawEffect2, myPos);
    }

    private void PlayLossEffect(Transform myPos, Transform _transform)
    {
        lossEffect.Play();
    }



    private void PlayUnactiveParticle(ParticleSystem particleToPlayFirst, ParticleSystem particleToPlaySecond, Transform _transform)
    {
        if (!particleToPlayFirst.isPlaying)
        {
            particleToPlayFirst.transform.position = _transform.position;
            particleToPlayFirst.Play();
        }
        else
        {
            particleToPlaySecond.transform.position = _transform.position;
            particleToPlaySecond.Play();
        }
    }

    private void InstantiateAll()
    {
        // Two effects to prevent repeated effect bug!
        handChangeEffect = Instantiate(handChangeEffectPrefab, GameManager.Instance.InGameCreatedObjects);
        handChangeEffect2 = Instantiate(handChangeEffectPrefab, GameManager.Instance.InGameCreatedObjects);

        winEffect = Instantiate(winEffectPrefab, GameManager.Instance.InGameCreatedObjects);
        winEffect2 = Instantiate(winEffectPrefab, GameManager.Instance.InGameCreatedObjects);

        drawEffect = Instantiate(drawEffectPrefab, GameManager.Instance.InGameCreatedObjects);
        drawEffect2 = Instantiate(drawEffectPrefab, GameManager.Instance.InGameCreatedObjects);

        lossEffect = Instantiate(lossEffectPrefab, GameManager.Instance.InGameCreatedObjects);
    }

}
