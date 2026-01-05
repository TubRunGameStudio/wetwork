using UnityEngine;
using UnityEngine.InputSystem;

public class Grenade : Weapon
{
    public const string NAME = "GRENADE";
    public override string name { get { return NAME; } }

    public override int Fire(InputAction.CallbackContext ctx)
    {
        Debug.Log("BOOM!");
        ammo--;
        return ammo;
    }
    public override bool CanFire()
    {
        return ammo > 0;
    }
    public override bool CanFire(Vector2 aim, GameController controller)
    {
        return CanFire();
    }
}
