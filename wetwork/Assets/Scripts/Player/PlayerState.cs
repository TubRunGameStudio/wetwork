using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerState
{
    public static int Health;
    public static List<Weapon> Weapons;
    public static Vector3 PlayerPosition;

    public static void InitGame(int maxHealth, List<Weapon> weapons, Vector3 playerPos)
    {
        Debug.Log("Starting Game");
        Health = maxHealth;
        Weapons = weapons;
        PlayerPosition = playerPos;
    }
}
