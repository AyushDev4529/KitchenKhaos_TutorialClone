using UnityEngine;

public class ContainerCounter : BaseCounter, IInteractable
{

    
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    

    public override void Interact(GameObject interactor)
    {
        // Try to get the Player component from the interactor GameObject
        Player player = interactor.GetComponent<Player>();
        if (player != null)
        {
            SpawnKitchenObject(player);
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
            // Set the local position to zero to align it with the counter top
            kitchenObjectInstance.GetComponent<KitchenObject>().SetKitchenObjectParent(player);

        }
    }   
}
