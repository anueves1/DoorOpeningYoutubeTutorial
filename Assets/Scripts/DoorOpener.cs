using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private Door m_Door;

    private void Update()
    {
        //If we press the 'E' key, toggle the doors' state.
        if (Input.GetKeyDown(KeyCode.E))
            m_Door.Toogle();
    }
}