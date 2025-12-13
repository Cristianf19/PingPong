using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;
    public bool inGame;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        inGame = false;
    }

    private void Move(float x, float y)
    {
        if (inGame)
        {
            rb.linearVelocity = new Vector2(x, y);
        }

        if (!inGame)
        {
            rb.linearVelocity = new Vector2(0f, -speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("sechoco");
            float playerPositionX = collision.transform.position.x;
            float ballPositionX = transform.position.x;
            float playerWidth = collision.bounds.size.x;

            float impact = (ballPositionX - playerPositionX) / (playerWidth /2f);

            impact *= 6;
            
            Move(impact, -rb.linearVelocity.y);
        }

        if (collision.CompareTag("GoalTop") || collision.CompareTag("GoalBottom"))
        {
            ScoreManager.instance.GoalScore(collision.tag);
        }

    }

    

    
}