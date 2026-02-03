using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static PlayerInventory;

public class CraftingArea : MonoBehaviour
{
    [SerializeField] private Image craftImage;
    [SerializeField] private Button craftButton;
    [SerializeField] private GameObject componentsImagesContainer;
    private List<Image> componentsImages;
    private CraftingRecipe recipe;

    private void Init()
    {
        componentsImages = new List<Image>();
        componentsImages.AddRange(componentsImagesContainer.GetComponentsInChildren<Image>());
    }

    public void SetRecipe(CraftingRecipe craftingRecipe)
    {
        if (componentsImages == null)
            Init();

        recipe = craftingRecipe;
        craftImage.sprite = recipe.image;

        for(int i = 0; i < componentsImages.Count; i++)
        {
            if (recipe.components.Count > i)
                componentsImages[i].sprite = recipe.components[i].sprite;
            else
                componentsImages[i].color = Color.black;
        }
        if (CanCraft())
            craftButton.interactable = true;
        else
            craftButton.interactable = false;
    }

    private bool CanCraft()
    {
        Dictionary<ComponentType, int> amounts = new();
        foreach(ComponentType type in Enum.GetValues(typeof(ComponentType)).Cast<ComponentType>()) {
            amounts.Add(type, 0);
        }

        foreach(Component component in recipe.components)
            amounts[component.componentType]++;

        bool canCraft = true;
        foreach(KeyValuePair<ComponentType, int> pair in amounts)
        {
            if (pair.Value == 0)
                continue;

            if (!PlayerState.components.ContainsKey(pair.Key) ||  PlayerState.components[pair.Key] < pair.Value)
                canCraft = false;
        }
        Debug.Log("CanCraft: " + canCraft);
        return canCraft;
    }
}
