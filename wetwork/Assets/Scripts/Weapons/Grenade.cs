using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class Grenade : Weapon
{
    public const string NAME = "GRENADE";
    public const string ICON = "grenade";
    public override string name { get { return NAME; } }
    public override string icon { get { return ICON; } }

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
