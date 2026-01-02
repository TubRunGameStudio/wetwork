using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class SceneState
{
    private static Dictionary<string, List<Vector3>> cctvs;

    static SceneState()
    {
        cctvs = new Dictionary<string, List<Vector3>>();
    }

    public static void Add(string scene, CCTV_Projectile cctv)
    {
        List<Vector3> cctvsInScene;
        if (cctvs.TryGetValue(scene, out cctvsInScene))
            cctvsInScene.Add(cctv.transform.position);
        else
        {
            cctvsInScene = new List<Vector3>();
            cctvsInScene.Add(cctv.transform.position);
            cctvs.Add(scene, cctvsInScene);
        }
    }

    public static List<Vector3> GetCCTVs(string scene)
    {
        List<Vector3> cctvsInScene;
        cctvs.TryGetValue(scene, out cctvsInScene);
        return cctvsInScene;
    }
}
