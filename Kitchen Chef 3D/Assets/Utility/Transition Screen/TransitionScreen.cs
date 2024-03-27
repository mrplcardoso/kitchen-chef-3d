using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Utility.EasingEquations;

public class TransitionScreen : MonoBehaviour
{
	GraphicRaycaster raycaster;
	Image transitionImage;
	public float alpha { get { return transitionImage.color.a; } 
		set { Color c = transitionImage.color; c.a = value; transitionImage.color = c; } }

	private void Awake()
	{
		if (!SetInstance()) { return; }

		transitionImage = GetComponentInChildren<Image>();
		raycaster = GetComponent<GraphicRaycaster>();
		alpha = 1f;
	}

	#region Singleton
	public static TransitionScreen instance { get; private set; }
	bool SetInstance()
	{
		TransitionScreen[] t = FindObjectsOfType<TransitionScreen>();
		if(t.Length > 1) { Destroy(gameObject); return false; }
		DontDestroyOnLoad(gameObject); instance = this; return true;
	}
	#endregion

	#region Transition
	Coroutine transition;
	float speed = 1;
	public bool inTransition { get; private set; }

	IEnumerator Transition(float end)
	{
		inTransition = true;
		float start = alpha;
		float t = 0;
		while (t < 1.01f)
		{
			alpha = EasingFloatEquations.Linear(start, end, t);
			t += speed * Time.unscaledDeltaTime;
			yield return null;
		}
		alpha = end;
		inTransition = false;
	}

	public void TransitionOn()
	{
		if(transition != null)
		{ StopCoroutine(transition); }
		raycaster.enabled = true;
		transition = StartCoroutine(Transition(1));
	}

	public void TransitionOff()
	{
		if (transition != null)
		{ StopCoroutine(transition); }
		raycaster.enabled = false;
		transition = StartCoroutine(Transition(0));
	}
	#endregion
}
