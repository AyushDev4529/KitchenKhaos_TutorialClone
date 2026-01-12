using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    // Other properties and methods related to KitchenObject can be added here

    // Method to get the KitchenObjectSO
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
