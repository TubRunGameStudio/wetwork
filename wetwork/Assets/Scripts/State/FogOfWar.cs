using UnityEngine;
using UnityEngine.SceneManagement;

public class FogOfWar : MonoBehaviour
{
    private string key;

    void Start()
    {
        key = SceneManager.GetActiveScene().name + "_" + gameObject.name;
        bool hidden = SceneState.TryAddFog(key);
        if (hidden == false)
            Destroy(gameObject);
    }

    public void Reveal()
    {
        SceneState.RevealFog(key);
        Destroy(gameObject);
    }
}
