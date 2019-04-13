using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounter : MonoBehaviour
{
    [SerializeField] Text text;
    PlayerShoot playerShoot;
    void Awake()
    {
        GameManager.Instance.OnLocalePlayerJoined += handleOnLocalPlayerJoined;
    }

    void handleOnLocalPlayerJoined(Player player)
    {
        playerShoot = player.GetComponent<PlayerShoot>();
        playerShoot.activeWeapon.reloader.OnAmmoChanged += HandleOnAmmoChanged;
        HandleOnAmmoChanged();
        playerShoot.OnWeaponSwitch += (newActiveWeapon) =>
        {
            HandleOnAmmoChanged();
            newActiveWeapon.reloader.OnAmmoChanged += HandleOnAmmoChanged;
        };
    }

    private void HandleOnAmmoChanged()
    {
        int amountInInventory = playerShoot.activeWeapon.reloader.roundsRemainingInInventory;
        int amountInClip = playerShoot.activeWeapon.reloader.roundsRemainingInClip;
        text.text = amountInClip + "/" + amountInInventory;
    }

    void Update()
    {

    }
}
