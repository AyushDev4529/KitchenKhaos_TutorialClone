using UnityEngine;

public class BaseCounter : MonoBehaviour, IInteractable , IKitchenObjectParent
{

    [SerializeField] private GameObject counterTop;
    private KitchenObject kitchenObject;
    public virtual void Interact(GameObject interactor)
    {
        Debug.LogError("BaseCounter Interacted!!");
    }

    public GameObject GetKitchenObjectAttachPoint()
    {
        return counterTop;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
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
