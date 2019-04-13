using System;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp: PickUpItem
{
    [SerializeField]
    EWeaponType weaponType;
    [SerializeField]
    float respawnTime;
    [SerializeField]
    int amount;
    public override void OnPickUp(Transform item)
    {
        var playerInventory = item.GetComponentInChildren<Container>();
        GameManager.Instance.Respawner.Despawn(gameObject, respawnTime);
        playerInventory.Put(weaponType.ToString(), amount);
        item.GetComponent<PlayerShoot>().activeWeapon.reloader.HandleOnAmmoChanged();
    }
}