using UnityEngine;
using System.Collections;

public class BodyBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
//		this.gameObject.SendMessageUpwards ("hit");
	}

	public void OnTriggerEnter(Collider other){
		Debug.Log ("!!!!!!" + other.name);
		this.gameObject.SendMessageUpwards ("hit", other);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
