using UnityEngine;

//Class that detects objects by overlaying/triggering colliders
[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerDetector : MonoBehaviour
{
	//maintains a reference to the object being detected
	//while in the player's view
	public IInteractable inDetection { get; private set; }

	//When overlapping, it highlights the detected object
	private void OnTriggerEnter(Collider other)
	{
		if (inDetection != null) { return; }

		IInteractable detected = other.GetComponent<IInteractable>();
		if (detected != null) { inDetection = detected; detected.Hightlight(); }
	}

	//When exiting overlay with a detectable,
	//de-highlight the detected object
	private void OnTriggerExit(Collider other)
	{
		IInteractable detected = other.GetComponent<IInteractable>();
		if (detected != null) { inDetection = null; detected.DeHightlight(); }
	}
}