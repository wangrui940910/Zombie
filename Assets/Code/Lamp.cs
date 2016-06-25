using UnityEngine;
using System.Collections;
using GDGeek;

public class Lamp : MonoBehaviour {
	public GameLamp _shit = null;
	public GameLamp _fire = null;
	private bool isRun_ = false;

	private float _shitTime = 0.5f;
	private float _fireTime = 0.3f;
	private float atime_ = 0f;
	private float stime_ = 0f;
	public void over(){
		isRun_ = false;
	}
	public Body _we = null;
	public FoeBody _foe = null;
	public void reset(){
		isRun_ = true;
		atime_ = 0f;
		stime_ = 0f;
		_foe._animator.Play ("shit");
	}
	public void begin(){
		isRun_ = true;
		//time_ = 0f;
	}
	public Task winTask(){
		TaskList tl = new TaskList ();

		TaskManager.PushBack (tl, delegate {
			isRun_ = true;
		});

		TaskManager.PushFront (tl, delegate {
			atime_ = 0f;
			isRun_ = false;
		});
		tl.push (_we.moveTask (1));
		TaskSet ts = new TaskSet ();
		ts.push (_we.action ("skill"));


		TaskList ttl = new TaskList ();
		ttl.push (new TaskWait(0.5f));
		ttl.push (_foe.action ("hit"));
		ts.push (ttl);
		tl.push (ts);
		tl.push (_we.resetTask ());
		ttl.push (_foe.action ("die", false));
		return tl;
	}
	public bool isGood(){
		return (Mathf.FloorToInt (atime_ / _fireTime) % 4) == 3;
	}
	public int getGood(){

		return (Mathf.FloorToInt (atime_ / _fireTime));
	}
	// Use this for initialization
	void Start () {
		_shit.light (3);
		_fire.light (2);
		isRun_ = true;
	}
	public Task good(){
		TaskList tl = new TaskList ();

		TaskManager.PushBack (tl, delegate {
			isRun_ = true;
		});

		TaskManager.PushFront (tl, delegate {
			atime_ = 0f;
			isRun_ = false;
		});
		tl.push (_we.moveTask (1));
		TaskSet ts = new TaskSet ();
		ts.push (_we.action ("attack"));


		TaskList ttl = new TaskList ();
		ttl.push (new TaskWait(0.5f));
		ttl.push (_foe.action ("hit"));
		ts.push (ttl);
		tl.push (ts);
		tl.push (_we.resetTask ());
		return tl;
	}
	public Task error(){
		TaskList tl = new TaskList ();

		TaskManager.PushBack (tl, delegate {
			isRun_ = true;
		});

		TaskManager.PushFront (tl, delegate {
			atime_ = 0f;
			isRun_ = false;
		});
		tl.push (_we.moveTask (getGood() * 0.2f));
		tl.push (_we.action ("attack"));
		tl.push (_we.resetTask ());
		return tl;
	
	}
	public bool isShiting(){
		//Debug.Log (Mathf.FloorToInt (time_ / _shitTime));
		return (Mathf.FloorToInt (stime_ / _shitTime) > 5);
	
	}
	// Update is called once per frame
	void Update () {
		if (isRun_) {
			stime_ += Time.deltaTime;
			atime_ += Time.deltaTime;
			_shit.light (Mathf.FloorToInt(stime_/_shitTime)%6);
			_fire.light (Mathf.FloorToInt(atime_/_fireTime)%4);
		}
	}
}
