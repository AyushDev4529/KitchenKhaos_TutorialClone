using UnityEngine;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    //TODO : to be moved to separate player Interaction Script with IInteractable interface

    [SerializeField] GameInput gameInput;
    [SerializeField] CharacterController characterController;
    [SerializeField] LayerMask interactableLayerMask;

    private void Start()
    {
        gameInput.OnInteractionAction += GameInput_OnInteractionAction;
    }

    private void OnDestroy()
    {
        gameInput.OnInteractionAction -= GameInput_OnInteractionAction;
    } 


    //Handle Interaction Logic
    private void HandleInteraction()
    {

        float chestHeight = characterController.height * 0.5f;
        float capsuleRadius = characterController.radius;
        Vector3 raycastOrigin = transform.position + Vector3.up * chestHeight + transform.forward * capsuleRadius;
        // Using -transform.forward because PlayerVisual faces opposite direction
        Vector3 raycastDirection = -transform.forward;
        float raycastDistance = 2f;

        // Perform raycast to detect interactable objects in front of the player
        if (Physics.Raycast(raycastOrigin, raycastDirection, out RaycastHit hitInfo, raycastDistance, interactableLayerMask))
        {
            Debug.Log("Hit: " + hitInfo.collider.name);
            if (hitInfo.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(gameObject);
            }
        }
        else
        {
            Debug.Log("No interactable object in front of the player.");
        }

    }

    // Event handler for interaction action
    private void GameInput_OnInteractionAction(object sender, System.EventArgs e)
    {
        HandleInteraction();
    }

}
