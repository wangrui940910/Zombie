using UnityEngine;
using System.Collections;
using GDGeek;

public class Outside : MonoBehaviour {
	public GameLine _line = null;
	public Camera _camera;
	private bool isWeakup_ = false;
	private FSM fsm_ = new FSM();
	public Task run(){
		Task task = new Task ();
		task.init = delegate {
			isWeakup_ = true;
			this.fsm_.post("weakup");	
		};
		task.isOver = delegate {
			return !isWeakup_;	
		};
		return task;

	}
	State getSleep(){
		var sleep = new StateWithEventMap ();
		sleep.addAction ("weakup", "weakup");
		sleep.onStart += delegate {
			_camera.gameObject.SetActive(false);
		};
		sleep.onOver += delegate {
			_camera.gameObject.SetActive(true);
		};

		return sleep;
	}
	State getWeakup(){

		var sleep = new StateWithEventMap ();
		sleep.onStart += delegate {
			isWeakup_ = true;
		};

		sleep.onOver += delegate {
			isWeakup_ = false;
		};
		sleep.addAction ("sleep", "sleep");
		return sleep;

	}

	State getWait(){
		var isIn = false;
		StateWithEventMap ts = new StateWithEventMap ();

		ts.addAction ("in", "in");
		return ts;

	}

	State getIn(){
		var instate = TaskState.Create (delegate() {
			return _line.inTask();
		}, fsm_, "sleep");
		return instate;

	}


	// Use this for initialization
	void Start () {
		fsm_.addState ("sleep", getSleep());
		fsm_.addState ("weakup", getWeakup());
		fsm_.addState ("wait", getWait(), "weakup");
		fsm_.addState ("in", getIn (), "weakup");
		fsm_.init ("sleep");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			this.fsm_.post ("in");
		}
	
	}
}
