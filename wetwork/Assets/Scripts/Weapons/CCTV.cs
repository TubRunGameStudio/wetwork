using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
using static UnityEngine.UI.Image;

public class CCTV : Weapon
{
    public const string NAME = "CAMERA";
    public override string name { get { return NAME; } }
    public GameObject projectile;

    public override bool CanFire(InputAction.CallbackContext ctx, Vector3 playerPos, GameController controller)
    {
        if (ammo <= 0)
            return false;

        Vector3 point = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
        point.z = 0;
        Vector3 vec = point - playerPos;
        RaycastHit2D hit = Physics2D.Raycast(playerPos, vec, vec.magnitude, controller.OBSTACLE_LAYERMASK);

        if (hit.collider == null)
        {
            // No Hit
            return false;
        }
        else
        {
            // Hit
            return true;
        }
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
