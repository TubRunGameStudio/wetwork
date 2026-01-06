using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public abstract class ArcWeapon : Weapon
{
    protected abstract string projectilePath { get; }
    protected abstract GameObject container { get;  }

    public GameObject projectile;
    protected Vector3 target;
    protected Vector3 origin;
    protected bool canFire;

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
            target = point;
        }
        else
        {
            // Hit
            target = hit.point;
        }
        origin = playerPos;
        controller.RETICULE.SetActive(true);
        controller.RETICULE.transform.position = target;
        canFire = true;
        return true;
    }

    public override bool CanFire()
    {
        return canFire && ammo > 0;
    }

    public override int Fire(InputAction.CallbackContext ctx)
    {
        if (projectile == null)
            projectile = (GameObject)Resources.Load(projectilePath, typeof(GameObject));


        GameObject obj = GameObject.Instantiate(projectile);
        Projectile proj = GetProjectile(obj);
        proj.transform.SetParent(container.transform);
        proj.transform.position = origin;
        proj.Target = target;
        proj.Activate();
        ammo--;
        
        return ammo;
    }

    protected abstract Projectile GetProjectile(GameObject obj);
}
