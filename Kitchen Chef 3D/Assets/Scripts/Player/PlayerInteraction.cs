using System.Collections;
using UnityEngine;

//Run player interaction's with interactive objects
public class PlayerInteraction : MonoBehaviour
{
	Animator animator;

	PlayerDetector detector;
	public InteractableCanvas interactableCanvas; //Canvas that displays object's infomation

	[SerializeField] Transform ingredientHolder; //Position of ingredient when been holding
	Ingredient ingredient; //current ingredient
	public bool holdingIngredient { get { return ingredient != null; } }

	private void Start()
	{
		if(interactableCanvas == null) { PrintConsole.Error("No Interactable Canvas attached"); }

		animator = GetComponentInChildren<Animator>();
		detector = GetComponentInChildren<PlayerDetector>();
	}

	private void Update()
	{
		interactableCanvas.SetInfo(); //Clear infos

		if (detector.inDetection == null) { return; }

		interactableCanvas.SetInfo(detector.inDetection);

		if (Input.GetKeyDown(KeyCode.E))
		{
			detector.inDetection.Interact(this);
		}
	}

	public void CatchIngredient(Ingredient ingredient)
	{
		animator.SetTrigger("pick");
		ingredient.transform.parent = ingredientHolder.transform;
		ingredient.transform.localPosition = Vector3.zero;
		this.ingredient = ingredient;
	}

	public Ingredient DropIngredient()
	{
		animator.SetTrigger("drop");
		return ingredient;
	}
}