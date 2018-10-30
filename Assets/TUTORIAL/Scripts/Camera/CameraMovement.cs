using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5;

    private CharacterController controller;

    private void Awake()
    {
        //Get a reference to the character controller.
        controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        //Get the vector left/right.
        var hor = Input.GetAxisRaw("Horizontal") * transform.right;

        //Get the vector forward/backward.
        var ver = Input.GetAxisRaw("Vertical") * transform.forward;

        //Get the vector up/down.
        var upDown = Input.GetAxisRaw("UpDown") * Vector3.up;

        //Get the movement direction and normalize it.
        var moveDir = Vector3.Normalize(hor + ver + upDown);

        //Move the controller.
        controller.Move(moveDir * speed * Time.fixedDeltaTime);
    }
}