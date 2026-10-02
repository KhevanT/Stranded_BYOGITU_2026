using UnityEngine;

// Moves the camera based on trigger calls
public class CameraMovement : MonoBehaviour
{
    void Awake()
    {
        Door.OnPlayerEnterDoor += OnPlayerEnterDoor;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Moves camera to new room
    // TEMP: Maybe lerp or add transition
    void OnPlayerEnterDoor(Room.Room_ID from, Room.Room_ID to)
    {

    }
}
