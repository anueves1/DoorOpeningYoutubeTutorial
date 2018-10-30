using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField]
    private float m_Sensitivity = 0.5f;

    private float m_MouseX;
    private float m_MouseY;

    private InputManager m_Input;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        m_Input = GetComponentInParent<InputManager>();
    }

    private void LateUpdate()
    {
        m_MouseX += m_Input.MouseX * m_Sensitivity;
        m_MouseY -= m_Input.MouseY * m_Sensitivity;

        Quaternion cameraRotation = Quaternion.Euler(m_MouseY, 0f, 0f);
        Quaternion playerRotation = Quaternion.Euler(0f, m_MouseX, 0f);

        transform.localRotation = cameraRotation;
        transform.parent.localRotation = playerRotation;
    }
}