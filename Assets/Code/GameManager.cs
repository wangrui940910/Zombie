using UnityEngine;
using System.Collections;
using GDGeek;

public class GameManager : MonoBehaviour {
	private FSM fsm_ = new FSM ();
	public WeFighter _we = null;
	public FoeFighter _foe = null;
	private State getBegin(){
		StateWithEventMap swem = TaskState.Create(delegate {
			return new Task();
		}, this.fsm_, "fighting");


		return swem;
	}
	private State getFighting(){
		StateWithEventMap swem = new StateWithEventMap ();
		swem.onStart += delegate {
			Debug.Log("asdfasdfasdfsdfsdf");	
		};


		return swem;
	}
	// Use this for initialization
	void Start () {

		fsm_.addState ("begin", getBegin());
		fsm_.addState ("fighting", getFighting ());

		fsm_.init ("begin");
	}
	

}
