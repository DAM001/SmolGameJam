using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiController : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterHand _characterHand;
    [SerializeField] private CharacterHealth _characterHealth;
    [Space(10)]
    [SerializeField] private float _range = 30f;

    private GameObject _target;
    private int _logicPhase = 0;
    private bool _targetIsPlayer = false;
    private bool _hasWeapon = false;

    private void Start()
    {
        StartCoroutine(UpdateHandler());
    }

    private IEnumerator UpdateHandler()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        while (true)
        {
            UpdateLogic();
            yield return new WaitForSeconds(.3f);
        }
    }

    private void FixedUpdate()
    {
        if (_target == null) return;
        _movement.LookAt(_target.transform.position);
    }

    private void UpdateLogic()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) > 100f
            || !_characterHealth.IsInCircle)
        {
            return;
        }

        switch (_logicPhase)
        {
            case 0:
                _targetIsPlayer = false;
                _hasWeapon = false;
                _target = FindNearestWeapon();
                break;
            case 1:
                Phase1();
                break;
            case 2:
                Phase2();
                break;
            case 3:
                _logicPhase--;
                _target = FindNearestPlayer();
                break;
        }

        Movement();
        HandleItems();
        HandleWeapon();


    }

    private void Phase1()
    {
        if (_characterHand.IsWeapon() && !_characterHand.CurrentItem.GetComponent<WeaponScript>().HasAmmo())
        {
            _target = FindNearestAmmo();
            _targetIsPlayer = false;
            _hasWeapon = true;
            return;
        }

        if (_characterHand.HasAmmo()) _logicPhase++;

        GameObject ammo = FindNearestAmmo();
        GameObject player = FindNearestPlayer();
        float distanceAmmo = ammo == null ? Mathf.Infinity : Vector3.Distance(ammo.transform.position, transform.position);
        float distancePlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceAmmo > distancePlayer && distancePlayer < _range)
        {
            _target = player;
            _targetIsPlayer = true;
        }
        else
        {
            _target = ammo;
            _targetIsPlayer = false;
        }
    }

    private void Phase2()
    {
        if (!_characterHand.HasAmmo()) _logicPhase--;
        GameObject ammo = FindNearestAmmo();
        GameObject player = FindNearestPlayer();
        float distanceAmmo = ammo == null ? Mathf.Infinity : Vector3.Distance(ammo.transform.position, transform.position);
        float distancePlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceAmmo < distancePlayer && distancePlayer > _range / 2f)
        {
            _targetIsPlayer = false;
            _target = ammo;
        }
        else
        {
            _target = player;
            _targetIsPlayer = true;
        }
    }

    private void HandleItems()
    {
        GameObject item = _characterHand.EquipableItem();
        if (item == null) return;

        InventoryItemType type = item.GetComponent<InventoryItem>().ItemType;
        if (type == InventoryItemType.Health)
        {
            _characterHealth.UseHeal();
            Data.Items.Remove(item);
            Destroy(item);
            return;
        }
        if (type == InventoryItemType.Shield)
        {
            _characterHealth.UseShield();
            Data.Items.Remove(item);
            Destroy(item);
            return;
        }
        if (type == InventoryItemType.Grenade)
        {
            _characterHand.OnEquip();
            return;
        }
        if (type == InventoryItemType.BackpackUpgrade)
        {
            Data.Items.Remove(item);
            Destroy(item);
            return;
        }
        if (item != _target) return;

        _characterHand.OnEquip();
        _logicPhase++;
    }

    private void Movement()
    {
        if (_target == null) return;
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        float speed = 1f;
        if (_targetIsPlayer && distance < 15f) speed = -.5f;
        else if (!_targetIsPlayer && distance < .5f) speed = -.5f;
        _movement.Move(new Vector2(transform.forward.x * speed / .8f, transform.forward.z * speed));
    }

    private void HandleWeapon()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up * 1f + transform.forward * .5f, transform.forward, out hit, _range))
        {
            if (hit.transform.gameObject == _target)
            {
                StartCoroutine(ReleaseFireHandler());
            }
        }
    }

    private IEnumerator ReleaseFireHandler()
    {
        _characterHand.OnFireDown();
        yield return new WaitForSeconds(.1f);
        _characterHand.OnFireUp();
    }

    private GameObject FindNearestPlayer()
    {
        GameObject[] players = Data.Characters.ToArray();
        for (int i = 0; i < Data.Characters.Count; i++)
        {
            if (Data.Characters[i] == null)
            {
                Data.Characters.Remove(Data.Characters[i]);
            }
            else if (players[i] == gameObject) players[i] = null;
        }

        return GameObjectUtil.FindClosest(players, transform.position);
    }

    private GameObject FindNearestWeapon()
    {
        List<GameObject> weapons = new List<GameObject>();
        for (int i = 0; i < Data.Items.Count; i++)
        {
            if (Data.Items[i] == null)
            {
                Data.Items.Remove(Data.Items[i]);
            }
            else if (Data.Items[i].GetComponent<InventoryItem>().ItemType == InventoryItemType.Weapon)
            {
                if (Data.Items[i].GetComponent<WeaponScript>().HasAmmo() 
                    && Data.Items[i].GetComponent<WeaponScript>().Parent == null)
                {
                    weapons.Add(Data.Items[i]);
                }
            }
        }
        return GameObjectUtil.FindClosest(weapons.ToArray(), transform.position);
    }

    private GameObject FindNearestAmmo()
    {
        List<GameObject> ammos = new List<GameObject>();
        for (int i = 0; i < Data.Items.Count; i++)
        {
            if (Data.Items[i] == null)
            {
                Data.Items.Remove(Data.Items[i]);
            }
            else if (Data.Items[i].GetComponent<InventoryItem>().ItemType == InventoryItemType.Ammo)
            {
                if (_characterHand.IsWeapon()
                    && Data.Items[i].GetComponent<AmmoItem>().WeaponType == _characterHand.GetWeapon().WeaponType)
                {
                    ammos.Add(Data.Items[i]);
                }
            }
        }

        return GameObjectUtil.FindClosest(ammos.ToArray(), transform.position);
    }
}
