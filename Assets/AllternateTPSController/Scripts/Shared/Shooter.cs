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

    float nextFireAllowed;
    protected bool canFire;
    Transform muzzle;

    WeaponReloader reloader;

    private void Awake()
    {
        muzzle = transform.Find("Muzzle");
        reloader = GetComponent<WeaponReloader>();
        transform.SetParent(hand);
    }

    public void Reload()
    {
        if (reloader != null)
        {
            reloader.Reload();
            print("Reloading");
        }
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

        canFire = true;
    }
}
