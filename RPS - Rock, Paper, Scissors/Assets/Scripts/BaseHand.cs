using UnityEngine;

public class BaseHand : MonoBehaviour
{
    
    // Hand type.
    public enum HandType { Rock, Paper, Scissors };
    [HideInInspector] public HandType type;

    // Hand Sprite.
    [SerializeField] protected SpriteRenderer handSprite;

}
