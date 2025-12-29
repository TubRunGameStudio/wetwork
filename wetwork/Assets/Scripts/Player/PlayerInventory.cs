using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] InputActionReference aim;
    
    public GameController controller;

    private List<Weapon> weapons;
    private bool canFire;


    public void Awake()
    {
        weapons = new List<Weapon>();
        weapons.Add(new CCTV());
        canFire = false;
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        text = controller.AMMO_TXT;
        UpdateText("0");
    }

    public void FixedUpdate()
    {
        Vector2 aimPos = Camera.main.ScreenToWorldPoint(aim.action.ReadValue<Vector2>());
        weapons[0].CanFire(aimPos, controller);
    }

    public void Fire(InputAction.CallbackContext ctx, GameController controller)
    {
        if (weapons[0].CanFire())
        {
            int ammo = weapons[0].Fire(ctx);
            UpdateText(ammo.ToString());
        }
    }

    public void Pickup(string name, int amount)
    {
        Weapon weapon = weapons.Find(w => w.name == name);
        int ammo = weapon.PickupAmmo(amount);
        UpdateText(ammo.ToString());
    }

    private void UpdateText(string txt)
    {
        text.text = txt;
    }
}
