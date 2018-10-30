using UnityEngine;

public class Door1 : MonoBehaviour
{
    [SerializeField]
    private Transform pivot;

    [SerializeField]
    private Vector3 openRotation = new Vector3(0, 90, 0);

    [SerializeField]
    private Vector3 openPosition;

    [SerializeField]
    private float speed = 3;

    private bool isOpen;

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

        //Use the closed rotation as a target.
        targetRotation = closedRotation;
        //Use the closed position as a target.
        targetPosition = closedPosition;
    }

    private void Update()
    {
        //Calculate the lerp's progress.
        var lerpTime = Time.deltaTime * this.speed;

        //Lerp to the required rotation.
        pivot.localEulerAngles = Vector3.Slerp(pivot.localEulerAngles, targetRotation, lerpTime);

        //Lerp to the required position.
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, targetPosition, lerpTime);
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
    }
}