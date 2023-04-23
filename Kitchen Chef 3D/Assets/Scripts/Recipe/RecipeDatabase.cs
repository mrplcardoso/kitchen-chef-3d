using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe Database",
	menuName = "Scriptable Objects/Recipe Database", order = 1)]
public class RecipeDatabase : ScriptableObject
{
	[SerializeField] Recipe[] recipes;
	
	public Recipe[] LoadRecipes()
	{
		return recipes.Clone() as Recipe[];
	}
}