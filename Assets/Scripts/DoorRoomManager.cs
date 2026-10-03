using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Room;
using static UnityEngine.Rendering.DebugUI;

public class DoorRoomManager : MonoBehaviour
{
    // Room storage
    public Room.Room_ID currRoom = Room.Room_ID.Room_4_3;
    [SerializeField]
    private Dictionary<Room.Room_ID, Room> roomIndexInstance = new Dictionary<Room.Room_ID, Room>(); // only unity 6.6
    public static Dictionary<Room.Room_ID, Room> RoomIndex { get; private set; }

    // Room Door storage
    [SerializeField]
    private Dictionary<Room.Room_ID, List<Door>> roomDoorInstance = new Dictionary<Room.Room_ID, List<Door>>(); // only unity 6.6
    public static Dictionary<Room.Room_ID, List<Door>> RoomDoorIndex { get; private set; }

    // Door collider storage
    public float doorCooldown = 3f; // in seconds
    /*
    [SerializeField]
    private Dictionary<Door, BoxCollider2D> doorColliderInstance = new Dictionary<Door, BoxCollider2D>(); // only unity 6.6
    public static Dictionary<Door, BoxCollider2D> DoorColliderIndex { get; private set; }
    */

    void Awake()
    {
        RoomIndex = roomIndexInstance;
        RoomDoorIndex = roomDoorInstance;
        // DoorColliderIndex = doorColliderInstance;
        Door.OnPlayerEnterDoor += UpdateRoom;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currRoom = Room.Room_ID.Room_4_3;
        InitializeDoors();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeDoors()
    {
        // Disable all doors
        foreach (var roomDoors in RoomDoorIndex.Values)
        {
            foreach (Door door in roomDoors)
            {
                door.gameObject.SetActive(false);
            }
        }

        // Enable doors for the current room
        if (RoomDoorIndex.TryGetValue(currRoom, out List<Door> currentDoors))
        {
            foreach (Door door in currentDoors)
            {
                door.gameObject.SetActive(true);
            }
        }
    }

    // Updates room by disabling all current doors and re enabling new ones (after a short delay)
    void UpdateRoom(Room.Room_ID from, Room.Room_ID to)
    {
        currRoom = to;
        StartCoroutine(UpdateDoors(from, to));
    }

    // Handles door delay
    IEnumerator UpdateDoors(Room.Room_ID from, Room.Room_ID to)
    {
        if (RoomDoorIndex.TryGetValue(from, out List<Door> doorsFrom))
        {
            foreach (Door door in doorsFrom)
            {
                door.gameObject.SetActive(false);
            }
        }

        // Wait 2 seconds
        yield return new WaitForSeconds(doorCooldown);

        if (RoomDoorIndex.TryGetValue(to, out List<Door> doorsTo))
        {
            foreach (Door door in doorsTo)
            {
                door.gameObject.SetActive(true);
            }
        }
    }

    void OnDestroy()
    {
        Door.OnPlayerEnterDoor -= UpdateRoom;
    }

}
