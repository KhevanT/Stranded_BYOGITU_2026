using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
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
    public static event Action OnVisualSink;
    public static event Action OnMotorSink;
    public static event Action OnLanguageSink;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SinkVisual()
    {
        OnVisualSink?.Invoke();
    }

    void SinkMotor()
    {
        OnMotorSink?.Invoke();
    }

    void SinkLanguage()
    {
        OnLanguageSink?.Invoke();
    }
}
