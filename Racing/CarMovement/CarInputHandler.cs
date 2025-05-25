using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{

    CarController carController;

    public bool canMove = true;

    public bool alternativeMovementScheme = false;

    public InputActionReference moveInput;

    private void Awake()
    {
        carController = GetComponent<CarController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector = moveInput.action.ReadValue<Vector2>();


        if (canMove)
        {
            carController.SetInputVector(inputVector);
        }
        else
        {
            carController.SetInputVector(Vector2.zero);
        }
    }

    public void MakeCanMove()
    {
        canMove = true;
    }

    public void MakeCanNotMove()
    {
        canMove = false;
    }
}
