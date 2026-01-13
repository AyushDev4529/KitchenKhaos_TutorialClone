using System;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject counterTop;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private KitchenObject kitchenObject;
    public void Interact(GameObject interactor)
    {
        if (kitchenObject == null)
        {
            SpawnKitchenObject();
        }
        else
        {
            Debug.Log("Counter already has an object.");
        }
    }

    private void SpawnKitchenObject()
    {
        GameObject kitchenObjectInstance = Instantiate(kitchenObjectSO.prefab, counterTop.transform);

        // Get the KitchenObject component from the instantiated prefab and set its position
        kitchenObject = kitchenObjectInstance.GetComponent<KitchenObject>();

        // Ensure the kitchen object is positioned correctly on the counter
        if (kitchenObject != null)
            kitchenObject.transform.localPosition = Vector3.zero;
        else
            Debug.LogError("KitchenObject component not found on the instantiated prefab.");

    }
}
