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
    public void Equip(Equipment newItem)
    {
        // find out what slot fits in
        int slotIndex = (int) newItem.equipmentSlot;
        Equipment oldItem = null;
        if(currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
        }

        if(onEquipmentChange != null)
        {
            onEquipmentChange.Invoke(newItem, oldItem);
        }
        currentEquipment[slotIndex] = newItem;

    }


    public void Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {

            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChange != null)
            {
                onEquipmentChange.Invoke(null, oldItem);
            }
        }
    }


    public void UnequipAll()
    {

        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
