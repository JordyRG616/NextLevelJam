using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputMap : MonoBehaviour
{
    [SerializeField] private ScriptableSignal OnRotatePressed;
    [SerializeField] private ScriptableSignal OnConfirmPressed;

    private PlayerControls controls;


    void Start()
    {
        controls = new PlayerControls();

        controls.Default.Enable();
        controls.Default.Rotate.performed += Rotate_performed;
        controls.Default.Confirm.performed += Confirm_performed;
    }

    private void Confirm_performed(InputAction.CallbackContext obj)
    {
        OnConfirmPressed.Fire();
    }

    private void Rotate_performed(InputAction.CallbackContext obj)
    {
        OnRotatePressed.Fire();
    }
}
