using System;
using UnityEngine;

public class ItemPickUp : Interactables
{
    public Item item;
    public override void Interact()
    {
        base.Interact();
        PickUp();
    }

    private void PickUp()
    {

        Debug.Log("Pick up item" + item.name);
        bool wasPickup = Inventory.instance.Add(item);
        if (wasPickup)
        {
            Destroy(gameObject);
        }
    }
}
