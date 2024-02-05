using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;


    private Dictionary<string, AudioClip> Clips = new Dictionary<string, AudioClip>();

    [Header("SOURCES")]
    [SerializeField] private AudioSource backGroundSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("SFX CLIPS")]
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip timer;
    [SerializeField] private AudioClip start;
    [SerializeField] private AudioClip win;
    [SerializeField] private AudioClip draw;
    [SerializeField] private AudioClip loss;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Assigning clips.
        Clips.Add("Click", buttonClick);
        Clips.Add("Timer", timer);
        Clips.Add("Start", start);
        Clips.Add("Win", win);
        Clips.Add("Draw", draw);
        Clips.Add("Loss", loss);
    }


    private void Start()
    {
        backGroundSource.Play();
        AddSoundToAllButtons();

        // Events.
        EventManager.Instance.OnWin += PlayWinSound;
        EventManager.Instance.OnDraw += PlayDrawSound;
        EventManager.Instance.OnLoss += PlayLossSound;
        EventManager.Instance.OnStateChanged += OnStateChanged;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Events.
        EventManager.Instance.OnWin -= PlayWinSound;
        EventManager.Instance.OnDraw -= PlayDrawSound;
        EventManager.Instance.OnLoss -= PlayLossSound;
        EventManager.Instance.OnStateChanged -= OnStateChanged;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void PlayWinSound(Transform myTransform, Transform otherTransform)
    {
        PlaySFX("Win");
    }

    private void PlayDrawSound(Transform myTransform, Transform otherTransform)
    {
        PlaySFX("Draw");
    }

    private void PlayLossSound(Transform myTransform, Transform otherTransform)
    {
        PlaySFX("Loss");
    }


    private void OnStateChanged()
    {
        if (GameManager.Instance.IsCountingDown)
        {
            PlaySFX("Timer");
        }
        else if (GameManager.Instance.IsGameActive)
        {
            PlaySFX("Start");
        }
    }


    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        AddSoundToAllButtons();
    }

    private void AddSoundToAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => { PlaySFX("Click"); });
        }
    }
    

    private void PlaySFX(string name)
    {
        if (Clips.ContainsKey(name))
        {
            sfxSource.PlayOneShot(Clips[name]);
        }
        else
        {
            Debug.LogError("There is no such named clip!");
        }
    }

}
