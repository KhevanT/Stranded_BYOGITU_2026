using System;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum Room_ID
    {
        StartRoom,
        NorthRoom,
        SouthRoom,
        EastRoom,
        WestRoom
    }

    public Room_ID roomID;
    public Vector3 cameraPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
