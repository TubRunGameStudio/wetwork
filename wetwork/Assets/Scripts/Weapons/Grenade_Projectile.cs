using System.Collections;
using UnityEngine;

public class Grenade_Projectile : Projectile
{

    protected override IEnumerator ArcMove()
    {
        yield return base.ArcMove();
        Debug.Log("Boom!");
    }
}
