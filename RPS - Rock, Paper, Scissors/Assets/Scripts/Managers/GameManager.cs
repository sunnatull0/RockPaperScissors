using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum State
    {
        CountingDown, Playing, GameOver,
    }
    public State state;


    public bool IsCountingDown { get { return state == State.CountingDown; } private set { } }
    public bool IsGameActive { get { return state == State.Playing; } private set { } }
    public bool isGameOver { get { return state == State.GameOver; } private set { } }

    [SerializeField] private float countDownTimer = 3f;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        state = State.CountingDown;
    }

    private void Start()
    {
        // Event subscribing.
        EventManager.Instance.OnWin += (Transform myTransform, Transform otherTransform) => { otherTransform.gameObject.SetActive(false); };
        EventManager.Instance.OnDraw += (Transform myTransform, Transform otherTransform) => { otherTransform.gameObject.SetActive(false); };
        EventManager.Instance.OnLoss += (Transform myTransform, Transform otherTransform) => { state = State.GameOver; };
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnWin -= (Transform myTransform, Transform otherTransform) => { otherTransform.gameObject.SetActive(false); };
        EventManager.Instance.OnDraw -= (Transform myTransform, Transform otherTransform) => { otherTransform.gameObject.SetActive(false); };
        EventManager.Instance.OnLoss -= (Transform myTransform, Transform otherTransform) => { state = State.GameOver; };
    }

    private void Update()
    {
        switch (state)
        {
            case State.CountingDown:
                EventManager.Instance.OnStateChanged?.Invoke();

                countDownTimer -= Time.deltaTime;
                if (countDownTimer <= 0f)
                    state = State.Playing;
                break;

            case State.Playing:
                EventManager.Instance.OnStateChanged?.Invoke();
                break;

            case State.GameOver:
                EventManager.Instance.OnStateChanged?.Invoke();
                break;
        }
    }

    public float GetCountDownTimer()
    {
        return countDownTimer;
    }


    public void GameOverButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
