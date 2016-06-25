using UnityEngine;
using System.Collections;

public class BodyBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	public void OnTriggerEnter(Collider other){
		Debug.Log ("!!"+this.gameObject.name);
		this.gameObject.SendMessageUpwards ("hit", this.gameObject, SendMessageOptions.RequireReceiver);
		//this.gameObject.SendMessageUpwards ("hittttt", other);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
