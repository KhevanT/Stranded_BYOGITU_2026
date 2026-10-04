using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IslandSinkManager : MonoBehaviour
{
    public enum IslandState
    {
        FullIsland,
        PartialIsland,
        SunkIsland
    }

    public IslandState islandState;
    public bool isVisualUp = true;
    public bool isMotorUp = true;
    public bool isLanguageUp = true;
    public bool isWorldUp = true;
    public static event Action OnVisualSink;
    public static event Action OnMotorSink;
    public static event Action OnLanguageSink;
    public static event Action OnWorldSink;

    // Timers
    public float worldTimeTotal = 240f;
    public float worldTimeRemaining;
    private bool isWorldTimerRunning = false;

    public float visualTimeTotal = 60f;
    public float visualTimeRemaining;
    private bool isVisualTimerRunning = false;

    public float motorTimeTotal = 120f;
    public float motorTimeRemaining;
    private bool isMotorTimerRunning = false;

    public float languageTimeTotal = 180f;
    public float languageTimeRemaining;
    private bool isLanguageTimerRunning = false;

    // UI
    public TMP_Text timeLeftText;
    public Image visualImage, motorImage, languageImage; // fixed sequence visual > motor > language
    public GameObject gameOverPanel;

    void Awake()
    {
        isVisualUp = true;
        isMotorUp = true;
        isLanguageUp = true;
        isWorldUp = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);
        islandState = IslandState.FullIsland;

        worldTimeRemaining = worldTimeTotal;
        isWorldTimerRunning = true;

        visualTimeRemaining = visualTimeTotal;
        isVisualTimerRunning = true;

        motorTimeRemaining = motorTimeTotal;
        isMotorTimerRunning = true;

        languageTimeRemaining = languageTimeTotal;
        isLanguageTimerRunning = true;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateWorldTimer();

        UpdateVisualTimer();
        UpdateMotorTimer();
        UpdateLanguageTimer();
    }

    void UpdateWorldTimer()
    {
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

    void UpdateVisualTimer()
    {
        if (isVisualTimerRunning)
        {
            if (visualTimeRemaining > 0)
            {
                visualTimeRemaining -= Time.deltaTime;
            }
            else
            {
                visualTimeRemaining = 0;
                isVisualTimerRunning = false;

                SinkVisual();
            }
        }
    }

    void UpdateMotorTimer()
    {
        if (isMotorTimerRunning)
        {
            if (motorTimeRemaining > 0)
            {
                motorTimeRemaining -= Time.deltaTime;
            }
            else
            {
                motorTimeRemaining = 0;
                isMotorTimerRunning = false;

                SinkMotor();
            }
        }
    }

    void UpdateLanguageTimer()
    {
        if (isLanguageTimerRunning)
        {
            if (languageTimeRemaining > 0)
            {
                languageTimeRemaining -= Time.deltaTime;
            }
            else
            {
                languageTimeRemaining = 0;
                isLanguageTimerRunning = false;

                SinkLanguage();
            }
        }
    }

    void OnWorldTimerEnd()
    {
        isWorldUp = false;
        Debug.Log("Game over");
        islandState = IslandState.SunkIsland;
        OnWorldSink?.Invoke();
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        PauseController.SetPause(true);
        gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        GameSceneManager.OnGameOver();
    }

    void SinkVisual()
    {
        visualImage.enabled = false;
        islandState = IslandState.PartialIsland;
        OnVisualSink?.Invoke();
    }

    void SinkMotor()
    {
        OnMotorSink?.Invoke();
        motorImage.enabled = false;
    }

    void SinkLanguage()
    {
        OnLanguageSink?.Invoke();
        languageImage.enabled = false;
    }
}
