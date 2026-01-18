
>Clear Counter is only responsible for putting the kitchen object down(holding kitchen object) like putting different material to make the burger, and when player interact again it is responsible for handing him the kitchen object.

To implement this logic, in `ClearCounter` we just need assign the held `KitchenObject` to counter by using `GetKitchenObject()` and `SetKitchenObject(this)` method, which we implemented in our previous lessons.
But before assigning it check if counter already has kitchen object using `HasKitchenObject()` method, if has and player is not holding the kitchen object then request the `KitchenObject` to change its parent using `SetKitchenObjectParent(this)`. but pass `player` as a parameter instead of `this`.
ex.
```
Player player = TryGetPlayer(interactor);  
if (!HasKitchenObject())  
{  
    if (player.HasKitchenObject())  
          player.GetKitchenObject().SetKitchenObjectParent(this);     
else  
{  
    if (!player.HasKitchenObject())  
           GetKitchenObject().SetKitchenObjectParent(player); 

```

Now after separating logic and assigning logic we have created a system where we can now implement assigning the kitchen object simpler.
This is the goal of refactoring our code trying to make the underlying logic simpler and separating the responsibility.