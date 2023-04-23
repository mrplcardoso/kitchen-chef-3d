using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientStock : MonoBehaviour, IInteractable
{
	void Awake()
	{
		MeshRenderer[] m = GetComponentsInChildren<MeshRenderer>();
		materials = new Material[m.Length];
		originals = new Color[m.Length];
		for (int i = 0; i < m.Length; i++)
		{ materials[i] = m[i].material; originals[i] = m[i].material.color; }
	}

	#region Interactable

	[SerializeField] Ingredient ingredient; //prefab
	public string actionHint { get { return "'E'"; } }

	//If the player already has an ingredient, it is returned to the stock,
	// otherwise it generates a copy of the ingredient
	public void Interact(PlayerInteraction player)
	{
		//TODO: Pooling
		if (!player.holdingIngredient)
		{ player.CatchIngredient(Instantiate(ingredient)); return; }

		Ingredient i = player.DropIngredient();
		if (i.type == ingredient.type)
		{ Destroy(i.gameObject); }
	}

	#endregion

	#region Detectable

	Material[] materials;
	Color[] originals;

	public void DeHightlight()
	{
		for (int i = 0; i < materials.Length; i++)
		{ materials[i].color = originals[i]; }
	}

	public void Hightlight()
	{
		for (int i = 0; i < materials.Length; i++)
		{ materials[i].color = Color.yellow; }
	}

	#endregion
}