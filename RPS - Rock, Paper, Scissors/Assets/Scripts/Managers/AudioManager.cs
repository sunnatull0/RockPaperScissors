using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [Header("SOURCES")]
    [SerializeField] private AudioSource backGroundSource;
    public AudioSource sfxSource;

    [Header("SFX CLIPS")]
    [SerializeField] private AudioClip shotClip;

    private Dictionary<string, AudioClip> Clips = new Dictionary<string, AudioClip>();

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
        Clips.Add("Shot", shotClip);
    }


    private void Start()
    {
        backGroundSource.Play();
    }

    public void PlaySFX(string name)
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
