using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class InputMap : MonoBehaviour
{
    [SerializeField] private ScriptableSignal OnRotatePressed;
    [SerializeField] private ScriptableSignal OnConfirmPressed;
    [SerializeField] private ScriptableSignal OnMenuPressed;

    private PlayerControls controls;
    private bool menuOpen;


    void Start()
    {
        controls = new PlayerControls();

        controls.Default.Enable();
        controls.Default.Rotate.performed += Rotate_performed;
        controls.Default.Confirm.performed += Confirm_performed;
        controls.Default.Menu.performed += Menu_performed;
    }

    private void Menu_performed(InputAction.CallbackContext obj)
    {
        menuOpen = !menuOpen;
        OnMenuPressed.Fire(menuOpen);
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
