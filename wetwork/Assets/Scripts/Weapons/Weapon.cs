using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon
{
    protected int ammo = 0;
    protected int maxAmmo = 99;
    public abstract string name { get; }

    public abstract int Fire(InputAction.CallbackContext ctx, Vector3 playerPos);

    public abstract bool CanFire(InputAction.CallbackContext ctx, Vector3 playerPos, GameController controller);

    public int PickupAmmo(int pickup) 
    {
        ammo += pickup;
        if (ammo > maxAmmo)
            ammo = maxAmmo;

        return ammo;
    }
}
