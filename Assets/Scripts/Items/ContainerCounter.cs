using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public event EventHandler OnPlayerGrabbedObject;

    public override void Interact(GameObject interactor)
    {
        Player player = TryGetPlayer(interactor);

        if (player != null)
        {
            if (!player.HasKitchenObject())
            {
                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                SpawnKitchenObject(player);
            }
        }
        else
        {
            Debug.LogWarning("Interactor does not have a Player component.");
        }
    }

    private void SpawnKitchenObject(Player player)
    {

        if (!HasKitchenObject())
        {
            GameObject kitchenObjectInstance = Instantiate(kitchenObjectSO.prefab);
            // Set the local position to zero to align it with the counter-top
            kitchenObjectInstance.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

        }
    }
}
