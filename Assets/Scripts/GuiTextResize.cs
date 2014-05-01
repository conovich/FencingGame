using UnityEngine;
using System.Collections;

public class GuiTextResize : MonoBehaviour {

	float virtualWidth = 1920.0f;
	
	float virtualHeight = 1080.0f;

	Matrix4x4 matrix;
	
	void Start () {
		matrix = new Matrix4x4();
		matrix.SetTRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width/virtualWidth, Screen.height/virtualHeight, 1.0f));
	}
	
	void OnGUI () 
		
	{
		GUI.matrix = matrix;
		GUILayout.Label ("Woo");
		
	}
}
