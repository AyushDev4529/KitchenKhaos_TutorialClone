using System;
using UnityEngine;

//TODO : New Input Actions for Interaction
public class GameInput : MonoBehaviour
{
    PlayerInputActions inputActions;
    public EventHandler OnInteractionAction;

    private void Awake()
    {        
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
        inputActions.Player.Interaction.performed += Interaction_performed;
    }

    private void Interaction_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractionAction?.Invoke(this, EventArgs.Empty);
    }

    // Get the movement vector from player input
    public Vector2 GetMovementVector()
    {
        Vector2 input = inputActions.Player.Move.ReadValue<Vector2>(); // Read movement input from the Player action map
        return input;
    }
}
