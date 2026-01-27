using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public static class SceneState
{
    private static Dictionary<string, List<Vector3>> cctvs;
    private static Dictionary<string, bool> fogOfWar;
    private static Dictionary<string, bool> relays;

    static SceneState()
    {
        cctvs = new Dictionary<string, List<Vector3>>();
        fogOfWar = new Dictionary<string, bool>();
        relays = new Dictionary<string, bool>();
    }

    public static void SetRelay(string path, bool active)
    {
        bool isNew = relays.TryAdd(path, active);
        if(!isNew)
        {
            relays[path] = active;
        }
    }

    public static bool GetRelay(string path)
    {
        relays.TryGetValue(path, out bool active);
        return active;
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

    public static string GetFullPathName(GameObject obj)
    {
        string path = SceneManager.GetActiveScene().name + "/" + obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }
}
