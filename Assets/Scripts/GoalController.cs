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
            bool gameOver = game.AddRightScore();

            // 게임이 끝났으면 공 다시 발사하지 않는다.
            if (gameOver)
            {
                return;
            }

            // 다음 공은 오른쪽 플레이어한테(이긴 사람)
            ball.ResetAndServe(Vector2.right);
        }
        else
        {
            // 왼쪽 플레이어 득점
            bool gameOver = game.AddLeftScore();

            if(gameOver)
            {
                return;
            }

            // 다음 공은 왼쪽 플레이어한테(이긴 사람)
            ball.ResetAndServe(Vector2.left);
        }
    }
}
