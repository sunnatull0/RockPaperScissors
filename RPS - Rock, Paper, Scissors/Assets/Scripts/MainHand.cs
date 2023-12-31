using UnityEngine;

public class MainHand : BaseHand
{

    #region Variables.
    private Transform myTransform;
    private Transform otherTransform;
    private HandType myType;
    #endregion

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
        otherTransform = collision.transform;
        
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
        EventManager.Instance.OnWin?.Invoke(myTransform, otherTransform);
    }

    private void Draw()
    {
        // Invoking Draw Event.
        EventManager.Instance.OnDraw?.Invoke(myTransform, otherTransform);
    }

    private void Loss()
    {
        // Invoking Loss Event.
        EventManager.Instance.OnLoss?.Invoke(myTransform, otherTransform);
    }

    // Function to get the HandType of some Gameobject.
    private HandType GetHandType(GameObject obj)
    {
        HandType handType = obj.GetComponent<BaseHand>().type;
        return handType;
    }

}
