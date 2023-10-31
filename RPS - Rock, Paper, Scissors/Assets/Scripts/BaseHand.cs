using UnityEngine;

public class BaseHand : MonoBehaviour
{
    public enum HandType { Rock, Paper, Scissors };
    public HandType type;

    public SpriteRenderer handSprite;

}
