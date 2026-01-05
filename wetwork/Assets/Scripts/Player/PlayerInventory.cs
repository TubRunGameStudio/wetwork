using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] InputActionReference aim;
    [SerializeField] InputActionReference weaponSelect;
    
    public GameController controller;

    public void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    public void FixedUpdate()
    {
        Vector2 aimPos = Camera.main.ScreenToWorldPoint(aim.action.ReadValue<Vector2>());
        PlayerState.CurrentWeapon.CanFire(aimPos, controller);
    }

    void OnEnable()
    {
        weaponSelect.action.performed += SelectWeapon;
    }

    private void OnDisable()
    {
        weaponSelect.action.started -= SelectWeapon;

    }

    private void SelectWeapon(InputAction.CallbackContext ctx)
    {
        float val = ctx.ReadValue<float>();
        if(val > 0)
        {
            if (PlayerState.CurrentWeaponIndex >= PlayerState.Weapons.Count - 1)
                PlayerState.CurrentWeaponIndex = 0;
            else
                PlayerState.CurrentWeaponIndex++;

            PlayerState.CurrentWeapon = PlayerState.Weapons[PlayerState.CurrentWeaponIndex];
            UpdateText(PlayerState.CurrentWeapon.Ammo().ToString());
        }

    }

    public void Initiate()
    {
        text = controller.AMMO_TXT;

        // TODO: update for multiple PlayerState.Weapons
        // Update ammo text
        int ammo = PlayerState.CurrentWeapon.Ammo();
        UpdateText(ammo.ToString());
    }

    public void Fire(InputAction.CallbackContext ctx, GameController controller)
    {
        if (PlayerState.CurrentWeapon.CanFire())
        {
            int ammo = PlayerState.CurrentWeapon.Fire(ctx);
            UpdateText(ammo.ToString());
        }
    }

    public void Pickup(string name, int amount)
    {
        Weapon weapon = PlayerState.Weapons.Find(w => w.name == name);
        int ammo = weapon.PickupAmmo(amount);

        if(weapon == PlayerState.CurrentWeapon)
            UpdateText(ammo.ToString());
    }

    private void UpdateText(string txt)
    {
        text.text = txt;
    }
}
