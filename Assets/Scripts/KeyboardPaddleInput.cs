using UnityEngine;
using UnityEngine.InputSystem;
// »ç¿ëÀÚ°¡ ¹» ´­·¶Áö?

public class KeyboardPaddleInput : MonoBehaviour
{
    [SerializeField]
    private PaddleMotor motor;

    [SerializeField]
    private KeyCode upKey = KeyCode.W;

    [SerializeField]
    private KeyCode downKey = KeyCode.S;

    private void Awake()
    {
        if(motor == null)
        {
            motor = GetComponent<PaddleMotor>();
        }
    }

    private void Update()
    {
        float input = 0f;

        if(Input.GetKey(upKey))
        {
            input += 1f;
        }

        if(Input.GetKey(downKey))
        {
            input -= 1f;
        }

        motor.SetMoveInput(input);
    }
}
