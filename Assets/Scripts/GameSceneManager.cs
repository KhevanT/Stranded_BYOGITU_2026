using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void OnStartClick()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public static void OnGameOver()
    {
        SceneManager.LoadScene("TitleScreen");
    }
}
