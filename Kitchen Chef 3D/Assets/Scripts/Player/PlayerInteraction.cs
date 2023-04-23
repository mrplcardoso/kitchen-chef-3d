using System.Collections;
using UnityEngine;

//Run player interaction's with interactive objects
public class PlayerInteraction : MonoBehaviour
{
	PlayerAnimation animation;

	PlayerDetector detector;
	public InteractableCanvas interactableCanvas; //Canvas that displays object's infomation

	[SerializeField] Transform ingredientHolder; //Position of ingredient when been holding
	Ingredient ingredient; //current ingredient
	public bool holdingIngredient { get { return ingredient != null; } }

	private void Start()
	{
		if(interactableCanvas == null) { PrintConsole.Error("No Interactable Canvas attached"); }

		animation = GetComponentInChildren<PlayerAnimation>();
		detector = GetComponentInChildren<PlayerDetector>();

		animation.OnPickFinished += OnPickFinished;
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

	public void OnPickFinished()
	{
		ingredient.transform.localPosition = Vector3.zero;
	}

	public void CatchIngredient(Ingredient ingredient)
	{
		animation.animator.SetTrigger("pick");
		ingredient.transform.parent = ingredientHolder.transform;
		this.ingredient = ingredient;
	}

	public Ingredient DropIngredient()
	{
		animation.animator.SetTrigger("drop");
		return ingredient;
	}
}