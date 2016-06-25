using UnityEngine;
using System.Collections;
using GDGeek;

public class FoeFighter : MonoBehaviour {

	public Body _body;

	public void hit(Collider other){
		_body.post ("hit");
	}
	// Update is called once per frame
	void Update () {
		return;
		if (Input.GetKeyDown (KeyCode.Space)) {

			_body.post ("attack");
			//	_body.fsm_.post ("attack");
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			_body.post ("magic");
			//attack_++;
			//_body.fsm_.post ("magic");
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			_body.post ("def");
			//_body.fsm_.post ("def");
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow)) {
			_body.post ("undef");
			//_body.fsm_.post ("undef");
		}


		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			_body.post ("skill");
			//_body.fsm_.post ("skill");
		}


		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			_body.post ("power");
			//	_body.fsm_.post ("power");
		}
	}
}
