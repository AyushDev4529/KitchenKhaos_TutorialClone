using UnityEngine;

public class ClearCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform counterTopTransform;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public void Interact(GameObject interactor)
    {
       Transform kitchenObject = Instantiate(kitchenObjectSO.prefab, counterTopTransform);
        kitchenObject.localPosition = Vector3.zero;

        Debug.Log(kitchenObject.GetComponent<KitchenObject>().GetKitchenObjectSO().objectName);
        Debug.Log("Interacted with ClearCounter by " + interactor.name);
    }
}
