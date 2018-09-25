using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 0.2f;

    private CharacterController m_Controller;

    private void Awake()
    {
        //Get a reference to the character controller.
        m_Controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        //Get the vector left/right.
        var hor = Input.GetAxisRaw("Horizontal") * Vector3.right;

        //Get the vector forward/backward.
        var ver = Input.GetAxisRaw("Vertical") * Vector3.forward;

        //Get the vector up/down.
        var upDown = Input.GetAxisRaw("UpDown") * Vector3.up;

        //Get the movement direction and normalize it.
        var moveDir = Vector3.Normalize(hor + ver + upDown);

        //Move the controller.
        m_Controller.Move(moveDir * m_Speed * Time.fixedDeltaTime);
    }
}