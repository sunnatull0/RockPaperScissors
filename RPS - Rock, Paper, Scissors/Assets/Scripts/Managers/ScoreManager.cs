using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static int Score;

    private void Awake()
    {
        Score = 0;
    }

    private void Start()
    {   
        // Event subscribing.
        EventManager.Instance.OnWin += (Transform myTransform, Transform otherTransform) => { Score++; };
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnWin -= (Transform myTransform, Transform otherTransform) => { Score++; };
    }
}
