using UnityEngine;

public class MovingHand : MonoBehaviour
{
    private Rigidbody2D rb;
    private float defaultSpeed = 70f;
    private static float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        speed = defaultSpeed;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameActive)
            Move();
        else
            gameObject.SetActive(false);
    }

    private void Move()
    {
        rb.velocity = Vector2.down * speed * Time.deltaTime;
    }
}
