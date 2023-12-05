using UnityEngine;

public class MovingHand : MonoBehaviour
{

    #region Variables
    private Rigidbody2D rb;
    private float speed = 100f;
    #endregion


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = speed * Time.fixedDeltaTime * Vector2.down;
    }

}
