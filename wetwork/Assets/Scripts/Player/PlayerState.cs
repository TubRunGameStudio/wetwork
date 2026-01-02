using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerState
{
    public static int Health;
    public static List<Weapon> Weapons;
    public static Vector3 PlayerLoadPosition;
    public static Vector3 PlayerReturnPosition;

    public static void InitGame(int maxHealth, List<Weapon> weapons)
    {
        Health = maxHealth;
        Weapons = weapons;
        Weapons.Add(new CCTV());
        PlayerLoadPosition = new Vector3(0,0,0);
    }
}
