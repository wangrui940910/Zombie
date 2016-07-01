using UnityEngine;
using System.Collections;
using GDGeek;

public class Lamp : MonoBehaviour {
	public GameLamp _shit = null;
	public GameLamp _fire = null;
	public AudioSource _doshit;
	public Txwx _txwx = null;
	private bool isRun_ = false;
	public AudioSource _win;
	public AudioSource _shu;
	public AudioSource _cool;
	public GameCamera _camera;
	private float _shitTime = 0.75f;
	private float _fireTime = 0.3f;
	private float atime_ = 0f;
	private float stime_ = 0f;
	public void over(){
		isRun_ = false;
	}
	public Body _we = null;
	public FoeBody _foe = null;
	public FoeBody _foe1 = null;
	public FoeBody _foe2 = null;
	public void reset(){
		Debug.Log ("aaaa");
		if (Random.Range (0, 100) % 2 == 0) {
			_foe1.gameObject.SetActive (true);
			_foe = _foe1;
			_foe2.gameObject.SetActive (false);
		}else{
			_foe2.gameObject.SetActive (true);
			_foe = _foe2;
			_foe1.gameObject.SetActive (false);
		}
		isRun_ = true;
		atime_ = 0f;
		stime_ = 0f;
		_foe._animator.Play ("shit");

	}
	public void begin(){
		isRun_ = true;
		//time_ = 0f;
	}
	private bool isOver_ = true;
	public AudioSource _bei;
	public Task winTask(){
		TaskSet tset = new TaskSet ();
		TaskList tl = new TaskList ();
	

		TaskManager.PushBack (tl, delegate {
			isRun_ = true;
			Time.timeScale = 1.0f;	
			_camera.reset();

		});

		TaskManager.PushFront (tl, delegate {
			Time.timeScale = 0.3f;	
			_win.Play();
			atime_ = 0f;
			isRun_ = false;

		});
		tl.push (_camera.win ());
		tl.push (_we.moveTask (1));
		TaskSet ts = new TaskSet ();
		ts.push (_we.action ("skill"));


		TaskList ttl = new TaskList ();
		ttl.push (new TaskWait(0.5f));
		ttl.push (_foe.action ("hit"));
		ts.push (ttl);
		tl.push (ts);

		ttl.push (_foe.action ("die", false));

		Task check = new TaskCheck (delegate() {
			return isOver_; 
		});
		TaskManager.PushFront (check, delegate() {
			SoundEffect.instance.SmallVictorySound();
			isOver_ = false;
		});
		tl.push (_txwx.show ());
		tl.push (check);
		tset.push (tl);
		TaskWait tw = new TaskWait (0.5f);
		TaskManager.PushBack (tw, delegate {
			_bei.Play();	
		});
		tset.push (tw);
		return tset;
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
	public Task attack(bool hit){
		TaskSet ta = new TaskSet();
		Task attack = _we.action ("attack");
		ta.push (attack);
		TaskWait asound = new TaskWait (0.3f);


		TaskManager.PushBack (asound, delegate {
			if(hit){

				SoundEffect.instance.BoxingVictorySound();
			//
			}else{
				SoundEffect.instance.PlayAttackSound(0.1f);
			}
		});
		ta.push (asound);
		return ta;
	}
	public Task good(){
		TaskList tl = new TaskList ();

		TaskManager.PushBack (tl, delegate {
			isRun_ = true;
		});

		TaskManager.PushFront (tl, delegate {
			atime_ = 0f;
			_cool.Play();
			isRun_ = false;
		});
		tl.push (_we.moveTask (1));
		TaskSet ts = new TaskSet ();

		ts.push (attack(true));


		TaskList ttl = new TaskList ();
		ttl.push (new TaskWait(0.5f));
		var hit = _foe.action ("hit");
		TaskManager.PushFront (hit, delegate() {
			SoundEffect.instance.PlayHitSound();
		});
		ttl.push (hit);
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
			_shu.Play();
		});
		tl.push (_we.moveTask (0.3f));
		tl.push (attack(false));
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
			int nshit = Mathf.FloorToInt (stime_ / _shitTime) % 6;
			if (nshit != _shit.n) {
				_doshit.Play ();
				_shit.light (nshit);
			}

			_fire.light (Mathf.FloorToInt(atime_/_fireTime)%4);
		}
		if (Input.GetKey (KeyCode.Space)) {
			isOver_ = true;
		}
	}
}
