using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVisuals : MonoBehaviour
{
    [SerializeField] private GameObject _bulletShell;
    [SerializeField] private float _bulletShellDestroyTime = 10f;
    [Header("Smoke:")]
    [SerializeField] private ParticleSystem _smokeEffect;
    [SerializeField] private float _smokeIntensityModifier = .03f;
    [SerializeField] private float _smokeDecrease = 1f;

    private GameObject _muzzleFlash;
    private float _smokeIntensity = 0f;

    public GameObject MuzzleFlash
    { 
        get => _muzzleFlash;
        set
        {
            _muzzleFlash = value;
            _muzzleFlash.SetActive(false);
            _muzzleFlash.transform.parent = null;
        }
    }

    public ParticleSystem SmokeEffect
    {
        get => _smokeEffect;
        set
        {
            _smokeEffect = value;
            var emission = _smokeEffect.emission;
            emission.rateOverTime = 0f;
        }
    }

    private void Start()
    {
        SmokeEffect = _smokeEffect;
    }

    private void FixedUpdate()
    {
        UpdateSmoke();
    }

    public void CreateBulletShell(GameObject firePoint)
    {
        if (_bulletShell == null) return;
        GameObject shell = Instantiate(_bulletShell, firePoint.transform);
        shell.transform.parent = null;
        shell.transform.position = firePoint.transform.position + transform.forward * -.6f + transform.right * .3f;
        shell.transform.Rotate(Random.Range(2f, 15f), Random.Range(-15f, 15f), 0f);
        shell.GetComponent<Rigidbody>().AddForce(shell.transform.right * Random.Range(200f, 500f));
        Destroy(shell, _bulletShellDestroyTime);
    }

    public void ShowMuzzleFlash(GameObject firePoint)
    {
        StartCoroutine(MuzzleFlashHandler(_muzzleFlash, firePoint));
    }

    private IEnumerator MuzzleFlashHandler(GameObject muzzleFlash, GameObject firePoint)
    {
        muzzleFlash.transform.position = firePoint.transform.position;
        muzzleFlash.transform.rotation = firePoint.transform.rotation;

        muzzleFlash.SetActive(true);
        muzzleFlash.transform.Rotate(0f, 0f, Random.Range(-45f, 45f));
        float scale = Random.Range(.5f, 1f);
        muzzleFlash.transform.localScale = new Vector3(scale, scale, .1f);
        yield return new WaitForSeconds(Random.Range(.03f, .08f));
        muzzleFlash.gameObject.SetActive(false);
    }

    private void UpdateSmoke()
    {
        var emission = _smokeEffect.emission;
        emission.rateOverTime = _smokeIntensity;

        if (_smokeIntensity <= 0f)
        {
            _smokeIntensity = 0f;
            return;
        }

        _smokeIntensity -= Time.fixedDeltaTime * (1f / _smokeDecrease);
    }

    public void SmokeOnFire(float damage)
    {
        _smokeIntensity += damage * _smokeIntensityModifier;
    }
}
