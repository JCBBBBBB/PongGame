using UnityEngine;
using UnityEngine.WSA;

public class BallController : MonoBehaviour
{
    // 게임 시작시 공이 가지는 최초 속도
    [SerializeField]
    private float initialSpeed = 6f;

    // Paddle에 한 번 부딪힐 때마다 증가할 속도
    [SerializeField]
    private float speedIncrease = 0.5f;

    // 공의 최대 속도 제한
    [SerializeField]
    private float maxSpeed = 12f;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // 게임 시작과 동시에 공을 오른쪽으로 발사한다
        Launch(Vector2.right);
    }

    // 공 발사
    private void Launch(Vector2 direction)
    {
        // direction을 단위벡터로 만들고 처음 속도 곱한다
        body.linearVelocity = direction.normalized * initialSpeed;
    }

    // 충돌 처리
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 오브젝트가 Paddle인지 확인
        if(!collision.gameObject.CompareTag("Paddle"))
        {
            return;
        }

        // Paddle이랑 충돌했다면
        // Paddle에 맞은 위치를 기준으로 공을 반사시킨다
        ReflectFromPaddle(collision);
    }

    private void ReflectFromPaddle(Collision2D collision)
    {
        // 실제 충돌 지점 정보
        ContactPoint2D contact = collision.GetContact(0);

        // Paddle 자체가 어디에 있는지를 나타내는 실제 Y 좌표(Paddle 중심)
        float paddleY = collision.transform.position.y;

        // 충돌한 Paddle Collider의 전체 높이
        float paddleHeight = collision.collider.bounds.size.y;

        // 공이 Paddle의 세로 어디를 맞았는지를 -1 ~ +1로 바꾸는 것
        float relativeY = (contact.point.y - paddleY) / (paddleHeight * 0.5f);

        // 범위 제한
        relativeY = Mathf.Clamp(relativeY, -1f, 1f);

        // Paddle의 가장 위/아래에 맞았을때
        // 최대 60도까지 꺾이도록 한다
        float maxAngle = 60f;

        // -1 ~ +1의 충돌 위치를
        // -60 ~ +60도의 각도로 변환한다.
        float angle = relativeY * maxAngle;

        // Ball이 Paddle보다 왼쪽에 있으면 -1f 방향
        // Ball이 Paddle보다 오른쪽에 있으면 1f 방향
        float directionX = transform.position.x < collision.transform.position.x ? -1f : 1f;

        // directionX = 1 -> 오른쪽
        // directionX = -1 -> 왼쪽
        Vector2 direction = Quaternion.Euler(0f, 0f, angle) * new Vector2(directionX, 0f);

        // 현재 공의 속도
        float currentSpeed = body.linearVelocity.magnitude;

        // Paddle에 맞을 때마다 공을 조금 빠르게 한다.
        // maxSpeed 이상으로는 올라가지 않는다.
        float newSpeed = Mathf.Min(currentSpeed + speedIncrease, maxSpeed);

        // 최종적으로 계산한 방향과 속도를
        // Rigidbody2D에 적용한다
        body.linearVelocity = direction.normalized * newSpeed;
    }
}
