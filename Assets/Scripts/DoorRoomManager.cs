using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DoorRoomManager : MonoBehaviour
{
    [SerializeField]
    private Dictionary<Room.Room_ID, Room> roomIndex = new Dictionary<Room.Room_ID, Room>(); // only unity 6.6

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
