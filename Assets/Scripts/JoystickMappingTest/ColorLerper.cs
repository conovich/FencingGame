using UnityEngine;
using System.Collections;

public class ColorLerper : MonoBehaviour {
	float myAlpha;

	// Use this for initialization
	void Start () {
		myAlpha = renderer.material.color.a;
	}
	
	// Update is called once per frame
	void Update () {
		LerpAlpha(myAlpha);
	}

	public void SetAlpha(float newAlpha){
		myAlpha = newAlpha;
	}

	void LerpAlpha(float endAlpha){
		Color oldColor = renderer.material.color;
		Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, endAlpha);
		renderer.material.color = Color.Lerp(oldColor, newColor, 0.4f);
	}
}
