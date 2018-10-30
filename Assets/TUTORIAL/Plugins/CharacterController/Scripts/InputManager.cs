using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool IsRunning;
    public bool Jump;

    public float Horizontal;
    public float Vertical;

    public float MouseX;
    public float MouseY;

    private void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");

        Jump = Input.GetKeyDown(KeyCode.Space);
        IsRunning = Input.GetKey(KeyCode.LeftShift);

        MouseX = Input.GetAxisRaw("Mouse X");
        MouseY = Input.GetAxisRaw("Mouse Y");
    }
}