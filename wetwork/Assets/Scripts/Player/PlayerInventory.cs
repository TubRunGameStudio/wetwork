using NUnit.Framework;
using System.Collections.Generic;
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
    }

    public void Fire()
    {
        int ammo = weapons[0].Fire();
        text.text = ammo.ToString();
    }
}
