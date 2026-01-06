using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CCTV_Projectile : Projectile
{
    [SerializeField] private GameObject cutout;

    protected override IEnumerator ArcMove()
    {
        yield return base.ArcMove();
        cutout.SetActive(true);

        // Scene state
        string scene = SceneManager.GetActiveScene().name;
        SceneState.AddCCTV(scene, this);
    }

    public void Load(Vector3 pos)
    {
        transform.position = pos;
        cutout.SetActive(true);
        SetSortingLayer();
    }
}
