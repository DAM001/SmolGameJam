using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuroRudi : InventoryItem
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _eatTime = 1.5f;

    private bool _isEated = false;

    protected override void Start()
    {
        base.Start();

        _animator.speed = 0f;
    }

    public override void Deactivate()
    {
        base.Deactivate();

        if (!_isEated) return;
        Destroy(gameObject);
    }

    public override void UseDown()
    {
        _animator.speed = 1f;
    }

    public override void UseUp()
    {
        ResetAnim();
    }

    private void ResetAnim()
    {
        _animator.Rebind();
        _animator.Update(0f);
        _animator.speed = 0f;
    }

    public void Destroy()
    {
        EnableRigidbody(true);
        IsEquipped = false;
        Data.Items.Remove(gameObject);
        _isEated = true;
    }
}
