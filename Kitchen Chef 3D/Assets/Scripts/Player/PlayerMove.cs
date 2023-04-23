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

	Animator animator;

	private void Awake()
	{
		//GetComponente<>() is the method that allows accessing
		//a component, of a scene object, by code
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
	}

	private void Update()
	{
		InputDirection();
		Move();
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
		animator.SetFloat("speed", velocity.sqrMagnitude);
	}
}
