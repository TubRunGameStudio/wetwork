using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CCTV : Weapon
{
    public const string NAME = "CAMERA";
    public override string name { get { return NAME; } }
    public GameObject projectile;

    public override bool CanFire()
    {
        return ammo > 0;
    }

    public override int Fire(InputAction.CallbackContext ctx, Vector3 playerPos)
    {
        
        if (projectile == null)
            projectile = (GameObject)Resources.Load("projectile", typeof(GameObject));


        GameObject obj = GameObject.Instantiate(projectile);
        Vector3 vec = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
        vec.z = 0;
        CCTV_Projectile proj = obj.GetComponent<CCTV_Projectile>();
        proj.transform.position = playerPos;
        proj.Target = vec;
        proj.Activate();
        ammo--;
    
        return ammo;
    }
}
