using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour
{
    // 현재 게임의 상태
    public enum GameState
    {
        Ready,
        Playing,
        GameOver
    }

    // 현재 상태
    private GameState gameState;

    // 승리 조건
    [SerializeField]
    private int scoreToWin = 5;

    [SerializeField]
    private BallController ball;


    // 왼쪽 플레이어의 점수
    private int leftScore;

    // 오른쪽 플레이어의 점수
    private int rightScore;

    // 화면에 표시할 왼쪽 점수
    [SerializeField]
    private TMP_Text leftScoreText;

    // 화면에 표시할 오른쪽 점수
    [SerializeField]
    private TMP_Text rightScoreText;

    [SerializeField]
    private TMP_Text messageText;

    private void Start()
    {
        leftScore = 0;
        rightScore = 0;

        gameState = GameState.Ready;

        UpdateScoreUI();

        messageText.text = "READY";

        Invoke(nameof(StartGame), 1f);
    }

    private void Update()
    {
        if (gameState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

    }

    private void StartGame()
    {
        gameState = GameState.Playing;

        messageText.text = "";

        // 게임 시작 시 공을 발사한다.
        ball.ResetAndServe(Vector2.right);
    }

    public bool AddLeftScore()
    {
        if(gameState != GameState.Playing)
        {
            return false;
        }

        leftScore++;
        UpdateScoreUI();

        // 승리 조건 확인
        if(leftScore >= scoreToWin)
        {
            EndGame();
            return true;
        }

        return false;
    }

    public bool AddRightScore()
    {
        if(gameState != GameState.Playing)
        {
            return false;
        }

        rightScore++;
        UpdateScoreUI();

        // 승리 조건 확인
        if(rightScore >= scoreToWin)
        {
            EndGame();
            return true; // 게임 끝남
        }

        return false; // 게임 아직 계속된다
    }

    // 현재 점수를 UI에 반영
    private void UpdateScoreUI()
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }

    private void EndGame()
    {
        gameState = GameState.GameOver;

        if(leftScore >= scoreToWin)
        {
            messageText.text = "LEFT PLAYER WINS";
        }
        else
        {
            messageText.text = "RIGHT PLAYER WINS";
        }
    }

    private void Restart()
    {
        leftScore = 0;
        rightScore = 0;

        UpdateScoreUI();

        gameState = GameState.Ready;

        messageText.text = "READY";

        ball.Stop();

        Invoke(nameof(StartGame), 1f);
    }
}
