using UnityEngine;

public class MovingHand : MonoBehaviour
{

    #region Variables
    private Rigidbody2D rb;
    private float speed = 350f;
    #endregion


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameActive)
            Move();
        
    }


    private void Move()
    {
        rb.velocity = Vector2.down * speed * Time.deltaTime;
    }

}
