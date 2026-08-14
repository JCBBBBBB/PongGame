using UnityEngine;
using TMPro;


public class GameController : MonoBehaviour
{
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

    private void Start()
    {
        leftScore = 0;
        rightScore = 0;

        UpdateScoreUI();
    }

    public void AddLeftScore()
    {
        leftScore++;
        UpdateScoreUI();
    }

    public void AddRightScore()
    {
        rightScore++;
        UpdateScoreUI();

    }

    // 현재 점수를 UI에 반영
    private void UpdateScoreUI()
    {
        leftScoreText.text = leftScore.ToString();
        rightScoreText.text = rightScore.ToString();
    }
}
