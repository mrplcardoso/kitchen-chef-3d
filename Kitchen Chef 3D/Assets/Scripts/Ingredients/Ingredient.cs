using System.Collections;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
	[SerializeField] IngredientType ingredientType;
	public IngredientType type { get { return ingredientType; } }
	public bool fresh;
}