using System.ComponentModel;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ComponentDiffDisplay : MonoBehaviour
{
    [SerializeField] private ComponentType component;
    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = Color.red;
        text.text = string.Empty;
    }

    public void SetRecipe(CraftingRecipe recipe)
    {
        int count = 0;
        foreach(Component comp in recipe.components)
        {
            if (comp.componentType == component)
                count++;
        }
        if (count > 0)
            text.SetText($"(-{count})");
        else
            text.SetText(string.Empty);
    }
}
