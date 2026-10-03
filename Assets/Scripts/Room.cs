using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum Room_ID // starts from top left
    {
        Room_1_1,
        Room_1_2,
        Room_1_3,
        Room_1_4,
        Room_2_1,
        Room_2_2,
        Room_2_3,
        Room_2_4,
        Room_3_1,
        Room_3_2,
        Room_3_3, // safe
        Room_3_4,
        Room_4_1, // empty just sea
        Room_4_2,
        Room_4_3, // start room
        Room_4_4,
    }

    public Room_ID roomID;
    // public Vector3 cameraPos;

    // changed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
