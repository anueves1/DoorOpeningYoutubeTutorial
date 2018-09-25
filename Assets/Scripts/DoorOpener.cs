using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private KeyCode m_ToggleKey = KeyCode.F;

    [SerializeField]
    private Door[] m_Doors;

    private void Update()
    {
        //If we press the 'E' key, toggle the doors' state.
        if (Input.GetKeyDown(m_ToggleKey))
        {
            //Toggle all doors in the array.
            foreach (var door in m_Doors)
                door.Toogle();
        }
    }
}