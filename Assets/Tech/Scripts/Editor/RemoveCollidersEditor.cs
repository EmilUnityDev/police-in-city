using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RemoveColliders))]
public class RemoveCollidersEditor : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		RemoveColliders rmc = (RemoveColliders)target;

		if (GUILayout.Button("Remove ComponentName"))
		{
			rmc.RemoveComponents();
		}
	}
}
