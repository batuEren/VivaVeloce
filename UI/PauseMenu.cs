using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public InputActionReference moveInput;

    void Awake()
    {
        pauseMenu.SetActive(false);
        moveInput.action.started += IncomingPause;
    }

    private void Update()
    {
        //if (Keyboard.current.escapeKey.wasPressedThisFrame || Gamepad.current.startButton.wasPressedThisFrame) { IncomingPause(); }
    }

    public void IncomingPause(InputAction.CallbackContext a)
    {
        if (a.started)    //detect started action, like old keydown.
        {
            if (pauseMenu.active)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
