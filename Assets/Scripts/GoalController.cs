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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Ball"))
        {
            return;
        }

        if(side == GoalSide.Left)
        {
            game.AddRightScore();
        }
        else
        {
            game.AddLeftScore();
        }
    }
}
