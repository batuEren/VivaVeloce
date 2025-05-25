using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerRumble : MonoBehaviour
{
    public static Gamepad gamepad;

    void Start()
    {
        // Get the current connected gamepad
        gamepad = Gamepad.current;
    }

    public void Rumble(float duration){
        if (gamepad != null)
        {
            // Set motor speeds to make the controller rumble
            gamepad.SetMotorSpeeds(0.45f, 0.6f);
            Debug.Log("Rumble");
            // Stop the rumble after a certain duration
            Invoke(nameof(StopRumble), duration);
        }
    }

    public void Rumble(float lowFrequency, float highFrequency, float duration)
    {
        if (gamepad != null)
        {
            // Set motor speeds to make the controller rumble
            gamepad.SetMotorSpeeds(lowFrequency, highFrequency);
            // Stop the rumble after a certain duration
            Invoke(nameof(StopRumble), duration);
        }
    }

    void StopRumble()
    {
        // Stop the controller rumble
        gamepad.SetMotorSpeeds(0, 0);
    }
}