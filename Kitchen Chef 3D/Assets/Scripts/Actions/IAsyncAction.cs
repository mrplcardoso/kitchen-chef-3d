using System.Collections;
using UnityEngine;


public interface IAsyncAction
{
	public float processTime { get; }
	public bool processing { get; }
	public void Trigger();
	public void OnEnd();
}