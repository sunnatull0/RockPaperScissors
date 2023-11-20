using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        EventManager.Instance.OnWin += (Transform myTr, Transform otherTr) => { UIanimator.SetTrigger(Win); };
        EventManager.Instance.OnDraw += (Transform myTr, Transform otherTr) => { UIanimator.SetTrigger(Draw); };
        EventManager.Instance.OnStateChanged += OnStateChaged;
    }

    private void OnDisable()
    {
        EventManager.Instance.OnWin -= (Transform myTr, Transform otherTr) => { UIanimator.SetTrigger(Win); };
        EventManager.Instance.OnDraw -= (Transform myTr, Transform otherTr) => { UIanimator.SetTrigger(Draw); };
        EventManager.Instance.OnStateChanged -= OnStateChaged;
    }


    [Header("UI")]
    [SerializeField] private Animator UIanimator;
    private const string GreenPlay = "GreenPlay";
    private const string Win = "Win";
    private const string Draw = "Draw";

    private void OnStateChaged()
    {
        if (GameManager.Instance.IsGameActive)
        {
            StartCoroutine(PlayWithOneFrameDelay());
        }
    }

    private void PlayGreenAnimation()
    {
        UIanimator.SetTrigger(GreenPlay);
    }

    private IEnumerator PlayWithOneFrameDelay()
    {
        yield return null;

        PlayGreenAnimation();
    }

}
