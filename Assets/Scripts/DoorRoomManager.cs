using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DoorRoomManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<Room.Room_ID, Room> roomIndex = new Dictionary<Room.Room_ID, Room>(); // only unity 6.6

    public Room.Room_ID currRoom = Room.Room_ID.StartRoom;

    void Awake()
    {
        Door.OnPlayerEnterDoor += UpdateRoom;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currRoom = Room.Room_ID.StartRoom;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateRoom(Room.Room_ID from, Room.Room_ID to)
    {
        currRoom = to;
    }
}
