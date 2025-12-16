using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private List<Weapon> weapons;


    public void Start()
    {
        weapons = new List<Weapon>();
        weapons.Add(new Camera());
        UpdateText("0");
    }

    public void Fire()
    {
        int ammo = weapons[0].Fire();
        UpdateText(ammo.ToString());
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
