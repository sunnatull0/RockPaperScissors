using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    
    public static UI Instance;

    #region Variables.
    [Header("Health")]
    [SerializeField] private GameObject healthParent;
    [SerializeField] private Image healthBar;
    [SerializeField] private float healthChangeSpeed;
    private float currentValue;
    private float targetValue;

    [Header("Score")]
    [SerializeField] private GameObject scoreParent;
    [SerializeField] private TMP_Text scoreText;

    [Header("WaitingToStart")]
    [SerializeField] private GameObject waitingParent;

    [Header("CountDown")]
    [SerializeField] private GameObject countDownParent;
    [SerializeField] private TMP_Text countText;

    [Header("Playing")]
    [SerializeField] private GameObject startPlayingParent;
    [SerializeField] private GameObject winEffect;
    [SerializeField] private GameObject drawEffect;

    [Header("GameOver")]
    [SerializeField] private GameObject gameOverParent;
    #endregion


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        HideAll();
        Show(waitingParent);

        currentValue = Health._Health;
        UpdateScore();

        // Event subscribing.
        EventManager.Instance.OnWin += UpdateScoreUI;
        EventManager.Instance.OnDraw += UpdateHealthBar;
        EventManager.Instance.OnStateChanged += OnStateChangedActions;
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnWin -= UpdateScoreUI;
        EventManager.Instance.OnDraw -= UpdateHealthBar;
        EventManager.Instance.OnStateChanged -= OnStateChangedActions;
    }

    private void Update()
    {
        countText.text = Mathf.Ceil(GameManager.Instance.GetCountDownTimer()).ToString();
    }

    #region Health Methods.
    // Health.
    private void UpdateHealthBar(Transform myTransform, Transform otherTransform)
    {
        currentValue = (float)Health._Health / Health.Instance.defaultHealth;

        // Update bar after health has been changed.
        GameManager.Instance.PlayAfterDelay(Decrease);
    }

    private void Decrease()
    {
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
    #endregion


    #region Score Methods.
    // Score.
    private void UpdateScoreUI(Transform myTransform, Transform otherTransform) => GameManager.Instance.PlayAfterDelay(UpdateScore);

    private void UpdateScore() => scoreText.text = ScoreManager.Score.ToString();
    #endregion


    #region State handling.
    // UI handling depending on state.
    private void OnStateChangedActions()
    {
        // Handle UI when Game is waiting to start.
        if (GameManager.Instance.IsWaiting)
            Show(waitingParent);
        else
            Hide(waitingParent);

        // Handle UI when Game is countingDown.
        if (GameManager.Instance.IsCountingDown)
            Show(countDownParent);
        else
            Hide(countDownParent);


        // Handle UI when Game is playing.
        if (GameManager.Instance.IsGameActive)
        {
            Show(healthParent);
            Show(scoreParent);
            Show(startPlayingParent);
        }
        else
        {
            Hide(healthParent);
            Hide(scoreParent);
            Hide(startPlayingParent);
        }


        // Handle UI when Game is over.
        if (GameManager.Instance.isGameOver)
            Show(gameOverParent);
        else
            Hide(gameOverParent);
    }

    private void Show(GameObject obj) => obj.SetActive(true);

    private void Hide(GameObject obj) => obj.SetActive(false);

    private void HideAll()
    {
        Hide(healthParent);
        Hide(scoreParent);
        Hide(waitingParent);
        Hide(countDownParent);
        Hide(startPlayingParent);
        Hide(gameOverParent);
    }
    #endregion
}
