using UnityEngine;

public abstract class Weapon
{
    protected int ammo = 0;
    protected int maxAmmo = 99;
    public abstract string name { get; }

    public abstract int Fire();

    public bool HasAmmo()
    {
        return ammo > 0;
    }

    public int PickupAmmo(int pickup) 
    {
        ammo += pickup;
        if (ammo > maxAmmo)
            ammo = maxAmmo;

        return ammo;
    }
}
