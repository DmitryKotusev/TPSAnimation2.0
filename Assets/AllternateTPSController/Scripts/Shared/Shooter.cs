using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    float rateOfFire;

    [SerializeField]
    Projectile projectile;

    [SerializeField]
    Transform hand;

    [SerializeField]
    AudioController audioReload;

    [SerializeField]
    AudioController audioFire;

    float nextFireAllowed;
    protected bool canFire;
    Transform muzzle;

    public WeaponReloader reloader;

    public void Equip()
    {
        transform.SetParent(hand);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    private void Awake()
    {
        muzzle = transform.Find("Model/Muzzle");
        reloader = GetComponent<WeaponReloader>();
    }

    public void Reload()
    {
        if (reloader != null)
        {
            reloader.Reload();
            print("Reloading");
        }
        audioReload.Play();
    }

    public virtual void Fire()
    {
        canFire = false;

        if (Time.time < nextFireAllowed)
        {
            return;
        }

        if (reloader != null)
        {
            if (reloader.IsReloading)
            {
                return;
            }
            if (reloader.shotsFiredInClip >= reloader.clipSize)
            {
                return;
            }
            reloader.TakeFormClip(1);
        }

        nextFireAllowed = Time.time + rateOfFire;

        // instantiate bullet
        Instantiate(projectile, muzzle.position, muzzle.rotation);

        audioFire.Play();
        canFire = true;
    }
}
