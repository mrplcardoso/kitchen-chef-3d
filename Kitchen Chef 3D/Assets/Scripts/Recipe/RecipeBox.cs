using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeBox : MonoBehaviour
{
	public RectTransform rectTransform { get; private set; }
	[SerializeField] TextMeshProUGUI recipeName;
	public string recipe { get { return recipeName.text; } set { recipeName.text = value; } }
	[SerializeField] TextMeshProUGUI recipeDescription;
	public string description { get { return recipeDescription.text; } set { recipeDescription.text = value; } }

	void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		recipeName = GetComponentsInChildren<TextMeshProUGUI>()[0];
		recipeDescription = GetComponentsInChildren<TextMeshProUGUI>()[1];
	}
}
