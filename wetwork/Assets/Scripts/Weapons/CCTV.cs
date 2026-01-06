using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;
using static UnityEngine.UI.Image;

public class CCTV : ArcWeapon
{
    public const string NAME = "CAMERA";
    public const string ICON = "camera";
    public const string PROJECTILE = "cctv_prefab";

    public override string name { get { return NAME; } }
    public override string icon { get { return ICON; } }

    protected override string projectilePath { get { return PROJECTILE; } }
    protected override GameObject container { get { return GameObject.Find("Doodads"); } }

    protected override Projectile GetProjectile(GameObject obj)
    {
        return obj.GetComponent<CCTV_Projectile>();
    }
}
