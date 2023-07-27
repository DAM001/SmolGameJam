using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicOrb : InventoryItem
{
    [SerializeField] protected SpawnerBase _spawner;
    [Space(10)]
    [SerializeField] protected GameObject _monster;
    [SerializeField] protected int _spawnQuantity = 4;
    //[SerializeField] protected float _cooldownTime = 10f;

    public override void Equip(UnitHand unitHand)
    {
        base.Equip(unitHand);
    }

    public override void Equip()
    {
        base.Equip();
    }

    public override void Throw()
    {
        base.Throw();
    }

    public override void UseDown()
    {
        base.UseDown();

        float distance = 3f;
        GameObject[] monsters = _spawner.CreateMultipleObjects(_spawnQuantity, _monster);
        for (int i = 0; i < monsters.Length; i++)
        {
            monsters[i].transform.position += new Vector3(Random.Range(-distance, distance), 0f, Random.Range(-distance, distance));
        }
    }

    public override void UseUp()
    {
        base.UseUp();
    }
}
