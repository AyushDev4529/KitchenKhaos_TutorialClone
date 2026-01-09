using UnityEngine;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    //TODO : to be moved to separate player Interaction Script with IInteractable interface
    //TODO : to be moved to separate player Movement Script
    //TODO : Using new Input System for Interaction 

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] GameInput gameInput;
    [SerializeField] CharacterController characterController;
    [SerializeField] LayerMask interactableLayerMask;


    Vector3 movementVector;
    bool isWalking = false;
    float verticalVelocity;
    Vector3 lastInteractDirection;

    private void Start()
    {
        gameInput.OnInteractionAction += GameInput_OnInteractionAction;
    }

    private void OnDestroy()
    {
        gameInput.OnInteractionAction -= GameInput_OnInteractionAction;
    }


    private void Update()
    {
       
        HandleMovement();
        
    }

    // Handle Movement Logic 
    private void HandleMovement()
    {
        float moveDistance = Time.deltaTime * moveSpeed;
        Vector3 playerPos = transform.position;
        Vector3 movementDirection = MovementDirection();

        // Calculate final movement vector including gravity
        movementVector = movementDirection * moveDistance + new Vector3(0, HandleGravity() * Time.deltaTime, 0);

        PlayerMovement();

        // Check if the player is walking
        Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);
        float horizontalVelocityMagnitude = horizontalVelocity.magnitude;

        // Small threshold to avoid floating point precision issues
        float smallThreshold = 0.01f;
        isWalking = horizontalVelocityMagnitude > smallThreshold;

        if (horizontalVelocityMagnitude > smallThreshold)
            PlayerRotation();
    }

    // Move the player based on input and return the new position
    private Vector3 PlayerMovement()
    {
        characterController.Move(movementVector);// Apply horizontal movement
        return transform.position;
    }

    // Rotate the player to face the movement direction
    private void PlayerRotation()
    {
        Vector3 rotationDeltaVector = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);
        transform.forward = Vector3.Slerp(transform.forward, -rotationDeltaVector.normalized, Time.deltaTime * rotationSpeed);
    }

    // Get the movement direction based on input
    private Vector3 MovementDirection()
    {
        Vector2 movementInput = new Vector2();
        movementInput = gameInput.GetMovementVector().normalized; // Normalize to prevent faster diagonal movement
        Vector3 movementDirection = new Vector3(movementInput.x, 0, movementInput.y);
        return movementDirection;
    }

    private float HandleGravity()
    {
        //Industry practice usually sets it to a small negative value instead of zero.
        if (characterController.isGrounded)
            verticalVelocity = 0;
        else
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        return verticalVelocity;
    }

    // Check if the player is currently walking
    public bool IsWalking()
    {
        return isWalking;
    }


    //Handle Interaction Logic
    private void HandleInteraction()
    {

        float chestHeight = characterController.height * 0.5f;
        float capsuleRadius = characterController.radius;
        Vector3 raycastOrigin = transform.position + Vector3.up * chestHeight + transform.forward * capsuleRadius;
        // Using -transform.forward because PlayerVisual faces opposite direction
        Vector3 raycstDirection = -transform.forward;
        float raycastDistance = 2f;

        // Perform raycast to detect interactable objects in front of the player
        if (Physics.Raycast(raycastOrigin, raycstDirection, out RaycastHit hitInfo, raycastDistance, interactableLayerMask))
        {
            Debug.Log("Hit: " + hitInfo.collider.name);
            if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter)) 
                    clearCounter.Interact();
         }
        else
        {
            Debug.Log("No interactable object in front of the player.");
        }

    }

    private void GameInput_OnInteractionAction(object sender, System.EventArgs e)
    {
        HandleInteraction();
    }

}
