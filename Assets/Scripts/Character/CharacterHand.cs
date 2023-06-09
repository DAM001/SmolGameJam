using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHand : MonoBehaviour
{

    public WeaponScript GetWeapon()
    {
        if (transform.childCount == 0) return null;
        return transform.GetChild(0).GetComponent<WeaponScript>();
    }

    public void OnFireDown()
    {
        if (GetWeapon() == null) return;

        GetWeapon().FireDown();
    }

    public void OnFireUp()
    {
        if (GetWeapon() == null) return;

        GetWeapon().FireUp();
    }
}
