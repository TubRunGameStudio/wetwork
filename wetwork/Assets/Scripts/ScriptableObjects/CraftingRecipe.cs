using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AmmoPickup;
using static PlayerInventory;

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Scriptable Objects/CraftingRecipe")]
public class CraftingRecipe : ScriptableObject
{
    public string recipeName;
    public Ammo ammo;
    public Sprite image;
    public List<Component> components;
}
