using UnityEngine;
using System.Collections;

public class SwordSwordCollisions : MonoBehaviour {
	public string _HiltTag = "Hilt2";
	public AudioSource _SwordAudio;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == _HiltTag){
			float random = Random.Range(-1.0f, 2.0f);
			_SwordAudio.pitch = random;
			_SwordAudio.Play();
			Debug.Log("collision!!!");
		}
	}
}
