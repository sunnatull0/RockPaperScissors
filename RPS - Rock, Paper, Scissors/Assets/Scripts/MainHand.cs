using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainHand : BaseHand
{
    
    private Transform myTransform;
    private Transform _otherTransform;
    private HandType myType;


    private void Awake()
    {
        // Getting the type of this hand.
        myType = GetHandType(gameObject);
    }

    private void Start()
    {
        myTransform = transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Assigning the transform that hit this object
        _otherTransform = collision.transform;
        
        // Getting the type of interracted hand.
        HandType otherType = GetHandType(collision.gameObject);

        // Interacting with other hand
        HandleInteraction(myType, otherType);
    }



    private void HandleInteraction(HandType myType, HandType InterractedType)
    {
        // Checking all possible cases.
        switch (myType, InterractedType)
        {
            // Rock check.
            case (HandType.Rock, HandType.Paper):
                Loss();
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
                Loss();
                break;
            case (HandType.Paper, HandType.Paper):
                Draw();
                break;

            // Scissors check.
            case (HandType.Scissors, HandType.Paper):
                Win();
                break;
            case (HandType.Scissors, HandType.Rock):
                Loss();
                break;
            case (HandType.Scissors, HandType.Scissors):
                Draw();
                break;
        }
    }
    
    private void Win()
    {
        // Invoking Win Event.
        EventManager.Instance.InvokeOnWinActions(myTransform, _otherTransform);
    }

    private void Draw()
    {
        // Invoking Draw Event.
        EventManager.Instance.InvokeOnDrawActoins(myTransform, _otherTransform);
    }

    private void Loss()
    {
        // Invoking Loss Event.
        EventManager.Instance.InvokeOnLossActoins(myTransform, _otherTransform);
    }

    // Function to get the HandType of some Gameobject.
    private HandType GetHandType(GameObject obj)
    {
        HandType handType = obj.GetComponent<BaseHand>().type;
        return handType;
    }

}
