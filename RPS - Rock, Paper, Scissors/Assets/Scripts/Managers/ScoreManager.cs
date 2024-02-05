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
        EventManager.Instance.OnWin += AddScore;
    }

    private void OnDisable()
    {
        // Event unsubscribing.
        EventManager.Instance.OnWin -= AddScore;
    }

    private void AddScore(Transform myTransform, Transform otherTransform)
    {
        Score++;
    }
}
