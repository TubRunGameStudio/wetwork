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
    public const string ICON = "camera";

    public override string name { get { return NAME; } }
    public override string icon { get { return ICON; } }

    public GameObject projectile;
    public GameObject container;
    private Vector3 target;
    private Vector3 origin;
    private bool canFire;

    public override bool CanFire(Vector2 aim, GameController controller)
    {

        Vector3 point = aim;
        point.z = 0;
        Vector3 playerPos = controller.PLAYER.transform.position;
        Vector3 vec = point - playerPos;
        RaycastHit2D hit = Physics2D.Raycast(playerPos, vec, vec.magnitude, controller.OBSTACLE_LAYERMASK);

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

    public override bool CanFire()
    {
        return canFire && ammo > 0;
    }

    public override int Fire(InputAction.CallbackContext ctx)
    {
        if (container == null) {
            container = GameObject.Find("CCTVs");
        }
        if (projectile == null)
            projectile = (GameObject)Resources.Load("projectile", typeof(GameObject));


        GameObject obj = GameObject.Instantiate(projectile);
        CCTV_Projectile proj = obj.GetComponent<CCTV_Projectile>();
        proj.transform.SetParent(container.transform);
        proj.transform.position = origin;
        proj.Target = target;
        proj.Activate();
        ammo--;
    
        return ammo;
    }
}
