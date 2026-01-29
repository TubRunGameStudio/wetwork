using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FogOfWar : MonoBehaviour
{
    public UnityEvent revealFogEvent;
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
        revealFogEvent.Invoke();
        Destroy(gameObject);
    }
}
