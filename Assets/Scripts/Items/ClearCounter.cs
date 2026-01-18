using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    public override void Interact(GameObject interactor)
    {
        // Try to get the Player component from the interactor GameObject
        Player player = TryGetPlayer(interactor);
        if (!HasKitchenObject())
        {
            
            if (player.HasKitchenObject())
            {
               player.GetKitchenObject().SetKitchenObjectParent(this);
               player.SetKitchenObject(null);
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                SetKitchenObject(null);
            }
        }
    }




}