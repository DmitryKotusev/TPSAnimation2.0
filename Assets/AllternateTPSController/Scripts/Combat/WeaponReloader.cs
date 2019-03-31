using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    [SerializeField]
    int maxAmmo;
    [SerializeField]
    float reloadTime;
    [SerializeField]
    Container inventory;
    public int clipSize;

    public int shotsFiredInClip;
    bool isReloading;
    System.Guid containerItemId;

    private void Awake()
    {
        containerItemId = inventory.Add(this.name, maxAmmo);
    }

    public int roundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    public bool IsReloading
    {
        get
        {
            return isReloading;
        }
    }

    public void Reload()
    {
        if (isReloading)
        {
            return;
        }

        isReloading = true;

        GameManager.Instance.Timer.Add(() =>
        {
            ExecuteReload(inventory.TakeFromContainer(containerItemId, shotsFiredInClip));
        }, reloadTime);
    }

    private void ExecuteReload(int amount)
    {
        isReloading = false;
        shotsFiredInClip -= amount;
    }

    public void TakeFormClip(int amount)
    {
        shotsFiredInClip += amount;
    }
}
