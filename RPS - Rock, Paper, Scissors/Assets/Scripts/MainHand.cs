using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : BaseHand
{

    private HandType myType;

    private void Awake()
    {
        myType = GetHandType(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandType otherType = GetHandType(collision.gameObject);
        HandleInteraction(myType, otherType);
    }

    private void HandleInteraction(HandType myType, HandType InterractedType)
    {
        switch (myType, InterractedType)
        {
            // Rock check.
            case (HandType.Rock, HandType.Paper):
                Lose();
                break;
            case (HandType.Rock, HandType.Scissors):
                Win();
                break;
            case (HandType.Rock, HandType.Rock):
                Draw();
                break;

            // Paper check.
            case (HandType.Paper, HandType.Rock):
                Win();
                break;
            case (HandType.Paper, HandType.Scissors):
                Lose();
                break;
            case (HandType.Paper, HandType.Paper):
                Draw();
                break;

            // Scissors check.
            case (HandType.Scissors, HandType.Paper):
                Win();
                break;
            case (HandType.Scissors, HandType.Rock):
                Lose();
                break;
            case (HandType.Scissors, HandType.Scissors):
                Draw();
                break;
        }
    }
    
    
    private void Win()
    {
        Debug.Log("Win!");
    }

    private void Draw()
    {
        Debug.Log("Draw!");
    }

    private void Lose()
    {
        Debug.Log("Lost!");
    }

    private HandType GetHandType(GameObject obj)
    {
        HandType handType = obj.GetComponent<BaseHand>().type;
        return handType;
    }

}
