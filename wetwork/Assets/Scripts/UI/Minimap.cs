using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [SerializeField] private GameObject minimap;
    [SerializeField] private GameObject staticMap;
    public bool minimapEnabled;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        minimapEnabled = false;
    }

    private void Update()
    {
        if (minimapEnabled)
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
