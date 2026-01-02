using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public static class SceneState
{
    private static Dictionary<string, List<CCTV_Projectile>> cctvs;

    static SceneState()
    {
        cctvs = new Dictionary<string, List<CCTV_Projectile>>();
    }

    public static void Add(string scene, CCTV_Projectile cctv)
    {
        List<CCTV_Projectile> cctvsInScene;
        if (cctvs.TryGetValue(scene, out cctvsInScene))
            cctvsInScene.Add(cctv);
        else
        {
            cctvsInScene = new List<CCTV_Projectile>();
            cctvsInScene.Add(cctv);
            cctvs.Add(scene, cctvsInScene);
        }
    }

    public static List<CCTV_Projectile> GetCCTVs(string scene)
    {
        List<CCTV_Projectile> cctvsInScene;
        cctvs.TryGetValue(scene, out cctvsInScene);
        return cctvsInScene;
    }
}
