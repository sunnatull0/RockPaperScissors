using UnityEngine;

public class MovingHand : MonoBehaviour
{
    private Rigidbody2D rb;
    private float minSpeed = 70f;
    private float maxSpeed = 100f;
    private float speed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        speed = GenerateRandomSpeed();
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

    private float GenerateRandomSpeed()
    {
        return Random.Range(minSpeed, maxSpeed);
    }

}
