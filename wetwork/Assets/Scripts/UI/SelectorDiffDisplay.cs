using TMPro;
using UnityEngine;

public class SelectorDiffDisplay : MonoBehaviour
{
    [SerializeField] private AmmoPickup.Ammo ammoType;
    private TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.color = Color.darkSeaGreen;
        text.text = string.Empty;
    }

    public void SetRecipe(CraftingRecipe recipe)
    {
        if(recipe.ammo == ammoType)
            text.SetText($"(+1)");
        else
            text.SetText(string.Empty);
    }
}
