using UnityEngine;
using UnityEngine.InputSystem;

// 흐름
// -> 외부에서 입력 전달
// -> moveInput
// -> FixedUpdate()
// -> MovePosition()
// -> 패들 이동



public class PaddleMotor : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 8f; // 이동 속도

    [SerializeField]
    private float minY = -6f; // 패들 움직일 수 있는 y 범위

    [SerializeField]
    private float maxY = 3.3f;

    private Rigidbody2D body;
    private BoxCollider2D boxCollider;

    private float moveInput; // 패들이 어느 방향으로 움직여야 하는지 저장

    public float CenterY => body.position.y; // 현재 패들의 Y좌표를 외부에서 읽을 수 있게 만든 프로퍼티

    public float HalfHeight { get; private set; } // 패들 높이의 절반

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();

        HalfHeight = boxCollider.bounds.extents.y;
    }

    // 외부에서 이동 명령을 받는 함수
    public void SetMoveInput(float input)
    {
        moveInput = Mathf.Clamp(input, -1f, 1f); // clamp 사용 이유 : 입력값을 -1 ~ 1 사이로 제한
    }

    
    private void FixedUpdate()
    {
        Vector2 nextPosition = body.position;

        nextPosition.y +=
            moveInput * // 방향
            moveSpeed * // 속도
            Time.fixedDeltaTime; // 시간

        // 패들이 화면 밖으로 못 나가게 하는 부분
        nextPosition.y = Mathf.Clamp(
            nextPosition.y,
            minY + HalfHeight,
            maxY - HalfHeight);

        // 계산한 위치로 Rigidbody2D 이동
        body.MovePosition(nextPosition);
    }
}
