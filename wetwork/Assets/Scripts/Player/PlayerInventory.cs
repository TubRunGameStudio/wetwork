using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<Weapon> weapons;


    public void Start()
    {
        weapons = new List<Weapon>();
        weapons.Add(new Camera());
    }

    public void Fire()
    {
        weapons[0].Fire();
    }
}
