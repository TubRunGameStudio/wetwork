using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class GrenadeWeapon : ArcWeapon
{
    public const string NAME = "GRENADE";
    public const string ICON = "grenade";
    public const string PROJECTILE = "grenade_prefab";
    public override string name { get { return NAME; } }
    public override string icon { get { return ICON; } }

    protected override string projectilePath { get { return PROJECTILE; } }
    protected override GameObject container { get { return GameObject.Find("Doodads"); } }

    protected override Projectile GetProjectile(GameObject obj)
    {
        return obj.GetComponent<Grenade_Projectile>();
    }

}
