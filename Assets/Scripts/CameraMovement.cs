using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;

// Moves the camera based on trigger calls
public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Dictionary<Room.Room_ID, CinemachineCamera> cameraIndexInstance = new Dictionary<Room.Room_ID, CinemachineCamera>(); // only unity 6.6
    public static Dictionary<Room.Room_ID, CinemachineCamera> CameraRoomIndex { get; private set; }

    public Room.Room_ID currRoom = Room.Room_ID.StartRoom;

    void Awake()
    {
        CameraRoomIndex = cameraIndexInstance;
        Door.OnPlayerEnterDoor += OnPlayerEnterDoor;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisableAllCameras();
        CameraRoomIndex[currRoom].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Moves camera to new room
    // TEMP: Maybe lerp or add transition
    void OnPlayerEnterDoor(Room.Room_ID from, Room.Room_ID to)
    {
        Debug.Log("Swapping cameras");
        CameraRoomIndex[from].gameObject.SetActive(false);
        CameraRoomIndex[to].gameObject.SetActive(true);
    }

    void DisableAllCameras()
    {
        foreach (CinemachineCamera cam in CameraRoomIndex.Values)
        {
            cam.gameObject.SetActive(false);
        }
    }

    void OnDestroy()
    {
        Door.OnPlayerEnterDoor -= OnPlayerEnterDoor;
    }
}
