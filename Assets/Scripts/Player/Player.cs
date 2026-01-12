using System;
using UnityEngine;
using static UnityEngine.UI.Image;

public class Player : MonoBehaviour
{
    //TODO : to be moved to separate player Interaction Script with IInteractable interface
    //TODO : generalize so selectedVisual can be any type of interactable object

    [SerializeField] GameInput gameInput;
    [SerializeField] CharacterController characterController;
    [SerializeField] LayerMask interactableLayerMask;

    // Singleton instance
    public static Player Instance { get; private set; }

    private ClearCounter selectedCounter;
    public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChangedWithArgs;
    public class SelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

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
        HandleSelection();

    }

    //Handle Interaction Logic
    private void HandleSelection()
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
                if (hitInfo.transform.TryGetComponent(out ClearCounter clearCounter))
                {
                    SetSelectedCounter(clearCounter); 
                }
                else
                {
                    SetSelectedCounter(null);
                }
            }
        }
        else
        {
            Debug.Log("No interactable object in front of the player.");
            SetSelectedCounter(null);
        }

    }

    // Event handler for interaction action
    private void GameInput_OnInteractionAction(object sender, System.EventArgs e)
    {

        if (selectedCounter != null)
            selectedCounter.Interact(gameObject);


    }

    // Set the currently selected counter and invoke the event
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        // Only invoke event if selected counter has changed
        if (this.selectedCounter != selectedCounter)
        {
            this.selectedCounter = selectedCounter;
            OnSelectedCounterChangedWithArgs?.Invoke(this, new SelectedCounterChangedEventArgs { selectedCounter = selectedCounter }); 
        }

    }

}
