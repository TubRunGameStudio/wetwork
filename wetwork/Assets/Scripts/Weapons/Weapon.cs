using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Weapon
{
    protected int ammo = 0;
    protected int maxAmmo = 99;
    public abstract string name { get; }

    public abstract int Fire(InputAction.CallbackContext ctx);
    public abstract bool CanFire();
    public abstract bool CanFire(Vector2 aim, GameController controller);

    public int PickupAmmo(int pickup) 
    {
        ammo += pickup;
        if (ammo > maxAmmo)
            ammo = maxAmmo;

        return ammo;
    }
}
