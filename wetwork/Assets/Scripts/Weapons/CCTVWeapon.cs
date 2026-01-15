using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;
using static UnityEngine.UI.Image;

public class CCTVWeapon : ArcWeapon
{
    public const string NAME = "CAMERA";
    public const string ICON = "camera";
    public const string PROJECTILE = "cctv_prefab";

    public override string name { get { return NAME; } }
    public override string icon { get { return ICON; } }

    protected override string projectilePath { get { return PROJECTILE; } }
    protected override GameObject container { get { return GameObject.Find("Doodads"); } }

    public override bool CanFire(Vector2 aim, GameController controller)
    {

        Vector3 point = aim;
        point.z = 0;
        Vector3 playerPos = controller.PLAYER.transform.position;
        Vector3 vec = point - playerPos;
        float distance = vec.magnitude < range ? vec.magnitude : range;
        RaycastHit2D hit = Physics2D.Raycast(playerPos, vec, distance, controller.OBSTACLE_LAYERMASK);

        if (hit.collider == null)
        {
            // No Hit
            canFire = false;
            controller.RETICULE.SetActive(false);
            return false;
        }
        else
        {
            // Hit
            target = hit.point;
            origin = playerPos;
            controller.RETICULE.SetActive(true);
            controller.RETICULE.transform.position = target;
            canFire = true;
            return true;
        }
    }

    protected override Projectile GetProjectile(GameObject obj)
    {
        return obj.GetComponent<CCTV_Projectile>();
    }
}
