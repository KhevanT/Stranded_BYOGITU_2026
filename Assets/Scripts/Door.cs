using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Room.Room_ID from;
    public Room.Room_ID to;

    public BoxCollider2D box2D;
    public static event Action<Room.Room_ID, Room.Room_ID> OnPlayerEnterDoor; // from, to

    void Awake()
    {
        box2D = GetComponent<BoxCollider2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            OnPlayerEnterDoor?.Invoke(from, to);
            Debug.Log("Player entered door");
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player exited door");
        }
    }
}
