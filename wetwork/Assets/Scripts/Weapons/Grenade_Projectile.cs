using UnityEngine;

public class Grenade_Projectile : Projectile
{
    public override void Activate()
    {
        base.Activate();
        Debug.Log("Boom!");
    }
}
