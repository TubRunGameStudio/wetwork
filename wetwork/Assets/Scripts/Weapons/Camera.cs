using UnityEngine;

public class Camera : Weapon
{
    public const string NAME = "CAMERA";
    public override string name { get { return NAME; } }

    public override int Fire()
    {
        if(ammo > 0)
            ammo--;
    
        return ammo;
    }
}
