using UnityEngine;

public class GameController : MonoBehaviour
{
    // 왼쪽 플레이어의 점수
    private int leftScore;

    // 오른쪽 플레이어의 점수
    private int rightScore;

    private void Start()
    {
        leftScore = 0;
        rightScore = 0;

        Debug.Log("Pong Game Start!");
    }

    public void AddLeftScore()
    {
        leftScore++;

        Debug.Log($"Score: {leftScore} : {rightScore}");
    }

    public void AddRightScore()
    {
        rightScore++;

        Debug.Log($"Score: {leftScore} : {rightScore}");
    }
}
