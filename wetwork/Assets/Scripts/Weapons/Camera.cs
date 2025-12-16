using UnityEngine;

public class Camera : Weapon
{

    public override void Fire()
    {
        Debug.Log("Fired the Camera");
        if (ammo == 0) 
            return;
        
        ammo--;
    }
}
