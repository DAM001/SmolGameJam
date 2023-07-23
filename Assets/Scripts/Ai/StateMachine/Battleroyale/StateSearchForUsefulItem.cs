using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSearchForUsefulItem : BaseState
{
    [SerializeField] private StateSearchForWeapon _stateSearchForWeapon;

    [HideInInspector] public bool HasAmmo { get; private set; }
    [HideInInspector] public bool IsInventoryFull { get; private set; }

    public override void EnterState() 
    {
        HasAmmo = HasAmmoFunc();
        IsInventoryFull = IsInventoryFullFunc();
    }

    public override void ExitState() { }

    public override void FrameUpdate() { }

    public override void PhysicsUpdate()
    { 
        if (_stateSearchForWeapon.Weapon == null)
        {
            StateMachine.ChangeState(_stateSearchForWeapon);
        }

        if (!HasAmmo)
        {

        }
    }

    protected bool HasAmmoFunc()
    {
        InventoryItemType weaponType = _stateSearchForWeapon.Weapon.GetComponent<GunScript>().AmmoType;

        for (int i = 0; i < StateMachine.UnitHand.Inventory.AvailableSlots; i++)
        {
            GameObject item = StateMachine.UnitHand.Inventory.GetInventoryItem(i);
            InventoryItemType itemType = item.GetComponent<InventoryItem>().ItemType;
            if (item != null && weaponType == itemType)
            {
                return true;
            }
        }

        return false;
    }

    protected bool IsInventoryFullFunc()
    {
        int numberOfInventoryItems = 0;
        int numberOfAvailableSlots = StateMachine.UnitHand.Inventory.AvailableSlots;

        for (int i = 0; i < numberOfAvailableSlots; i++)
        {
            if (StateMachine.UnitHand.Inventory.GetInventoryItem(i) != null)
            {
                numberOfInventoryItems++;
            }
        }

        return numberOfInventoryItems == numberOfAvailableSlots;
    }
}
