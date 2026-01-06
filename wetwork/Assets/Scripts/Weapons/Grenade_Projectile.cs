using System.Collections;
using UnityEngine;

public class Grenade_Projectile : Projectile
{
    [SerializeField] GameObject explosion;
    protected override IEnumerator ArcMove()
    {
        yield return base.ArcMove();
        Explode();
    }

    private void Explode()
    {
        GameObject expl = GameObject.Instantiate(explosion);
        expl.transform.position = transform.position;
        Destroy(gameObject);
    }
}
