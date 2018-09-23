using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_OpenRotation = new Vector3(0, 90, 0);

    [SerializeField]
    private float m_Speed = 1;

    private bool m_IsOpen;
    private bool m_InTransition;

    private Vector3 m_ClosedRotation;
    private Vector3 m_TargetRotation;

    private void Awake()
    {
        //Save the rotation that the door has when its closed.
        m_ClosedRotation = transform.localEulerAngles;
    }

    private void Update()
    {
        //Go back if this door is not in a transition state.
        if (m_InTransition == false)
            return;

        //Lerp to the rotation needed.
        transform.localEulerAngles = Vector3.Slerp(transform.localEulerAngles, m_TargetRotation, Time.deltaTime);

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

        //Assign the correct rotation.
        m_TargetRotation = (m_IsOpen) ? m_OpenRotation : m_ClosedRotation;
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