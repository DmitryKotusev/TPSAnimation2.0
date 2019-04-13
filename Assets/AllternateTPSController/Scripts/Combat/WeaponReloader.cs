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
    [SerializeField]
    EWeaponType weaponType;
    public int clipSize;

    public int shotsFiredInClip;
    bool isReloading;
    System.Guid containerItemId;

    public event System.Action OnAmmoChanged;

    private void Awake()
    {
        containerItemId = inventory.Add(weaponType.ToString(), maxAmmo);
        Debug.Log("Weapon" + containerItemId);
    }

    public int roundsRemainingInClip
    {
        get
        {
            return clipSize - shotsFiredInClip;
        }
    }

    public int roundsRemainingInInventory
    {
        get
        {
            return inventory.GetAmountRemaining(containerItemId);
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
        HandleOnAmmoChanged();
    }

    public void TakeFormClip(int amount)
    {
        shotsFiredInClip += amount;
        HandleOnAmmoChanged();
    }

    public void HandleOnAmmoChanged()
    {
        OnAmmoChanged?.Invoke();
    }
}
