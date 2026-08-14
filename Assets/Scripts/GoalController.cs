using UnityEngine;

public class GoalController : MonoBehaviour
{
    public enum GoalSide
    {
        Left,
        Right
    }

    [SerializeField]
    private GoalSide side;

    [SerializeField]
    private GameController game;

    [SerializeField]
    private BallController ball;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Ball"))
        {
            return;
        }

        if(side == GoalSide.Left)
        {
            // 오른쪽 플레이어 득점
            game.AddRightScore();

            // 다음 공은 오른쪽 플레이어한테(이긴 사람)
            ball.ResetAndServe(Vector2.right);
        }
        else
        {
            // 왼쪽 플레이어 득점
            game.AddLeftScore();

            // 다음 공은 왼쪽 플레이어한테(이긴 사람)
            ball.ResetAndServe(Vector2.left);
        }
    }
}
