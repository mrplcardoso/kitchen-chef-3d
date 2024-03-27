using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour, IInteractable, IAsyncAction
{
	List<IngredientType> ingredients;
	Recipe current;

	void Awake()
	{
		MeshRenderer[] m = GetComponentsInChildren<MeshRenderer>();
		materials = new Material[m.Length];
		originals = new Color[m.Length];
		for (int i = 0; i < m.Length; i++)
		{ materials[i] = m[i].material; originals[i] = m[i].material.color; }
	}

	#region Interactable

	public string actionHint { get { return "Press 'E' to drop/cook"; } }


	public void Interact(PlayerInteraction player)
	{
		if(processing) { return; }

		//Cook
		if (!player.holdingIngredient) { Trigger(); return; }

		//Drop
		//TODO: Pooling
		Ingredient i = player.DropIngredient();
		ingredients.Add(i.type);
		Destroy(i.gameObject);
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

	#region AsyncAction

	public float processTime { get { return current.cookingTime; } }
	public bool processing { get; private set; }

	public void Trigger()
	{
		//TODO: show fill bar
		processing = true;
		StartCoroutine(WaitAction());
	}

	IEnumerator WaitAction()
	{
		yield return new WaitForSeconds(processTime);
		OnEnd();
	}

	public void OnEnd()
	{
		processing = false;
		//TODO: hide fill bar
		//TODO: show get dish icon
	}

	#endregion AsynAction
}
