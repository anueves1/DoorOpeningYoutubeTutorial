using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private Door door;

    private void Update()
    {
        //If we press the e key, toggle the door's state.
        if (Input.GetKeyDown(KeyCode.F))
            door.ToggleState();
    }
}