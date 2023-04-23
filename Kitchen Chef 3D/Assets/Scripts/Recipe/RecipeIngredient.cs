using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RecipeIngredient
{
  public IngredientType ingredient;
  public string amount;
  public bool prepared;
  public string preparation { get { return prepared ? "prepared" : ""; } }
}
