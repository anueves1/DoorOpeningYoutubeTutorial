using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    [SerializeField]
    private Door[] m_Doors;

    private void Update()
    {
        //If we press the 'E' key, toggle the doors' state.
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Toggle all doors in the array.
            foreach (var door in m_Doors)
                door.Toogle();
        }
    }
}