using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAudio : AudioBase
{
    [Header("Sounds:")]
    [SerializeField] private AudioClip _fireSound;
    [SerializeField] private AudioClip _reloadSound;
    [SerializeField] private AudioClip _equip;

    public void Fire()
    {
        PlaySound(_fireSound);
    }

    public void Reload()
    {
        PlaySound(_reloadSound);
    }

    public void Equip()
    {
        PlaySound(_equip);
    }
}
