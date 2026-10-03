using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public bool menuOn = false;
    public GameObject menuCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleMenu(InputAction.CallbackContext context)
    {
        // Check for overlap with object first

        if (context.performed)
        {
            if(menuOn)
            {
                menuOn = false;
                menuCanvas.SetActive(false);
                PauseController.SetPaused(false);
            }
            else
            {
                menuOn = true;
                menuCanvas.SetActive(true);
                PauseController.SetPaused(true);
            }
            Debug.Log("Menu toggled to: " + menuOn);
        }
    }
}
