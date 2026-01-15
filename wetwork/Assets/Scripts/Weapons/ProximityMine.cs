using UnityEngine;
using UnityEngine.InputSystem;

public class ProximityMine : Weapon
{
    private const string NAME = "PROXIMITY";
    private const string ICON = "proximity_mine";
    private const string PREFAB_PATH = "proximity_mine_prefab";
    public override string name { get { return NAME; } }
    public override string icon { get { return ICON; } }

    private GameObject container { get { return GameObject.Find("Doodads"); } }

    private GameObject prefab;

    public override bool CanFire(Vector2 aim, GameController controller)
    {
        controller.RETICULE.SetActive(false);
        return CanFire();
    }

    public override bool CanFire()
    {
        return ammo > 0;
    }

    public override int Fire(InputAction.CallbackContext ctx)
    {
        if (prefab == null)
            prefab = (GameObject)Resources.Load(PREFAB_PATH, typeof(GameObject));


        GameObject obj = GameObject.Instantiate(prefab);
        obj.transform.SetParent(container.transform);
        obj.transform.position = PlayerController.PLAYER.transform.position;
        ammo--;

        return ammo;
    }
}
