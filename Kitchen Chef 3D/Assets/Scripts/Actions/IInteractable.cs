//Interface that implements interactive objects
public interface IInteractable
{
	//Interface inheritance forces the implementation of whatever is declared
	//Serves to relate objects in a non-hierarchical way

	public string name { get; } //inherits from GameObject
	public string actionHint { get; }

	public void Interact(PlayerInteraction player);

	void Hightlight();
	void DeHightlight();
}