using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSearchForWeapon : BaseState
{
    [SerializeField] private BaseState _searchForUsefulItem;

    [HideInInspector] public bool HasWeapon { get; private set; }

    public override void EnterState() 
    {
        HasWeapon = false;

        for (int i = 0; i < StateMachine.UnitHand.Inventory.AvailableSlots; i++)
        {
            GameObject item = StateMachine.UnitHand.Inventory.GetItem(i);
            if (item != null && item.GetComponent<InventoryItem>().ItemType == InventoryItemType.Gun)
            {
                HasWeapon = true;
                StateMachine.ChangeState(_searchForUsefulItem);
                break;
            }
        }
    }

    public override void ExitState()
    {

    }

    public override void FrameUpdate()
    { 

    }

    public override void PhysicsUpdate() 
    {

    }
}
