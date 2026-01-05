using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;

public static class SceneState
{
    private static Dictionary<string, List<Vector3>> cctvs;
    private static Dictionary<string, bool> fogOfWar;

    static SceneState()
    {
        cctvs = new Dictionary<string, List<Vector3>>();
        fogOfWar = new Dictionary<string, bool>();
    }

    public static void AddCCTV(string scene, CCTV_Projectile cctv)
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

    public static bool TryAddFog(string fog)
    {
        if (!fogOfWar.ContainsKey(fog))
        {
            fogOfWar.Add(fog, true);
            return true;
        } else
        {
            bool hidden = true;
            fogOfWar.TryGetValue(fog, out hidden);
            return hidden;
        }
    }

    public static void RevealFog(string fog)
    {
        fogOfWar[fog] = false;
    }

    public static void Reset()
    {
        cctvs = new Dictionary<string, List<Vector3>>();
        fogOfWar = new Dictionary<string, bool>();
    }
}
