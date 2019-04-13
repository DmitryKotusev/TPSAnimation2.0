using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] float weaponSwitchTime;
    public Shooter[] weapons;
    public Shooter activeWeapon;
    Transform weaponHoster;

    int currentWeaponIndex;

    bool canFire;

    public event System.Action<Shooter> OnWeaponSwitch;

    private void Awake()
    {
        weapons = transform.Find("Weapons").GetComponentsInChildren<Shooter>();
        weaponHoster = transform.Find("Weapons");
        if (weapons.Length > 0)
        {
            currentWeaponIndex = 0;
            EquipWeapon(currentWeaponIndex);
        }
    }

    public void DeactivateWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(false);
            weapons[i].transform.SetParent(weaponHoster);
        }
    }

    public void SwitchWeapon(int direction)
    {
        canFire = false;
        currentWeaponIndex = (currentWeaponIndex + direction) % weapons.Length;
        if (currentWeaponIndex < 0)
        {
            currentWeaponIndex = weapons.Length + currentWeaponIndex;
        }
        GameManager.Instance.Timer.Add(() =>
        {
            EquipWeapon(currentWeaponIndex);
        },
        weaponSwitchTime);
    }

    void EquipWeapon(int index)
    {
        DeactivateWeapons();
        activeWeapon = weapons[index];
        canFire = true;
        activeWeapon.gameObject.SetActive(true);
        activeWeapon.Equip();
        OnWeaponSwitch?.Invoke(activeWeapon);
    }

    private void Update()
    {
        if (GameManager.Instance.InputController.mouseWheelUp)
        {
            SwitchWeapon(1);
        }
        if (GameManager.Instance.InputController.mouseWheelDown)
        {
            SwitchWeapon(-1);
        }

        if (!canFire)
        {
            return;
        }

        if (GameManager.Instance.InputController.fire1)
        {
            activeWeapon.Fire();
        }
    }
}
