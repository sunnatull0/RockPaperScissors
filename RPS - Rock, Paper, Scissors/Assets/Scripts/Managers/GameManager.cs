using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum State
    {
        WaitingToStart, CountingDown, Playing, GameOver,
    }
    public State state;


    public bool IsWaiting { get { return state == State.WaitingToStart; } private set { } }
    public bool IsCountingDown { get { return state == State.CountingDown; } private set { } }
    public bool IsGameActive { get { return state == State.Playing; } private set { } }
    public bool isGameOver { get { return state == State.GameOver; } private set { } }


    [SerializeField] private float waitingToStart = 0.5f;
    [SerializeField] private float countDownTimer = 3f;

    // This transform will be used as a Parent for all instantiated objects during the game.
    [SerializeField] public Transform InGameCreatedObjects;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        state = State.WaitingToStart;
    }

    private void Start()
    {
        // Event subscribing.
        EventManager.Instance.OnWin += (Transform myTransform, Transform otherTransform) => { otherTransform.gameObject.SetActive(false); };
        EventManager.Instance.OnDraw += (Transform myTransform, Transform otherTransform) => { otherTransform.gameObject.SetActive(false); };
        EventManager.Instance.OnLoss += (Transform myTransform, Transform otherTransform) =>
        {
            state = State.GameOver;
            EventManager.Instance.OnStateChanged?.Invoke();
        };
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnWin -= (Transform myTransform, Transform otherTransform) => { otherTransform.gameObject.SetActive(false); };
        EventManager.Instance.OnDraw -= (Transform myTransform, Transform otherTransform) => { otherTransform.gameObject.SetActive(false); };
        EventManager.Instance.OnLoss -= (Transform myTransform, Transform otherTransform) =>
        {
            state = State.GameOver;
            EventManager.Instance.OnStateChanged?.Invoke();
        };
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:


                waitingToStart -= Time.deltaTime;
                if (waitingToStart <= 0f)
                {
                    state = State.CountingDown;
                    EventManager.Instance.OnStateChanged?.Invoke();
                }
                break;

            case State.CountingDown:
                countDownTimer -= Time.deltaTime;
                if (countDownTimer <= 0f)
                {
                    state = State.Playing;
                    EventManager.Instance.OnStateChanged?.Invoke();
                }
                break;

            case State.Playing:

                break;

            case State.GameOver:

                break;
        }
    }


    public float GetCountDownTimer()
    {
        return countDownTimer;
    }


    public void PlayAfterDelay(Action action) => StartCoroutine(Method(action));

    private IEnumerator Method(Action _action)
    {
        yield return null;
        _action();
    }
}
