using System;
using UnityEngine;

public class ClearCounter : BaseCounter, IInteractable
{
 
    public override void Interact(GameObject interactor)
    {
        //TODO: Clear Counter Logic
        Debug.Log("ClearCounter Interacted!!");
    }




}