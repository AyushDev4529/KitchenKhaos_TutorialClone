using UnityEngine;

public class ClearCounter : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor)
    {
        Debug.Log("Interacted with ClearCounter by " + interactor.name);
    }
}
