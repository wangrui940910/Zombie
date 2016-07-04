using UnityEngine;
using System.Collections;
using GDGeek;

public class GameManager : MonoBehaviour {


	private FSM fsm_ = new FSM ();

	public Outside _out = null;
	public Inside _in = null;


	private State getOutside(){
		StateWithEventMap swem = TaskState.Create(delegate {
			Task o = _out.run();

			return o; 
		}, this.fsm_, "in");


		return swem;
	}
	private State getInside(){
		StateWithEventMap swem = TaskState.Create(delegate {
			Task o = _in.run();
			return o; 
		}, this.fsm_, "out");
		return swem;
	}
	// Use this for initialization
	void Start () {

		fsm_.addState ("out", getOutside());
		fsm_.addState ("in", getInside ());
		fsm_.init ("out");
	}
	

}
