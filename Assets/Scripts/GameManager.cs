using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ball;
    private Ball ballScript;
    public GameObject player1;
    public GameObject player2;
    private int playerResetAfterGoal=1;
    private Rigidbody2D ballRB;

    void Start()
    {
        ballRB = ball.GetComponent<Rigidbody2D>();
        ballScript = ball.GetComponent<Ball>();
        StartCoroutine(WaitForPlayerDirection());
    }

    private void Awake()
    {
        instance = this;
    }
    public void ResetAfterGoal(string GoalTag)
    {
        if (GoalTag == "GoalBottom")
        {
            ball.transform.position = new Vector2(0f, -4f);
            ballRB.linearVelocity = new Vector2(0f, 0f);
            playerResetAfterGoal = 1;

        }
        if (GoalTag == "GoalTop")
        {
            ball.transform.position = new Vector2(0f, 4f);
            ballRB.linearVelocity = new Vector2(0f, 0f);
            playerResetAfterGoal = 2;
        }
        
        player1.transform.position = new Vector2(0f, player1.transform.position.y);
        player2.transform.position = new Vector2(0f, player2.transform.position.y);

        StartCoroutine(WaitForPlayerDirection());
    }

    IEnumerator WaitForPlayerDirection()
    {
        while (true)
        {
            if (playerResetAfterGoal==1 && player1.transform.position.x != 0)
            {
                ballRB.linearVelocity = new Vector2(player1.transform.position.x*10, ballScript.speed);
                ballScript.inGame = true; 
                yield break;
            }
            if (playerResetAfterGoal==2 && player2.transform.position.x != 0)
            {
                ballRB.linearVelocity = new Vector2(player2.transform.position.x*10, -ballScript.speed);
                ballScript.inGame = true; 
                yield break;
            }
            yield return null;
        }
        
    }
}
