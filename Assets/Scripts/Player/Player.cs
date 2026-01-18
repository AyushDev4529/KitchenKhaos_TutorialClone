using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    //TODO : to be moved to separate player Interaction Script with IInteractable interface
    //TODO : generalize so selectedVisual can be any type of interactable object

    [SerializeField] private GameInput gameInput;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private LayerMask interactableLayerMask;
    [SerializeField] private GameObject kitchenObjectHoldPoint;
    private KitchenObject kitchenObject;

    // Singleton instance
    public static Player Instance { get; private set; }

    private BaseCounter baseCounter;
    public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChangedWithArgs;
    public class SelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
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
        // Using -transform.forward because PlayerVisual faces the opposite direction
        Vector3 raycastDirection = -transform.forward;
        float raycastDistance = 2f;

        // Perform raycast to detect interactable objects in front of the player
        if (Physics.Raycast(raycastOrigin, raycastDirection, out RaycastHit hitInfo, raycastDistance, interactableLayerMask))
        {
            if (!hitInfo.transform.TryGetComponent(out IInteractable interactable)) return;
            SetSelectedCounter(hitInfo.transform.TryGetComponent(out BaseCounter baseCounter) ? baseCounter : null);
        }
        else
        {
            SetSelectedCounter(null);
        }

    }

    // Event handler for interaction action
    private void GameInput_OnInteractionAction(object sender, EventArgs e)
    {
        if (baseCounter != null)
            baseCounter.Interact(gameObject);
    }

    // Set the currently selected counter and invoke the event
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        // Only invoke event if selected counter has changed
        if (this.baseCounter != selectedCounter)
        {
            this.baseCounter = selectedCounter;
            OnSelectedCounterChangedWithArgs?.Invoke(this, new SelectedCounterChangedEventArgs { selectedCounter = selectedCounter }); 
        }

    }


    public GameObject GetKitchenObjectAttachPoint()
    {
        return kitchenObjectHoldPoint;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
}
