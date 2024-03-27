using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
	//CharacterController: helps implement non-physics-based 3D characters
	CharacterController controller; //reference
	Vector3 velocity;
	//movement direction, defined by the player (Input.GetAxis())
	Vector3 direction;
	[SerializeField] float speed = 5f;

	PlayerAnimation animation;
	public bool waitAnimation;

	private void Awake()
	{
		//GetComponente<>() is the method that allows accessing
		//a component, of a scene object, by code
		controller = GetComponent<CharacterController>();
		animation = GetComponentInChildren<PlayerAnimation>();

		animation.OnDropStart += OnAnimationStart;
		animation.OnDropFinished += OnAnimationFinished;
		animation.OnPickStart += OnAnimationStart;
		animation.OnPickFinished += OnAnimationFinished;
	}

	private void Update()
	{
		//TODO: Implement InputHandler and stop input checking
		if(waitAnimation) { return; }

		InputDirection();
		Move();
	}

	void OnAnimationStart()
	{
		waitAnimation = true;
	}

	void OnAnimationFinished()
	{
		waitAnimation = false;
	}

	void InputDirection()
	{
		//3D movement is done in the XZ plane
		//Input.GetAxis(): if the player isn't controlling, returns 0,
		//otherwise return values in 0/1 or 0/-1 range.
		//left/down = -1; right/up = 1
		direction.x = Input.GetAxis("Horizontal"); //left/right
		direction.z = Input.GetAxis("Vertical"); //up/down

		//normalizes the direction when it exceeds size 1
		//to take effect of the intensity of the analog stick
		direction = Vector3.ClampMagnitude(direction, 1f);
	}

	private void Move()
	{
		velocity = speed * direction;

		//sets the character's orientation so he looks in the direction he moves
		if (velocity.sqrMagnitude > 0) //sets orientation only when moving
		{ transform.forward = direction.normalized; }

		controller.Move(velocity * Time.deltaTime);
		animation.animator.SetFloat("speed", velocity.sqrMagnitude);
	}
}
