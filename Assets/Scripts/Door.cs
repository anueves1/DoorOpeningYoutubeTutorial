using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_OpenRotation = new Vector3(0, 90, 0);

    [SerializeField]
    private Vector3 m_OpenOffset;

    [SerializeField]
    private float m_Speed = 1;

    private bool m_IsOpen;
    private bool m_InTransition;

    private Vector3 m_ClosedRotation;
    private Vector3 m_TargetRotation;

    private Vector3 m_ClosedPosition;
    private Vector3 m_TargetPosition;

    private void Awake()
    {
        //Save the rotation that the door has when its closed.
        m_ClosedRotation = transform.localEulerAngles;

        //Save the position that the door has when its closed.
        m_ClosedPosition = transform.localPosition;
    }

    private void Update()
    {
        //Go back if this door is not in a transition state.
        if (m_InTransition == false)
            return;

        //Time used for the lerp.
        var t = Time.deltaTime * m_Speed;

        //Lerp to the rotation needed.
        transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, m_TargetRotation, t);

        //Lerp the position needed.
        transform.localPosition = Vector3.Lerp(transform.localPosition, m_TargetPosition, t);

        //Check how close to finishing the lerp we are.
        var distance = Vector3.Distance(transform.localEulerAngles, m_TargetRotation);

        //If we're done, stop transitioning.
        if (distance < 0.01f)
            m_InTransition = false;
    }

    private void StartTransition()
    {
        //Start the transition.
        m_InTransition = true;

        //Use the correct rotation as a target.
        m_TargetRotation = m_IsOpen ? m_OpenRotation : m_ClosedRotation;

        //Use the correct position as a target.
        m_TargetPosition = m_IsOpen ? (m_ClosedPosition + m_OpenOffset) : m_ClosedPosition;
    }

    public void Toogle()
    {
        //Toggle the is open bool.
        m_IsOpen = !m_IsOpen;

        StartTransition();
    }

    public void Open()
    {
        //We want to open the door.
        m_IsOpen = true;

        StartTransition();
    }

    public void Close()
    {
        //We want to close the door.
        m_IsOpen = false;

        StartTransition();
    }
}