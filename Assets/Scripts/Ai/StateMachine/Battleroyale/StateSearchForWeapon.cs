using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSearchForWeapon : BaseState
{
    [SerializeField] private BaseState _searchForUsefulItem;

    [HideInInspector] public bool HasWeapon { get; private set; }
    [HideInInspector] public GameObject Weapon { get => _weapon; }

    private GameObject _weapon;

    public override void EnterState() 
    {
        HasWeapon = HasWeaponFunc();

        if (!HasWeapon) return;
        StateMachine.ChangeState(_searchForUsefulItem);
    }

    public override void ExitState() { }

    public override void FrameUpdate() { }

    public override void PhysicsUpdate() 
    {
        HasWeapon = HasWeaponFunc();

        if (HasWeapon)
        {
            StateMachine.ChangeState(_searchForUsefulItem);
            return;
        }

        if (_weapon == null || _weapon.GetComponent<InventoryItem>().IsEquipped)
        {
            _weapon = SearchWeapon();
            return;
        }

        EquipWeapon();
        Movement();
    }

    protected bool HasWeaponFunc()
    {
        for (int i = 0; i < StateMachine.UnitHand.Inventory.AvailableSlots; i++)
        {
            GameObject item = StateMachine.UnitHand.Inventory.GetInventoryItem(i);
            if (item != null && item.GetComponent<InventoryItem>().ItemType == InventoryItemType.Gun)
            {
                return true;
            }
        }

        return false;
    }

    protected GameObject SearchWeapon()
    {
        List<GameObject> notEquippedWeapons = new List<GameObject>();
        for (int i = 0; i < Data.Items.Count; i++)
        {
            InventoryItem item = Data.Items[i].GetComponent<InventoryItem>();
            if (!item.IsEquipped && item.ItemType == InventoryItemType.Gun)
            {
                notEquippedWeapons.Add(Data.Items[i]);
            }
        }

        return GameObjectUtil.FindClosest(notEquippedWeapons.ToArray(), transform.position);
    }

    protected void Movement()
    {
        StateMachine.UnitMovement.MovementScript.Move(new Vector2(transform.forward.x, transform.forward.z));
        StateMachine.UnitMovement.MovementScript.LookAt(_weapon.transform.position);
    }

    protected void EquipWeapon()
    {
        float distance = Vector3.Distance(transform.position, _weapon.transform.position);
        if (distance > StateMachine.UnitHand.PickupDistance) return;
        StateMachine.UnitHand.Interaction();
    }
}
