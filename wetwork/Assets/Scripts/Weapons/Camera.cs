using UnityEngine;

public class Camera : Weapon
{

    public override int Fire()
    {
        if(ammo > 0)
            ammo--;
    
        return ammo;
    }
}
