using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton



    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than one instance of Equipment");
        }
        instance = this;
    }
    #endregion
    public Equipment[] defaultWear;
    Equipment[] currentEquipment;  //Items we are currently equiped

    //callback for when an item is equiped
    public delegate void OnEquipmentChange(Equipment newItem, Equipment oldItem);
    public OnEquipmentChange onEquipmentChange;
    Inventory inventory;
    private void Start()
    {
        inventory = Inventory.instance; // get reference to our inventory

        // initialize currentEquipment based on number of equipnment slot
       int numberOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numberOfSlots];
    }
    // equip new item
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
    public Equipment GetEquipment(EquipmentSlot slot)
    {
        return currentEquipment[(int)slot];
    }
    // Equip a new item
    public void Equip(Equipment newItem)
    {
        Equipment oldItem = null;
        // Find out what slot the item fits in
        // and put it there.
        int slotIndex = (int)newItem.equipmentSlot;
        // If there was already an item in the slot
        // make sure to put it back in the inventory
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }
        // An item has been equipped so we trigger the callback
        if (onEquipmentChange != null)
            onEquipmentChange.Invoke(newItem, oldItem);
        currentEquipment[slotIndex] = newItem;
        Debug.Log(newItem.name + " equipped!");
       
        //equippedItems [itemIndex] = newMesh.gameObject;
    }
    void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
          
            // Equipment has been removed so we trigger the callback
            if (onEquipmentChange != null)
                onEquipmentChange.Invoke(null, oldItem);
        }
    }
    void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

    }

}
