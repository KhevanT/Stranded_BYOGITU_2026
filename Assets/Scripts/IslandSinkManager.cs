using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IslandSinkManager : MonoBehaviour
{
    [SerializeField] private float worldTimeRemaining = 180f; // Starting time in seconds
    private bool isWorldTimerRunning = false;

    // UI
    public TMP_Text timeLeftText;
    public Image visualImage, motorImage, languageImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Starts the countdown automatically when the game begins
        isWorldTimerRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        // World timer
        if (isWorldTimerRunning)
        {
            if (worldTimeRemaining > 0)
            {
                // Subtract the time passed since the last frame
                worldTimeRemaining -= Time.deltaTime;
                timeLeftText.SetText("Time left: " + (int)worldTimeRemaining);
            }
            else
            {
                // Triggered the exact frame the timer hits 0
                worldTimeRemaining = 0;
                isWorldTimerRunning = false;

                OnWorldTimerEnd();
            }
        }
    }

    void OnWorldTimerEnd()
    {
        Debug.Log("Game over");
    }

    void SinkVisual()
    {
        
    }
}
