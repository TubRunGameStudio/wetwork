using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CCTV : Weapon
{
    public const string NAME = "CAMERA";
    public override string name { get { return NAME; } }
    public GameObject projectile;

    public override int Fire(InputAction.CallbackContext ctx)
    {
        if (projectile == null)
            projectile = (GameObject)Resources.Load("projectile", typeof(GameObject));


        GameObject proj = GameObject.Instantiate(projectile);
        Vector3 vec = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
        vec.z = 0;
        proj.transform.position = vec;
        proj.GetComponent<CCTV_Projectile>().Activate();

        ammo--;
    
        return ammo;
    }
}
