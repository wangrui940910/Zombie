using UnityEngine;
using System.Collections;
using GDGeek;

public class Outside : MonoBehaviour {
	public GameLine _line = null;
	public Camera _camera;
	private bool isWeakup_ = false;
	private FSM fsm_ = new FSM();
	public Task run(){
		Task task = new Task ();//一个任务
		task.init = delegate {//任务开始的，时候
			//isWeakup_ = true; //设置为醒来
			this.fsm_.post("weakup");//传给FSM一个消息，说我醒来了	
		};
		task.isOver = delegate {
			return !isWeakup_;//如果这个标记为false，那么我这个任务结束	
		};
		return task;

	}
	State getSleep(){
		var sleep = new StateWithEventMap ();//在睡眠状态
		sleep.addAction ("weakup", "weakup");//得到weakup消息，我就切换到weakup状态

		sleep.onStart += delegate {
			_camera.gameObject.SetActive(false);//在进入睡眠状态的时候，关闭我外面的摄像机
		};
		sleep.onOver += delegate {
			_camera.gameObject.SetActive(true);//在我离开睡眠的时候，打开外面的摄像机
		};

		return sleep;
	}
	State getWeakup(){

		var weakup = new StateWithEventMap ();
		weakup.onStart += delegate {
			isWeakup_ = true;//进入到wakup状态我就把标记设置为true
		};

		weakup.onOver += delegate {
			isWeakup_ = false;//退出weakup状态，我设置成false
		};
		weakup.addAction ("sleep", "sleep");//如果接收到sleep 我转换为sleep状态
		return weakup;

	}

	State getWait(){
		var isIn = false;
		StateWithEventMap ts = new StateWithEventMap ();

		ts.addAction ("space", "in");//如果输入空格，进入in
		return ts;

	}

	State getIn(){
		var instate = TaskState.Create (delegate() {
			return _line.inTask();//向前走一格
		}, fsm_, "sleep");
		return instate;

	}


	// Use this for initialization
	void Awake () {
		fsm_.addState ("sleep", getSleep());//睡眠，我这里不处理，交给别人处理
		fsm_.addState ("weakup", getWeakup());//我这里处理
		fsm_.addState ("wait", getWait(), "weakup");//我在等待输入
		fsm_.addState ("in", getIn (), "weakup");//向前走一步
		fsm_.init ("sleep");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			this.fsm_.post ("space");
		}
	
	}
}
