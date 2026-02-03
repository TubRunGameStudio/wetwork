using UnityEngine;

public class CraftButton : MonoBehaviour
{
    [SerializeField] CraftingArea craftingArea;
    public void CraftClick()
    {
        craftingArea.Craft();
    }
}
