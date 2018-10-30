using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;

    [SerializeField]
    private float m_RunMultiplier = 2;

    [SerializeField]
    private float m_Gravity = 5;

    [SerializeField]
    private float m_JumpForce = 6;

    private Vector3 m_MoveDir;
    private CollisionFlags m_MoveFlags;

    private InputManager m_Input;
    private CharacterController m_Controller;

    private void Awake()
    {
        m_Input = GetComponent<InputManager>();
        m_Controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        //If we're grounded.
        if(m_MoveFlags == CollisionFlags.Below)
        {
            var rightVector = m_Input.Horizontal * transform.right;
            var forwardVector = m_Input.Vertical * transform.forward;

            //Movement.
            m_MoveDir = rightVector + forwardVector;
            m_MoveDir *= GetSpeed();

            if (m_Input.Jump)
                m_MoveDir.y = m_JumpForce;
        }

        //Gravity.
        m_MoveDir.y -= m_Gravity * Time.fixedDeltaTime;

        //Move the controller.
        m_MoveFlags = m_Controller.Move(m_MoveDir * Time.fixedDeltaTime);
    }

    private float GetSpeed()
    {
        var speed = m_Speed;
        speed *= m_Input.IsRunning ? m_RunMultiplier : 1f;

        return speed;
    }
}