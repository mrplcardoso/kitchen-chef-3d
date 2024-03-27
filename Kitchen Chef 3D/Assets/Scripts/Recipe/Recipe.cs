using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Recipe
{
  public string name;
  public float cookingTime;
  public List<RecipeIngredient> ingredients;
}