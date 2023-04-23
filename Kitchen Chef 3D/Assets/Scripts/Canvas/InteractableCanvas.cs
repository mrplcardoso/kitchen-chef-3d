using System.Collections;
using UnityEngine;
using TMPro;

//Implements routines to display object information on the screen
public class InteractableCanvas : MonoBehaviour
{
	TextMeshProUGUI interactableName, interactableHint;

	void Start()
	{
		SetTextFields();
	}

	void SetTextFields()
	{
		TextMeshProUGUI[] t = transform.GetComponentsInChildren<TextMeshProUGUI>();
		for (int i = 0; i < t.Length; i++)
		{
			if (t[i].CompareTag("Interactable Name")) { interactableName = t[i]; }
			if (t[i].CompareTag("Interactable Hint")) { interactableHint = t[i]; }
		}

		if (interactableName == null) { PrintConsole.Error(
			"No field 'detectable name' with proper Tag found in Interactable Canvas, check tag of it's children"); }
		if (interactableHint == null) { PrintConsole.Error(
			"No field 'interactable hint' with proper Tag found in Interactable Canvas, check tag of it's children"); }
	}

	public void SetInfo(IInteractable interacted = null)
	{
		if (interacted == null) { interactableName.text = ""; interactableHint.text = ""; return; }

		interactableName.text = interacted.name;
		interactableHint.text = interacted.actionHint;
	}
}