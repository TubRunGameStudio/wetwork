using NUnit.Framework;
using System;
using System.Collections.Generic;
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
    }
}
