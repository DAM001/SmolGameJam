using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponVisuals : MonoBehaviour
{
    [SerializeField] private GameObject _bulletShell;
    //[SerializeField] private AudioSource _audioSource;

    public void CreateBulletShell(GameObject firePoint)
    {
        GameObject shell = Instantiate(_bulletShell, firePoint.transform);
        shell.transform.parent = null;
        shell.transform.position = firePoint.transform.position + transform.forward * -.6f + transform.right * .3f;
        shell.transform.Rotate(Random.Range(2f, 15f), Random.Range(-15f, 15f), 0f);
        shell.GetComponent<Rigidbody>().AddForce(shell.transform.right * Random.Range(200f, 500f));
        Destroy(shell, 5f);
    }

    public void MuzzleFlash(GameObject muzzleFlash)
    {
        StartCoroutine(MuzzleFlashHandler(muzzleFlash));
    }

    private IEnumerator MuzzleFlashHandler(GameObject muzzleFlash)
    {
        muzzleFlash.SetActive(true);
        muzzleFlash.transform.Rotate(0f, 0f, Random.Range(-45f, 45f));
        float scale = Random.Range(.5f, 1f);
        muzzleFlash.transform.localScale = new Vector3(scale, scale, .1f);
        yield return new WaitForSeconds(Random.Range(.03f, .08f));
        muzzleFlash.gameObject.SetActive(false);
    }
}
