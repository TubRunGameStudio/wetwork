using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static PlayerInventory;

[System.Serializable]
public class SetRecipeEvent : UnityEvent<CraftingRecipe> { }

public class CraftingArea : MonoBehaviour
{
    [SerializeField] private Image craftImage;
    [SerializeField] private Button craftButton;
    [SerializeField] private GameObject componentsImagesContainer;
    public SetRecipeEvent changeRecipeEvent;
    private List<Image> componentsImages;
    private CraftingRecipe recipe;

    private GameController controller;
    private PlayerInventory inventory;

    private void Init()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        inventory = controller.player.GetComponent<PlayerInventory>();

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
        RefreshButton();
        changeRecipeEvent.Invoke(recipe);
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
        return canCraft;
    }

    public void Craft()
    {
        Dictionary<ComponentType, int> amounts = new();
        foreach (ComponentType type in Enum.GetValues(typeof(ComponentType)).Cast<ComponentType>())
        {
            amounts.Add(type, 0);
        }

        foreach (Component component in recipe.components)
            amounts[component.componentType]++;

        foreach (KeyValuePair<ComponentType, int> pair in amounts)
        {
            if (pair.Value == 0)
                continue;

            PlayerState.components[pair.Key] = PlayerState.components[pair.Key] - pair.Value;
            
        }
        inventory.Pickup(recipe);
        RefreshButton();
    }

    private void RefreshButton()
    {
        if (CanCraft())
            craftButton.interactable = true;
        else
            craftButton.interactable = false;
    }
}
