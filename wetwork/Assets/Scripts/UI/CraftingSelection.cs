using UnityEngine;
using UnityEngine.EventSystems;

public class CraftingSelection : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] CraftingArea craftingArea;
    [SerializeField] CraftingRecipe recipe;
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        craftingArea.gameObject.transform.parent.gameObject.SetActive(true);
        craftingArea.SetRecipe(recipe);
    }
}
