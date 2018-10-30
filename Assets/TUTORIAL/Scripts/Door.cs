using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private Vector3 openRotation = new Vector3(0, 90, 0);

    [SerializeField]
    private bool lerpPosition = false;

    [SerializeField]
    private Vector3 openPosition;

    [SerializeField]
    private float duration = 3;

    private bool isOpen;

    private float lerpTime;
    private bool isTransitioningState;

    private Vector3 closedRotation;
    private Vector3 targetRotation;

    private Vector3 closedPosition;
    private Vector3 targetPosition;

    private void Awake()
    {
        //Save the start rotation as the closed rotation.
        closedRotation = pivot.localEulerAngles;
        //Save the start position as the closed position.
        closedPosition = pivot.localPosition;
    }

    private void Update()
    {
        //Go back if we're not transitioning to any state.
        if (isTransitioningState == false)
            return;

        //Increase the lerp time.
        lerpTime += Time.deltaTime;

        //Calculate the lerp's progress.
        var a = lerpTime / duration;

        //If we finished the lerp.
        if(a >= 1)
        {
            //Stop running the update.
            isTransitioningState = false;
        }

        //Lerp to the required rotation.
        var rot = Quaternion.Slerp(pivot.localRotation, Quaternion.Euler(targetRotation), a);
        pivot.localRotation = rot;

        //If we need to lerp the position.
        if (lerpPosition)
            //Lerp to the required position.
            pivot.localPosition = Vector3.Lerp(pivot.localPosition, targetPosition, a);
    }

    public void ToggleState()
    {
        //Toggle the is open value.
        isOpen = !isOpen;

        //If the door is now open.
        if (isOpen)
        {
            //Set the open position as the target.
            targetPosition = openPosition;
            //Set the open rotation as the target.
            targetRotation = openRotation;
        }
        else
        {
            //Set the closed position as the target.
            targetPosition = closedPosition;
            //Set the closed rotation as the target.
            targetRotation = closedRotation;
        }

        //We start lerping.
        isTransitioningState = true;

        //Reset the lerp time.
        lerpTime = 0;
    }

    /*
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
     */
}