using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject staticMap;

    private void Awake()
    {
        staticMap.SetActive(true);
        minimap.SetActive(false);
    }

    public void SetMinimap(bool setMinimap)
    {
        if (setMinimap)
        {
            minimap.SetActive(true);
            staticMap.SetActive(false);
        }
        else
        {
            minimap.SetActive(false);
            staticMap.SetActive(true);
        }
    }
}
