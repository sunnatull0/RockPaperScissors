using UnityEngine;

public class BaseHand : MonoBehaviour
{
    public enum HandType { Rock, Paper, Scissors };
    [HideInInspector] public HandType type;

    [SerializeField] protected SpriteRenderer handSprite;

}
