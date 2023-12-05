using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI : MonoBehaviour
{

    public static UI Instance;

    #region Variables.
    [Header("Health")]
    [SerializeField] private GameObject healthParent;
    [SerializeField] private Image healthBar;
    [SerializeField] private float healthChangeSpeed;
    private float currentHealthValue;
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
    [SerializeField] private TMP_Text gameOverText;
    #endregion


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Hide(
            healthParent,
            scoreParent,
            startPlayingParent,
            waitingParent,
            countDownParent,
            gameOverParent
            );

        Show(waitingParent);

        currentHealthValue = Health._Health;
        UpdateScore();

        // Event subscribing.
        EventManager.Instance.OnWin += UpdateScoreUI;
        EventManager.Instance.OnDraw += UpdateHealthBar;
        EventManager.Instance.OnLoss += UpdateGameOverPanel;
        EventManager.Instance.OnLoss += ActivateGameOver;
        EventManager.Instance.OnStateChanged += OnStateChangedActions;
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnWin -= UpdateScoreUI;
        EventManager.Instance.OnDraw -= UpdateHealthBar;
        EventManager.Instance.OnLoss -= UpdateGameOverPanel;
        EventManager.Instance.OnLoss -= ActivateGameOver;
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
        currentHealthValue = (float)Health._Health / Health.Instance.defaultHealth;

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
        while (currentHealthValue > targetValue)
        {
            currentHealthValue = Mathf.Lerp(currentHealthValue, targetValue, Time.deltaTime * healthChangeSpeed);

            healthBar.fillAmount = currentHealthValue;

            yield return null;
        }

        currentHealthValue = targetValue;

        healthBar.fillAmount = currentHealthValue;
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
        {
            Show(waitingParent);
        }
        else
        {
            Hide(waitingParent);
        }

        // Handle UI when Game is countingDown.
        if (GameManager.Instance.IsCountingDown)
        {
            Show(countDownParent);
        }
        else
        {
            Hide(countDownParent);
        }


        // Handle UI when Game is playing.
        if (GameManager.Instance.IsGameActive)
        {
            Show(
                healthParent,
                scoreParent,
                startPlayingParent
                );
        }
        else
        {
            Hide(
                healthParent,
                scoreParent,
                startPlayingParent
                );
        }


        // Handle UI when Game is over.
        if (GameManager.Instance.isGameOver)
        {
            Show(gameOverParent);
        }
        else
        {
            Hide(gameOverParent);
        }
    }

    private void Show(params GameObject[] args)
    {
        foreach (GameObject obj in args)
        {
            obj.SetActive(true);
        }
    }

    private void Hide(params GameObject[] args)
    {
        foreach (GameObject obj in args)
        {
            obj.SetActive(false);
        }
    }
    #endregion


    #region GameOver Panel.
    private void UpdateGameOverPanel(Transform myTransform, Transform otherTransform)
    {
        // Variable for GameOver text.
        string title;

        // Changing the variable depending on the Score.
        switch (ScoreManager.Score)
        {
            case 0:
                title = "Ooops! No win for you.";
                break;

            case 1:
                title = "Not bad! You defeated only 1 hand.";
                break;
            case > 1:

                if (ScoreManager.Score < 10) title = $"Nice try! You only defeated {ScoreManager.Score} hands.";

                else if (ScoreManager.Score < 20) title = $"Great! You defeated {ScoreManager.Score} hands.";

                else title = $"WOW! You defeated {ScoreManager.Score} hands!!!";

                break;

            default:
                title = "Oops! Something went wrong.";
                break;
        }

        gameOverText.text = title;
    }

    private void ActivateGameOver(Transform myTransform, Transform otherTransform)
    {
        gameOverParent.transform.DOLocalMoveY(0f, 1.5f).SetEase(Ease.OutBounce);
    }
    #endregion
}
