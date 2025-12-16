using UnityEngine;

public abstract class Weapon
{
    protected int ammo = 0;
    protected int maxAmmo = 99;

    public abstract void Fire();

    public void PickupAmmo(int pickup) 
    {
        ammo += pickup;
        if (ammo > maxAmmo)
            ammo = maxAmmo;
    }
}
