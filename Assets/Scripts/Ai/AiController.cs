using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AiController : MonoBehaviour
{
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private CharacterHand _characterHand;
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
        switch (_logicPhase)
        {
            case 0:
                _targetIsPlayer = false;
                _hasWeapon = false;
                if (_target == null) _target = FindNearestWeapon();
                break;
            case 1:
                _targetIsPlayer = false;
                _hasWeapon = true;
                _target = FindNearestAmmo();
                if (_target == null && _characterHand.IsWeapon()
                    && !_characterHand.GetWeapon().HasAmmo())
                {
                    _characterHand.OnThrow();
                    _logicPhase--;
                }
                if (_characterHand.HasAmmo()) _logicPhase++;
                break;
            case 2:
                Phase2();
                break;
            case 3:
                
                break;
        }

        Movement();
        HandleItems();
        HandleWeapon();
    }

    private void Phase2()
    {
        if (!_characterHand.HasAmmo()) _logicPhase--;
        GameObject ammo = FindNearestAmmo();
        GameObject player = FindNearestPlayer();
        float distanceAmmo = ammo == null ? Mathf.Infinity : Vector3.Distance(ammo.transform.position, transform.position);
        float distancePlayer = Vector3.Distance(player.transform.position, transform.position);
        if (distanceAmmo > distancePlayer && distancePlayer > _range / 2f)
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
        if (item != _target) return;

        _characterHand.OnEquip();
        _logicPhase++;
    }

    private void Movement()
    {
        if (_target == null) return;
        float distance = Vector3.Distance(transform.position, _target.transform.position);
        float speed = distance > _range ? 1f : distance / _range + .1f;
        if (!_targetIsPlayer && distance < _range / 10f) speed = .3f;
        else if (_hasWeapon && _targetIsPlayer && distance < 10f) speed = -.5f;
        _movement.Move(new Vector2(transform.forward.x * speed, transform.forward.z * speed));
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
        yield return new WaitForFixedUpdate();
        _characterHand.OnFireUp();
    }

    private GameObject FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] == gameObject)
            {
                players[i] = GameObject.FindGameObjectWithTag("Player");
                break;
            }
        }

        return GameObjectUtil.FindClosest(players, transform.position);
    }

    private GameObject FindNearestWeapon()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        List<GameObject> weapons = new List<GameObject>();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetComponent<InventoryItem>().ItemType == InventoryItemType.Weapon)
            {
                if (items[i].GetComponent<WeaponScript>().HasAmmo())
                {
                    weapons.Add(items[i]);
                }
            }
        }

        return GameObjectUtil.FindClosest(weapons.ToArray(), transform.position);
    }

    private GameObject FindNearestAmmo()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        List<GameObject> ammos = new List<GameObject>();
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].GetComponent<InventoryItem>().ItemType == InventoryItemType.Ammo)
            {
                if (_characterHand.IsWeapon() &&
                    items[i].GetComponent<AmmoItem>().WeaponType == _characterHand.GetWeapon().WeaponType)
                {
                    ammos.Add(items[i]);
                }
            }
        }

        return GameObjectUtil.FindClosest(ammos.ToArray(), transform.position);
    }
}
