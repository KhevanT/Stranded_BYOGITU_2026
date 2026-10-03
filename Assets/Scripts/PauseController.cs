using UnityEngine;

public class PauseController : MonoBehaviour
{
    public static bool IsPaused { get; private set; } = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetPaused(bool pause)
    {
        IsPaused = pause;
    }
}
