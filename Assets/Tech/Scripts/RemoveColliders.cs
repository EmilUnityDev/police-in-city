using UnityEngine;

public class RemoveColliders : MonoBehaviour
{
	public void RemoveComponents()
	{
		Component[] components = GetComponentsInChildren(typeof(Collider), true);

		foreach (var c in components)
		{
			DestroyImmediate(c);
		}
	}
}