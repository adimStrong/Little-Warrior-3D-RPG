using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment",menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;
    public int armorModifier;
    public int damageModifier;
    public GameObject equipedItem; // here we will use game object to parent it to the player when we equiping the item


    public override void Use()
    {
        base.Use();

        // equip the item
        EquipmentManager.instance.Equip(this);
        // remove it from inventory
        RemoveFromInventory();
    }
}


public enum EquipmentSlot
{
     Head,Chest,Legs,Weapon,Shield,Feet
}