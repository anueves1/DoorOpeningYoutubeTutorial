using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 1;

    [SerializeField]
    private bool m_AutoOpen = true;

    [SerializeField]
    private Transform m_Pivot;

    [SerializeField]
    private Vector3 m_OpenRotation = new Vector3(0, 90, 0);

    [SerializeField]
    private Vector3 m_OpenOffset;

    private bool m_IsOpen;
    private bool m_InTransition;

    private Vector3 m_ClosedRotation;
    private Vector3 m_TargetRotation;

    private Vector3 m_ClosedPosition;
    private Vector3 m_TargetPosition;

    private void Awake()
    {
        //Save the rotation that the door has when its closed.
        m_ClosedRotation = m_Pivot.localEulerAngles;

        //Save the position that the door has when its closed.
        m_ClosedPosition = m_Pivot.localPosition;

        //If we have no pivot, use this object as one.
        if (m_Pivot == null)
            m_Pivot = transform;
    }

    private void Update()
    {
        //Go back if this door is not in a transition state.
        if (m_InTransition == false)
            return;

        //Time used for the lerp.
        var t = Time.deltaTime * m_Speed;

        //Lerp to the rotation needed.
        m_Pivot.localEulerAngles = Vector3.Slerp(m_Pivot.localEulerAngles, m_TargetRotation, t);

        //Lerp the position needed.
        m_Pivot.localPosition = Vector3.Lerp(m_Pivot.localPosition, m_TargetPosition, t);

        //Check how close to finishing the rotation lerp we are.
        var rotDistance = Vector3.Distance(m_Pivot.localEulerAngles, m_TargetRotation);

        //Check how close to finishing the position lerp we are.
        var posDistance = Vector3.Distance(m_Pivot.localPosition, m_TargetPosition);

        //If we're done, stop transitioning.
        if (rotDistance < 0.01f && posDistance < 0.01f)
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

    private void OnTriggerEnter(Collider other)
    {
        //If the door doesn't need to auto open, go back.
        if (m_AutoOpen == false)
            return;

        //Check if the object that just entered the trigger is a door opener.
        var opener = other.GetComponent<DoorOpener>();

        //If it is, open the door.
        if (opener)
            Open();
    }

    private void OnTriggerExit(Collider other)
    {
        //If the door doesn't need to auto open, go back.
        if (m_AutoOpen == false)
            return;

        //Check if the object that just exited the trigger is a door opener.
        var opener = other.GetComponent<DoorOpener>();

        //If it is, close the door.
        if (opener)
            Close();
    }
}