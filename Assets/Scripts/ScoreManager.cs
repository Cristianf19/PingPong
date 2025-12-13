using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TMP_Text scoreText;
    private int scorePlayer1 = 0;
    private int scorePlayer2 = 0;

    private void Awake()
    {
        instance = this;
    }
    public void GoalScore(string GoalTag)
    {
        if (GoalTag == "GoalBottom")
        {
            ++scorePlayer1;
        }
        if (GoalTag == "GoalTop")
        {
            ++scorePlayer2;
        }

        scoreText.text = scorePlayer1 + ":" + scorePlayer2;

        GameManager.instance.ResetAfterGoal(GoalTag);
    }
}